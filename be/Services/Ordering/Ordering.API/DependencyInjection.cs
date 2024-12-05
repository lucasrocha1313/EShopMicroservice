using Carter;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}