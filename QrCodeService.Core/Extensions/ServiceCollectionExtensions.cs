using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using QrCodeService.Core.Services;
using QrCodeService.Core.Services.Implementations;
using QrCodeService.Core.Services.Interfaces;
using QrCodeService.Database.Extensions;
using QrCodeService.Rabbit.Publisher.Services;
using QrCodeService.Rabbit.Publisher.Services.Implementation;
using QrCodeService.Rabbit.Types.Tasks;
using System.Reflection;

namespace QrCodeService.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
    {
        Assembly.GetAssembly(typeof(DocumentRabbitTask));
        Assembly.GetAssembly(typeof(EventRabbitTask));
        services.AddDatabaseContext(configuration);
        services.AddRabbitConfiguration(configuration, LogManager.GetCurrentClassLogger());
        services.AddRabbitPublisher();
        services.AddScoped<IPartnerServicePublisher<DocumentRabbitTask>, PartnerServiceDocumentPublisher>();
        services.AddScoped<IPartnerServicePublisher<EventRabbitTask>, PartnerServiceEventPublisher>();
        services.AddScoped<IPartnerServicePublisher<QrCodeRabbitTask>, PartnerServiceQrCodePublisher>();
        return services;
    }

    public static IServiceCollection AddCommandServices(this IServiceCollection services)
    {
        services.AddScoped<IPartnerEventService, PartnerEventService>();
        services.AddScoped<IEventProcessingServicesFactory, EventProcessingServicesFactory>();

        services.AddScoped<IEventProcessingService, DocFlowService>();
        services.AddScoped<IEventProcessingService, ResponseService>();
        services.AddScoped<IEventProcessingService, DocResponseService>();

        return services;
    }

    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IQrCodeRequestService, QrCodeRequestService>();
        return services;
    }
}