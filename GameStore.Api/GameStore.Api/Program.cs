using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load optional secrets (local only)
builder.Configuration.AddJsonFile("secrets.json", optional: true);

// ✅ CORS
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

// ✅ Get connection string (Render env OR fallback)
var connString = builder.Configuration.GetConnectionString("GameStore");

if (string.IsNullOrEmpty(connString))
{
    throw new InvalidOperationException("CRITICAL: Database connection string 'GameStore' is missing!");
}

// ✅ PostgreSQL
builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseNpgsql(connString)
);

var app = builder.Build();

app.UseCors("AllowReactApp");

app.MapGamesEndpoints();
app.MapGenresEndpoints();

// ✅ Retry DB connection (Neon cold start safe)
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

            await Task.Delay(5000);
        }
    }
}

// ✅ Render port binding (CRITICAL FIX)
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

Console.WriteLine($"🚀 Game Store API is live on port: {port}");

app.Run();
