using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Diagnostics.CodeAnalysis;

namespace QrCodeService.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class HealthChecksPingFilter : IDocumentFilter
    {
        public const string HealthCheckEndpoint = @"/hc/ping";
        public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)
        {
            var pathItem = new OpenApiPathItem();

            var operation = new OpenApiOperation();
            operation.Tags.Add(new OpenApiTag { Name = "HealthCheck" });

            var response = new OpenApiResponse();
            response.Description = "Success";
            response.Content.Add("application/json", new OpenApiMediaType()
            {
                Schema = context.SchemaGenerator.GenerateSchema(typeof(HealthReport), context.SchemaRepository),
            });

            operation.Responses.Add("200", response);
            operation.Summary = "Check service availibale";
            pathItem.AddOperation(OperationType.Get, operation);
            openApiDocument?.Paths.Add(HealthCheckEndpoint, pathItem);
        }
    }
}