using Discounts.Grpc.Data;
using Discounts.Grpc.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//TODO: Remove swagger if it do not work
builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Database"));
});

builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "Discount gRPC transcoding", Version = "v1" });
    
    var filePath = Path.Combine(AppContext.BaseDirectory, "Discounts.Grpc.xml");
    c.IncludeXmlComments(filePath);
    c.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});

var app = builder.Build();

//TODO: Remove swagger if it do not work
app.UseSwagger();
if(app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.MapGrpcService<DicountService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();