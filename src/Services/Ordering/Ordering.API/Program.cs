using Ordering.API;
using Ordering.API.Infrastructure;
using Ordering.Application;

var builder = WebApplication.CreateBuilder(args);
//Add Services to the container

//-----------------
//Infrastructure - EF Core
//Application - MediatR
//API - Carter, HealthChecks, ...

builder.Services.AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices();
//------------------

var app = builder.Build();

// Configure the HTTP request pipline


app.Run();
