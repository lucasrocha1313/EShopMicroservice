using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder
    .Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddRateLimiter(r =>
{
    r.AddFixedWindowLimiter("fixed", o =>
    {
        o.Window = TimeSpan.FromSeconds(10);
        o.PermitLimit = 5;
    });
});

var app = builder.Build();

//Configure the HTTP request pipeline.
app.UseRateLimiter();
app.MapReverseProxy();

app.Run();
