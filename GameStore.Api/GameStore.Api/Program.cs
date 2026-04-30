using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("secrets.json", optional: true);

// 1. Register the CORS service (must be before builder.Build())
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // Removed the trailing slash
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSqlite<GameStoreContext>(
    builder.Configuration.GetConnectionString("GameStore"));

var app = builder.Build();

// 2. Use the Middleware (must be after app.Build() but BEFORE Map endpoints)
app.UseCors("AllowReactApp");

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

var port = Environment.GetEnvironmentVariable("PORT") ?? "unknown";
Console.WriteLine($"✅ ASP.NET Core is listening on port: {port}");

app.Run();