using System.Text.RegularExpressions;

namespace ApiGateways.Extensions;

public static class CorsExtensions
{
    private static readonly Regex[] ValidCorsRegexes =
    [
        new Regex(@"^http://localhost", RegexOptions.Compiled),
        new Regex(@"^https://localhost", RegexOptions.Compiled),
    ];

    /// <summary>
    /// Add CORS policy to the application
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMyCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder
                    .SetIsOriginAllowed(origin =>
                        ValidCorsRegexes.Any(x => x.Matches(origin).Count > 0)
                    )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
            );
        });

        return services;
    }
}
