// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using QrCodeService.Consumer.Extensions;
using QrCodeService.Consumer.Handlers;
using QrCodeService.Core.Extensions;
using QrCodeService.Core.Services;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Minio.Extensions;
using QrCodeService.Rabbit.Types.Tasks;
using RabbitMQ.Extensions;
using RabbitMQ.Interfaces;
using System.Diagnostics.CodeAnalysis;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            var rootPath = Path.GetDirectoryName(typeof(Program).Assembly.Location) ?? string.Empty;
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environments.Production;
            LogManager.Configuration = new XmlLoggingConfiguration(Path.Combine(rootPath, "Config", environment, "nlog.config"));
            var logger = LogManager.GetCurrentClassLogger();
            logger.Debug($"Running consumer with environment: {environment}");
            var hostingEnvironment = new HostingEnvironment
            {
                EnvironmentName = environment,
                ContentRootPath = AppDomain.CurrentDomain.BaseDirectory
            };
            var host = Host.CreateDefaultBuilder(args)
                .UseEnvironment(environment)
                .ConfigureCore(hostingEnvironment, args)
                .ConfigureLogging((_, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddNLog(new NLogProviderOptions
                    {
                        IncludeScopes = true,
                        CaptureMessageTemplates = true,
                        CaptureMessageProperties = true,
                        ParseMessageTemplates = true
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddCore(hostContext.Configuration);
                    services.AddMinioConfig(hostContext.Configuration);
                    services.AddHttpClients(hostContext.Configuration);
                    services.AddScoped<IPartnerFileService, PartnerFileService>();
                    services.AddScoped<IPartnerEventService, PartnerEventService>();
                    services.AddRabbitListener();
                    services.AddScoped<IRabbitService<DocumentRabbitTask>, TrafficDocumentHandler>();
                }).Build();

            List<string>? filterQueues = null;
            try
            {
                logger.Debug("Running consumer");
                host.UseRabbit(logger);
                var q = args.FirstOrDefault(s => s.StartsWith("--queue="));
                if (q != null)
                {
                    filterQueues = q[8..].Split(",").ToList();
                    logger.Debug($"Running consumer for queues: {string.Join(",", filterQueues)}");
                }
                host.StartRabbitListeners(filterQueues);
                await host.RunAsync();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp);
            }
            finally
            {
                host.StopRabbitListeners(filterQueues);
                LogManager.Shutdown();
            }

        }
        catch (Exception exception)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Fatal(exception);
            Console.WriteLine(exception);
            Environment.Exit(1);
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
}