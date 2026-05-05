using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json", optional: true);

// 1. CORS - Stay strict but allow your Vercel URL
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173", 
                "https://game-store-full-stack-bwbe.vercel.app"
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 2. Robust Connection String Lookup
// This looks in both Environment Variables and appsettings.json
var connString = builder.Configuration.GetConnectionString("GameStore") 
                 ?? builder.Configuration["ConnectionStrings:GameStore"];

if (string.IsNullOrEmpty(connString))
{
    throw new InvalidOperationException("CRITICAL: Database connection string 'GameStore' is missing!");
}

builder.Services.AddNpgsql<GameStoreContext>(connString);

var app = builder.Build();

app.UseCors("AllowReactApp");

app.MapGamesEndpoints();
app.MapGenresEndpoints();

// 3. Robust Neon Initialization with Retry Logic
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    
    int retries = 5;
    while (retries > 0)
    {
        try
        {
            logger.LogInformation("Attempting to connect to Neon database...");
            await dbContext.Database.EnsureCreatedAsync();
            logger.LogInformation("✅ Database initialized successfully.");
            break; 
        }
        catch (Exception ex)
        {
            retries--;
            logger.LogWarning($"⚠️ Database initialization failed. Retrying... ({retries} attempts left)");
            logger.LogWarning($"Error: {ex.Message}");
            
            if (retries == 0)
            {
                logger.LogCritical("❌ Could not connect to database after multiple attempts.");
                throw;
            }
            
            await Task.Delay(5000); // Wait 5 seconds for Neon to wake up
        }
    }
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
Console.WriteLine($"🚀 Game Store API is live on port: {port}");

app.Run();
