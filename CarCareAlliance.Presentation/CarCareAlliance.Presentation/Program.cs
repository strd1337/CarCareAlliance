using CarCareAlliance.Application;
using CarCareAlliance.Infrastructure;
using CarCareAlliance.Presentation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration, builder.Environment);

builder.Services.AddCors(options => options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection connection string is not configured.");

bool running = true;
builder.Services.AddHealthChecks()
    .AddCheck("self", () => running ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy())
    .AddSqlServer(connectionString, name: "services");
    
var app = builder.Build();

var useSwagger = builder.Configuration.GetValue<bool>("Kestrel:UseSwagger");
if (app.Environment.IsDevelopment() || useSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseExceptionHandler("/error");

app.Map("/error", (HttpContext httpContext) =>
{
    Exception? exception = httpContext.Features
               .Get<IExceptionHandlerFeature>()?.Error;

    return Results.Problem(
        title: exception?.Message,
        statusCode: StatusCodes.Status400BadRequest);
});

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("Open");

app.MapControllers();
app.MapFallbackToFile("index.html");

app.UseHealthChecks("/health/live", new HealthCheckOptions { Predicate = healthCheck => healthCheck.Name.Equals("self") });
app.UseHealthChecks("/health/ready", new HealthCheckOptions { Predicate = healthCheck => healthCheck.Name.Equals("services") });

await app.RunAsync();