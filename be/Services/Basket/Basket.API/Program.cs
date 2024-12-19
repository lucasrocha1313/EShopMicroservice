using Basket.API.Configurations;
using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Messaging.MassTransit;
using Carter;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var assembly = typeof(Program).Assembly;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Data services
builder
    .Services.AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database") ?? string.Empty);
        opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    })
    .UseLightweightSessions();

// Register repositories
builder.Services.AddScoped<BasketRepository>();

//Register Repository decorator - Manual Proxy Instantiation
//If this grows in complexity, consider using Scrutor(https://github.com/khellang/Scrutor).
builder.Services.AddScoped<IBasketRepository, CacheBasketRepository>(provider =>
{
    var basketRepository = provider.GetRequiredService<BasketRepository>();
    return new CacheBasketRepository(
        basketRepository,
        provider.GetRequiredService<IDistributedCache>(),
        provider.GetRequiredService<IOptions<RedisOptions>>()
    );
});

// Register options
builder.Services.Configure<RedisOptions>(builder.Configuration.GetSection("RedisOptions"));

// Register cache
builder.Services.AddStackExchangeRedisCache(opts =>
{
    opts.Configuration = builder.Configuration.GetConnectionString("Redis") ?? string.Empty;
    opts.InstanceName = builder.Configuration["RedisOptions:InstanceName"];
});

// Register gRPC client
builder.Services.AddGrpcClient<DiscountService.DiscountServiceClient>(opts =>
{
    opts.Address = new Uri(builder.Configuration["GrpcSettings:DiscountsUrl"] ?? string.Empty);
});

//Async communication services
builder.Services.AddMessageBroker(builder.Configuration, assembly);

//Cross-cutting Services
// Register exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder
    .Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database") ?? string.Empty)
    .AddRedis(builder.Configuration.GetConnectionString("Redis") ?? string.Empty); // Add health check services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapCarter();
app.UseExceptionHandler(_ => { });
app.UseHealthChecks(
    "/health",
    new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse }
); // Add health check endpoint

app.Run();
