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
var app = builder.Build();

// Configure the HTTP pipeline
app.MapCarter();
app.Run();

