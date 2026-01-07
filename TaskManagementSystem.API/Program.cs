using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// =======================
// Configure Serilog HERE
// =======================
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 14)
    .CreateLogger();

builder.Host.UseSerilog();


// =======================
// Add services to the container
// =======================

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagementSystemDb"));

// Add repositories, services and middlewares
builder.Services.AddRepositories();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =======================
// Configure the HTTP request pipeline
// =======================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Permission Middleware (HERE)
app.UseMiddleware<PermissionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
