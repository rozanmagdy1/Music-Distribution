using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Application.DTOs.Tracks;

public class TrackCreateDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int ArtistId { get; set; }

    [Required]
    [StringLength(12, MinimumLength = 12)]
    [RegularExpression("^[A-Z]{2}[A-Z0-9]{3}[0-9]{7}$")]
    public string ISRC { get; set; } = string.Empty;

    [Required]
    public DateOnly? ReleaseDate { get; set; }

    [Required]
    [StringLength(100)]
    public string Genre { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(TrackStatus))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TrackStatus? Status { get; set; }
}
