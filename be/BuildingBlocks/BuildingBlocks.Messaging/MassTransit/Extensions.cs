using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    /// <summary>
    /// Add message broker to the service collection
    /// For consumers the assembly is needed to scan for consumers
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration">This is used to get settings information</param>
    /// <param name="assembly">This is needed to scan the assembly for consumers. It is not needed for publishers.</param>
    /// <returns></returns>
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null // This is needed to scan the assembly for consumers. It is not needed for publishers.
    )
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            // Adds all consumers and consumer definitions in the specified an assembly.
            if (assembly != null)
                config.AddConsumers(assembly);

            config.UsingRabbitMq(
                (context, cfg) =>
                {
                    cfg.Host(
                        new Uri(configuration["MessageBroker:Host"]!),
                        h =>
                        {
                            h.Username(configuration["MessageBroker:Username"]!);
                            h.Password(configuration["MessageBroker:Password"]!);
                        }
                    );

                    cfg.ConfigureEndpoints(context);
                }
            );
        });

        return services;
    }
}
