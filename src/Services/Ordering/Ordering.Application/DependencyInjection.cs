using System.Reflection;
using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.API.Application;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>{
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValiddationBehavior<,>));
        });
        
        return services;
    }
}
