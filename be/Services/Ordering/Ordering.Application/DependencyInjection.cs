using System.Reflection;
using BuildingBlocks.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        
        return services;
    }
}