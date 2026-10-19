using BuildingBlocks.Exceptions.Handler;
using BuildingBlocks.Middleware;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assembly=typeof(Program).Assembly;
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValiddationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddMarten(opts=>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
    opts.Schema.For<ShoppingCart>().Identity(x=>x.UserName);
}).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository,BasketRepository>();
builder.Services.AddExceptionHandler<CustomHandlerException>();
var app = builder.Build();
// Configure the HTTP pipeline
app.UseExceptionHandler(options => { });
app.UseMiddleware<CorrelationIdMiddleware>();

app.MapCarter();

app.Run();
