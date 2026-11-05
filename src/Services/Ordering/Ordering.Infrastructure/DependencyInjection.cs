using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace Ordering.API.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
       var connectionString = configuration.GetConnectionString("Database");
        //Add services to the container
        services.AddScoped<ISaveChangesInterceptor,AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor,DispatchDomainEventsInterceptors>();
        services.AddDbContext<ApplicationDbContext>((sp,options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
            options.UseSqlServer(connectionString);
        });
        services.AddScoped<IApplicationDbContext,ApplicationDbContext>();
        //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
       return services;
    }
}

