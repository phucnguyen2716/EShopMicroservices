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
            cfg.AddBehavior(typeof(LoggingBehavior<,>));
            cfg.AddBehavior(typeof(ValiddationBehavior<,>));
        });
        
        return services;
    }
}
