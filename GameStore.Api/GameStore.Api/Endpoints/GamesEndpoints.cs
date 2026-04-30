using System;
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games")
                        .WithParameterValidation();

        // GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
            await dbContext.Games
                    .Include(game => game.Genre)
                    .Select(game => game.ToGameSummaryDto())
                    .AsNoTracking()
                    .ToListAsync());

        // GET /games/1
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            // Use Include here too so ToGameDetailsDto doesn't fail if it needs the Genre object
            Game? game = await dbContext.Games
                                        .Include(g => g.Genre)
                                        .FirstOrDefaultAsync(g => g.Id == id);

            return game is null ?
            Results.NotFound() : Results.Ok(game.ToGameDetailsDto());
        })
        .WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            // 1. Look up the Genre ID based on the string Name sent from React
            // Inside MapPost and MapPut
var genre = await dbContext.Genres
    .FirstOrDefaultAsync(g => g.Name.ToLower() == newGame.GenreId.ToLower());

            
            if (genre is null) return Results.BadRequest("Invalid Genre name.");

            // 2. Pass the found Genre ID into the mapping
            // Note: Update your GameMapping.ToEntity to accept (newGame, genre.Id)
            Game game = newGame.ToEntity(genre.Id);

            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync(); // SQLite generates the ID here automatically

            return Results.CreatedAtRoute(
                GetGameEndpointName,
                new { id = game.Id },
                game.ToGameDetailsDto());
        });

        // PUT /games/{id}
        group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            var existingGame = await dbContext.Games.FindAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            // 1. Look up the Genre ID for the update
            var genre = await dbContext.Genres
                                        .FirstOrDefaultAsync(g => g.Name.ToLower() == updatedGame.GenreId.ToLower());

            if (genre is null) return Results.BadRequest("Invalid Genre name.");

            // 2. Map the DTO to the entity using the found ID
            // Note: Update your GameMapping.ToEntity to accept (updatedGame, id, genre.Id)
            dbContext.Entry(existingGame)
                     .CurrentValues
                     .SetValues(updatedGame.ToEntity(id, genre.Id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /games/1
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            await dbContext.Games
                     .Where(game => game.Id == id)
                     .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
