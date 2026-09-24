using System.Text.Json.Serialization;
using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Application.DTOs.Tracks;

public class TrackDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ArtistId { get; set; }
    public string ISRC { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public string Genre { get; set; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TrackStatus Status { get; set; }
}
