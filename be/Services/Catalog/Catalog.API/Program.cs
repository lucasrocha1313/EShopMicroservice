using Carter;
using Catalog.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

//TODO - move these to a separate file
//Add services to the container
var assembly = typeof(Program).Assembly;
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

//Configure the http request pipeline
app.MapCarter();

app.Run();