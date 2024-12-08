using BuildingBlocks.Exceptions.Handler;
using Carter;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddExceptionHandler<CustomExceptionHandler>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();
        app.UseExceptionHandler(options => {}); //The empty options is a workaround to satisfy the framework's need for explicit configuration
        return app;
    }
}