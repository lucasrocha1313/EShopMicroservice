using System.Reflection;
using BuildingBlocks.Behaviors;
using Carter;
using Catalog.API.Data;
using FluentValidation;
using Marten;
using Weasel.Core;

namespace Catalog.API.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServices(this IServiceCollection services, ConfigurationManager configuration, bool isDevelopment)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddValidatorsFromAssembly(assembly);
        services.AddCarter();

        services.AddMarten(opts =>
        {
            opts.Connection(configuration.GetConnectionString("Database")!);
            opts.DisableNpgsqlLogging = true;
            opts.UseNewtonsoftForSerialization(EnumStorage.AsString);
        }).UseLightweightSessions();

        if (isDevelopment)
        {
            services.InitializeMartenWith<CatalogInitialData>();
        }

        return services;
    }
}