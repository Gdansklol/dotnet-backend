using dotnet_backend.Models;     // MongoDB settings model
using DotNetEnv;                 // .env file reader
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Configure MongoDB settings from environment variables
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") ?? "";
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE") ?? "";
    options.CollectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION") ?? "";
});

// Register controllers
builder.Services.AddControllers();

// Register Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()   // Allow requests from any origin
              .AllowAnyHeader()   // Allow any headers
              .AllowAnyMethod()); // Allow all HTTP methods
});

var app = builder.Build();

// Enable Swagger UI in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS (must come before authorization)
app.UseCors();

// Enable authorization middleware (if you use [Authorize])
app.UseAuthorization();

// Map attribute-routed controllers
app.MapControllers();

// Start the application
app.Run();
