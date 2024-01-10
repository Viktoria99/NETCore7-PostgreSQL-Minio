using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace QrCodeService.Api.Extensions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseSwaggerWithVersioning(this WebApplication app, IWebHostEnvironment environment)
        {
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            $"API docs {description.GroupName.ToUpperInvariant()}");
                        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
                    }
                    options.RoutePrefix = string.Empty;
                    options.EnableDeepLinking();
                });
            }
            return app;
        }
    }
}