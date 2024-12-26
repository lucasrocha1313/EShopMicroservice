using System.Reflection;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
            conf.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        // Add message broker
        // The assembly is needed here because this is a consumer
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
