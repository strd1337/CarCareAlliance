using CarCareAlliance.Application.Common.Interfaces.Persistance.CommonRepositories;
using CarCareAlliance.Application.Common.Services;
using CarCareAlliance.Domain.Common.Models;
using CarCareAlliance.Infrastructure.Persistance;
using CarCareAlliance.Infrastructure.Persistance.Repositories.Common;
using CarCareAlliance.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CarCareAlliance.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services
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
                .AddScoped<IUnitOfWork, UnitOfWork>();

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
    }
}