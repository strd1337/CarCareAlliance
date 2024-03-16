using Blazored.LocalStorage;
using CarCareAlliance.Presentation.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.ConfigureCustomServices();
builder.Services.ConfigureHttpHandlers();
builder.Services.ConfigureHttpClients(builder.HostEnvironment.BaseAddress);
builder.Services.AddMudBlazorServices();

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();