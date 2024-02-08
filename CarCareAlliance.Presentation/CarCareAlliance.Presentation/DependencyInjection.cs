﻿using CarCareAlliance.Presentation.Common.Errors;
using CarCareAlliance.Presentation.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;

namespace CarCareAlliance.Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(
            this IServiceCollection services)
        {
            services.AddMappings();

            services.AddControllers();

            services.AddSingleton<ProblemDetailsFactory, CarCareAllianceProblemDetailsFactory>();
            
            services.AddEndpointsApiExplorer();
            services.AddSwagger();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }
    }
}