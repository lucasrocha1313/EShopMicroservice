using Microsoft.EntityFrameworkCore;

namespace Discounts.Grpc.Data;

/// <summary>
/// Extensions for DiscountContext
/// </summary>
public static class Extentions
{
    /// <summary>
    /// Execute migration for DiscountContext
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        context.Database.MigrateAsync();
        return app;
    }
}