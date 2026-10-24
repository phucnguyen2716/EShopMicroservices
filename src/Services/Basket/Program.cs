using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Middleware;
using Discount.Grpc;
using HealthChecks.UI.Client;
using HealthChecks.UI.Core;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

//Application Services
var assembly=typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValiddationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

//Data Services
builder.Services.AddMarten(opts=>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x=>x.UserName);
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository,BasketRepository>();
builder.Services.Decorate<IBasketRepository,CacheBasketRepository>();
builder.Services.AddSingleton<CacheLockService>();

//Grpc Servicees
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!); 
});

//Cross-Cutting Service
builder.Services.AddExceptionHandler<CustomHandlerException>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);
var app = builder.Build();
// Configure the HTTP pipeline
app.UseExceptionHandler(options => { });
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapCarter();

app.Run();
