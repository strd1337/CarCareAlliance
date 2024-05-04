using CarCareAlliance.Presentation.Client.Common.Constants;
using CarCareAlliance.Presentation.Client.Handlers;
using CarCareAlliance.Presentation.Client.Models.Auth;
using CarCareAlliance.Presentation.Client.Services;
using CarCareAlliance.Presentation.Client.Services.Implementations;
using CarCareAlliance.Presentation.Client.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using MudBlazor.Services;

namespace CarCareAlliance.Presentation.Client
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureHttpHandlers(this IServiceCollection services)
        {
            services.AddTransient<AuthenticationHandler>();
            services.AddTransient<ShowLoadingHandler>();

            return services;
        }

        public static IServiceCollection AddMudBlazorServices(this IServiceCollection services)
        {
            services.AddMudServices(mudServicesConfiguration =>
            {
                mudServicesConfiguration.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
                mudServicesConfiguration.SnackbarConfiguration.PreventDuplicates = false;
                mudServicesConfiguration.SnackbarConfiguration.NewestOnTop = true;
                mudServicesConfiguration.SnackbarConfiguration.ShowCloseIcon = true;
                mudServicesConfiguration.SnackbarConfiguration.VisibleStateDuration = 4000;
                mudServicesConfiguration.SnackbarConfiguration.HideTransitionDuration = 500;
                mudServicesConfiguration.SnackbarConfiguration.ShowTransitionDuration = 500;
                mudServicesConfiguration.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
            }).AddMudBlazorSnackbar();

            return services;
        }

        public static IServiceCollection ConfigureCustomServices(this IServiceCollection services)
        {
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationState>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IServicePartnerService, ServicePartnerService>();
            services.AddScoped<IMechanicService, MechanicService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IWorkScheduleService, WorkScheduleService>();
            services.AddSingleton<LoadingService>();
            services.AddScoped<HttpErrorsService>();

            return services;
        }

        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services, string baseAddress)
        {
            services.AddHttpClient(Constants.Client, client => client.BaseAddress = new Uri(baseAddress))
                .AddHttpMessageHandler<ShowLoadingHandler>()
                .AddHttpMessageHandler<AuthenticationHandler>();

            return services;
        }
    }
}
