using System.Reflection;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.API.Application;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(cfg=>{
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValiddationBehavior<,>));
        });
        services.AddMessageBroker(configuration,Assembly.GetExecutingAssembly());
        return services;
    }
}
