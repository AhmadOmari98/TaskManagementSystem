using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskManagementSystem.API.Middlewares;
using TaskManagementSystem.API.Swagger;
using TaskManagementSystem.Application;
using TaskManagementSystem.Infrastructure;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Seed;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog 
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 14)
    .CreateLogger();

builder.Host.UseSerilog();

// DbContext (InMemory)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagementSystemDb"));

// Repositories & Services
builder.Services.AddRepositories();
builder.Services.AddServices();

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddHeadersOperationFilter>();
});

var app = builder.Build();

// Seed Database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    DbSeeder.Seed(dbContext);
}

// Global exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Permission / Authorization middleware
app.UseMiddleware<PermissionMiddleware>();

app.UseAuthorization();

// Save changes after request
app.UseMiddleware<SaveChangesMiddleware>();

app.MapControllers();

app.Run();
