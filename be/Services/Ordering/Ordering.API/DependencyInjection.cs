using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddCarter();
        services.AddProblemDetails(); // Register a "default" response in case your IExceptionHandler return false
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks().AddSqlServer(connectionString);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler();

        app.UseHealthChecks(
            "/health",
            new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
        );
        return app;
    }
}
