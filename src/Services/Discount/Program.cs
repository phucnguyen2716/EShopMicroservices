using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddDbContext<DiscountContext>(opts=>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Database")));
var app = builder.Build();
app.UseMigration();
// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}
app.MapGrpcService<DiscountService>();
app.Run();
