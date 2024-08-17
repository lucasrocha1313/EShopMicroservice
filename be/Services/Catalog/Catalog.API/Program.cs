using BuildingBlocks.Exceptions.Handler;
using Carter;
using Catalog.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the http request pipeline
app.MapCarter();
app.UseExceptionHandler(o => {});

app.Run();