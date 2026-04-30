using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
    string GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate
);