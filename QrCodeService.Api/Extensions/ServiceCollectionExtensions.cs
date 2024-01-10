using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using QrCodeService.Api.Filters;
using System.Reflection;

namespace QrCodeService.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services, IWebHostEnvironment environment)
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(setup =>
            {
                setup.GroupNameFormat = "KLG";
                setup.SubstituteApiVersionInUrl = true;
            });

            if (environment.IsDevelopment() || environment.IsStaging())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Article = $"{Assembly.GetExecutingAssembly().GetName().Name}", Version = "v1" });

                    var directory = new DirectoryInfo(AppContext.BaseDirectory);
                    foreach (var file in directory.GetFiles($"{Assembly.GetExecutingAssembly().GetName().Name}*.xml"))
                    {
                        c.CustomSchemaIds(t => t.FullName);
                        c.IncludeXmlComments(file.FullName);
                    }
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                    c.DocumentFilter<HealthChecksFilter>();
                    c.DocumentFilter<HealthChecksPingFilter>();
                    c.DocumentFilter<HealthChecksDbFilter>();
                    c.DocumentFilter<HealthChecksRabbitFilter>();

                    c.AddSecurityDefinition("sea", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        Scheme = "sea"
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "sea" }
                        },
                        new string[] {}
                    }
                    });
                });
                services.AddSwaggerGenNewtonsoftSupport();
            }
            return services;
        }
    }
}