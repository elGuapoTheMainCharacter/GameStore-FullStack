using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("secrets.json", optional: true);

// 1. Updated CORS service to include Vercel
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173", 
                "https://vercel.app" // Your live frontend
              )
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSqlite<GameStoreContext>(
    builder.Configuration.GetConnectionString("GameStore"));

var app = builder.Build();

// 2. Middleware (Correctly placed before endpoints)
app.UseCors("AllowReactApp");

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

var port = Environment.GetEnvironmentVariable("PORT") ?? "unknown";
Console.WriteLine($"✅ ASP.NET Core is listening on port: {port}");

app.Run();
