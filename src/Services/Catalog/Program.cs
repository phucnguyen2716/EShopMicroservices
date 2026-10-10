using BuildingBlocks.Behaviors;

var builder = WebApplication.CreateBuilder(args);
// Add services to container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config=>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValiddationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
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


