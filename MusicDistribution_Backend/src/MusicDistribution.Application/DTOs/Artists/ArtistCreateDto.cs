using System.ComponentModel.DataAnnotations;

namespace MusicDistribution.Application.DTOs.Artists;

public class ArtistCreateDto
{
    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Country { get; set; } = string.Empty;
}
