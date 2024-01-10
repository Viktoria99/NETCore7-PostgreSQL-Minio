using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace QrCodeService.Api.Filters
{
    public class HealthChecksFilter : IDocumentFilter
    {
        public const string HealthCheckEndpoint = @"/hc";
        public void Apply(OpenApiDocument openApiDocument, DocumentFilterContext context)
        {
            var pathItem = new OpenApiPathItem();

            var operation = new OpenApiOperation();
            operation.Tags.Add(new OpenApiTag { Name = "HealthCheck" });
            operation.Summary = "Check of the environment";

            var response = new OpenApiResponse();
            response.Description = "Success";
            response.Content.Add("application/json", new OpenApiMediaType()
            {
                Schema = context.SchemaGenerator.GenerateSchema(typeof(HealthReport), context.SchemaRepository),
            });

            operation.Responses.Add("200", response);
            pathItem.AddOperation(OperationType.Get, operation);
            openApiDocument?.Paths.Add(HealthCheckEndpoint, pathItem);
        }
    }
}