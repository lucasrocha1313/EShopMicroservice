using System.Reflection;
using BuildingBlocks.Behaviors;
using Carter;
using FluentValidation;
using Marten;

namespace Catalog.API.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
            //TODO - read about IPipelineBehavior
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(assembly);
        services.AddCarter();

        services.AddMarten(opts =>
        {
            opts.Connection(configuration.GetConnectionString("Database")!);
            opts.DisableNpgsqlLogging = true;
        }).UseLightweightSessions();

        return services;
    }
}