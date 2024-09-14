using Basket.API.Configurations;
using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Marten;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var assembly = typeof(Program).Assembly;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database") ?? string.Empty);
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
}).UseLightweightSessions();

// Register options
builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("RedisOptions"));

// Register repositories
builder.Services.AddScoped<BasketRepository>();

//Register Repository decorator - Manual Proxy Instantiation
//If this grows in complexity, consider using Scrutor(https://github.com/khellang/Scrutor).
builder.Services.AddScoped<IBasketRepository, CacheBasketRepository>(provider =>
{
    var basketRepository = provider.GetRequiredService<BasketRepository>();
    return new CacheBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>(),
        provider.GetRequiredService<IOptions<RedisOptions>>());
});

builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.Configuration = builder.Configuration.GetConnectionString("Redis") ?? string.Empty;
    opts.InstanceName = builder.Configuration["RedisOptions:InstanceName"];
});

// Register exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();
app.UseExceptionHandler(_ =>{});

app.Run();