using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Common.Services;
using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Infrastructure.Persistance;
using CarCareAlliance.Infrastructure.Persistance.Repositories.Common;
using CarCareAlliance.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CarCareAlliance.Application.Common.Interfaces.Persistance.AuthRepositories;
using CarCareAlliance.Infrastructure.Persistance.Repositories.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CarCareAlliance.Infrastructure.Persistance.Repositories.Auth.Roles;
using CarCareAlliance.Infrastructure.Filters.Auth;

namespace CarCareAlliance.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
                .AddAuth(configuration)
                .AddDbContext(configuration)
                .AddPersistance();

            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }

        public static IServiceCollection AddDbContext(
           this IServiceCollection services,
           ConfigurationManager configuration)
        {
            services.AddDbContext<CarCareAllianceDbContext>((sp, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }

        public static IServiceCollection AddPersistance(
            this IServiceCollection services)
        {
            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }

        public static IServiceCollection AddCustomRepository<TEntity, TId, TRepository>(
            this IServiceCollection services)
               where TEntity : Entity<TId>
               where TId : ValueObject
               where TRepository : class, IGenericRepository<TEntity, TId>

        {
            services.AddScoped<IGenericRepository<TEntity, TId>, TRepository>();

            return services;
        }

        public static IServiceCollection AddAuth(
           this IServiceCollection services,
           ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience= true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Secret))
                    });

            services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build());

            services.AddControllersWithViews(options => {
                options.Filters.Add(typeof(JwtAuthorizeFilter));
            });

            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, RoleAuthorizationPolicyProvider>();

            return services;
        }
    }
}