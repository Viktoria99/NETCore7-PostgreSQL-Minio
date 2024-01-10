using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;
using System.Reflection;

namespace QrCodeService.Core.Extensions;
public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureCore(this IHostBuilder builder, IHostEnvironment environment, string[] args)
    {
        builder.ConfigureLogging((hostingContext, logging) =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
            logging.AddNLog(new XmlLoggingConfiguration(
                Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", "Config", environment.EnvironmentName, "nlog.config")));
        });
        builder.ConfigureHostConfiguration(configurationBuilder =>
        {
            configurationBuilder.
                ConfigureCore(environment, args);
        });
        return builder;
    }

    public static IConfigurationBuilder ConfigureCore(this IConfigurationBuilder builder, IHostEnvironment environment, string[] args)
    {
        builder
            .SetBasePath(environment.ContentRootPath)
            .AddJsonFile(Path.Combine("Config", environment.EnvironmentName, "appsettings.json"), false, false)
            .AddJsonFile(Path.Combine("Config", environment.EnvironmentName, "rabbit-queues-settings.json"), false, false);
        builder
            .AddEnvironmentVariables()
            .AddCommandLine(args);
        return builder;
    }
}