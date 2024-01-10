using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting.Internal;
using NLog.Web;
using QrCodeService.Api.Extensions;
using QrCodeService.Core.Extensions;
using QrCodeService.Database;


var rootPath = Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? "";
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Development;

var hostingEnvironment = new HostingEnvironment
{
    EnvironmentName = environment,
    ContentRootPath = AppDomain.CurrentDomain.BaseDirectory
};

NLogBuilder.ConfigureNLog(Path.Combine(rootPath, "Config", environment, "nlog.config"));

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(rootPath)
    .ConfigureCore(hostingEnvironment, args)
    .Build();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddConfiguration(configurationBuilder);
builder.Services.AddCore(builder.Configuration);
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});


builder.Services.AddSwaggerWithVersioning(builder.Environment);
builder.Services.AddHttpContextAccessor();

var healthChecksBuilder = builder.Services
           .AddHealthChecks()
           .AddCheck("ping", () => new HealthCheckResult(HealthStatus.Healthy), new string[] { "ping" })
           .AddDbContextCheck<DatabaseContext>("db", tags: new string[] { "db" });

var rabbitSettings = builder.Configuration.GetSection("RabbitSettings").Get<RabbitSettings>();
var rabbitHostIndex = 1;
rabbitSettings?.Hosts.ForEach(hostSettings =>
{
    healthChecksBuilder
        .AddRabbitMQ(rabbitConnectionString:
            $"amqps://{rabbitSettings.UserName}:{rabbitSettings.Password}@{hostSettings.HostName}:{hostSettings.Port}",
            name: $"rabbit_{rabbitHostIndex}", tags: new[] { "rabbit", $"rabbit_{rabbitHostIndex}" });
    rabbitHostIndex++;
});

var app = builder.Build();

app.UseHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
}).UseHealthChecks("/hc/ping", new HealthCheckOptions()
{
    Predicate = check => check.Tags.Contains("ping"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
}).UseHealthChecks("/hc/db", new HealthCheckOptions()
{
    Predicate = check => check.Tags.Contains("db"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
}).UseHealthChecks("/hc/rabbit", new HealthCheckOptions()
{
    Predicate = check => check.Tags.Contains("rabbit"),
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseDeveloperExceptionPage();
}
app.UseSwaggerWithVersioning(builder.Environment);
app.UseResponsesWrapper();
app.MapControllers();
app.Run();