using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);
// Add services to container.
var assembly= typeof(Program).Assembly;
builder.Services.AddMediatR(config=>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValiddationBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMarten(opts=>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP pipeline
app.MapCarter();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();


