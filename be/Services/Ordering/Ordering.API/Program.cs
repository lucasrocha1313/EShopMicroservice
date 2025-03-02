using Ordering.API;
using Ordering.API.Middlewares;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Database") ?? string.Empty;
builder
    .Services.AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(connectionString)
    .AddApiServices(connectionString);

var app = builder.Build();

//Add middlewares to validate pagination.
app.UseMiddleware<PaginationValidationMiddleware>();

// Configure the HTTP request pipeline.
app.UseApiServices();

if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
