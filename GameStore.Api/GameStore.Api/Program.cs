using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Note: secrets.json won't exist on Render, but 'optional: true' prevents it from crashing.
builder.Configuration.AddJsonFile("secrets.json", optional: true);

// 1. Updated CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
    "http://localhost:5173", 
    "https://game-store-full-stack-bwbe.vercel.app" // Your specific Vercel URL
                )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSqlite<GameStoreContext>(
    builder.Configuration.GetConnectionString("GameStore"));

var app = builder.Build();

// 2. Middleware (Must be before mapping endpoints)
app.UseCors("AllowReactApp");

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

// Render uses the PORT environment variable automatically
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
Console.WriteLine($"✅ ASP.NET Core is listening on port: {port}");

app.Run();
