using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
// Add services to container.
var assembly= typeof(Program).Assembly;
builder.Services.AddMediatR(config=>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
    config.AddOpenBehavior(typeof(ValiddationBehavior<,>));
});
builder.Services.AddCarter();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMarten(opts=>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if(builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<InitialCatalogData>();

builder.Services.AddExceptionHandler<CustomHandlerException>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP pipeline
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseExceptionHandler(options=>{});

app.UseSwagger();
app.UseSwaggerUI();

app.MapCarter();
app.Run();


