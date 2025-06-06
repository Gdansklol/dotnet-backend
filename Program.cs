var builder = WebApplication.CreateBuilder(args);

// Register controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();        // 
builder.Services.AddSwaggerGen(); 

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()    // Allow requests from any origin
              .AllowAnyHeader()    // Allow any HTTP headers
              .AllowAnyMethod());  // Allow any HTTP methods (GET, POST, etc.)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                              // 
    app.UseSwaggerUI();                            // 
}


// Important: Middleware registration order matters
app.UseCors();                     // Enable CORS
app.UseAuthorization();           // Authorization middleware
app.MapControllers();             // Map controller routes
app.Run();                        // Run the application
