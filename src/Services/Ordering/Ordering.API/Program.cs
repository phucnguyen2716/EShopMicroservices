using Ordering.API;
using Ordering.API.Infrastructure;
using Ordering.API.Infrastructure.Data.Extensions;

using Ordering.API.Application;

var builder = WebApplication.CreateBuilder(args);
//Add Services to the container

//-----------------
//Infrastructure - EF Core
//Application - MediatR
//API - Carter, HealthChecks, ...

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);
//------------------

var app = builder.Build();

// Configure the HTTP request pipline
app.UseApiServices();
if(app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

await app.RunAsync();
