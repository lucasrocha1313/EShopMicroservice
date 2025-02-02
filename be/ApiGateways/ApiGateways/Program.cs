using ApiGateways.Extensions;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMyCorsPolicy();

//Add services to the container.
builder
    .Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(r =>
{
    r.AddFixedWindowLimiter(
        "fixed",
        o =>
        {
            o.Window = TimeSpan.FromSeconds(10);
            o.PermitLimit = 5;
        }
    );
});

var app = builder.Build();

// Allows the client app to make requests to the API Gateway.
app.UseCors();

//Configure the HTTP request pipeline.
app.UseRateLimiter();
app.MapReverseProxy();

app.Run();
