using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Messaging.MassTransit;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly? assembly = null // This is needed to scan the assembly for consumers. It is not needed for publishers.
    )
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            // Add consumers from the assembly
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
