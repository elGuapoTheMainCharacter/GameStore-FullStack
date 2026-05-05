using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using Microsoft.EntityFrameworkCore; // Added for database logic

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json", optional: true);

// 1. CORS
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

// 2. Database Connection (Neon Postgres)
var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddNpgsql<GameStoreContext>(connString);

var app = builder.Build();

app.UseCors("AllowReactApp");

app.MapGamesEndpoints();
app.MapGenresEndpoints();

// 3. Neon Auto-Setup (Replaces MigrateDbAsync)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
    // This creates the tables in Neon if they don't exist
    await dbContext.Database.EnsureCreatedAsync(); 
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
Console.WriteLine($"✅ ASP.NET Core is listening on port: {port}");

app.Run();
