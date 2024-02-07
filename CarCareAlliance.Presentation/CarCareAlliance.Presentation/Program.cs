using CarCareAlliance.Application;
using CarCareAlliance.Infrastructure;
using CarCareAlliance.Presentation;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();