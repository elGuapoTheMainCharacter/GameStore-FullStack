using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    string GenreId,
    [Range(1, 1000)] decimal Price,
    DateOnly ReleaseDate
);
