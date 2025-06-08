using dotnet_backend.Models;
using dotnet_backend.Services;
using DotNetEnv;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file (only works locally)
Env.Load();

// Bind MongoDB settings
builder.Services.Configure<MongoDBSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_STRING") ?? "";
    options.DatabaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE") ?? "";
    options.CollectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION") ?? "";
});

// Register services
builder.Services.AddControllers();
builder.Services.AddSingleton<BookService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow all CORS (for testing / dev)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// ALWAYS enable Swagger (including Production)
app.UseSwagger();
app.UseSwaggerUI();

// Middleware order matters
app.UseCors();
app.UseAuthorization();
app.MapControllers();

app.Run();
