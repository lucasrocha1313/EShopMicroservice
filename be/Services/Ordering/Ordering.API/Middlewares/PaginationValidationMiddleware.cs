namespace Ordering.API.Middlewares;

/// <summary>
/// Middleware to validate pagination.
/// </summary>
public class PaginationValidationMiddleware
{
    private readonly RequestDelegate _next;

    public PaginationValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //Page number must be greater than or equal to 1.
        if (context.Request.Query.TryGetValue("PageNumber", out var pageNumberValue) &&
            int.TryParse(pageNumberValue, out var pageNumber) &&
            pageNumber < 1)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { Error = "PageNumber must be greater than or equal to 1." });
            return;
        }

        await _next(context);
    }
}