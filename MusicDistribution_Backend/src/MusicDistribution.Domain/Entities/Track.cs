using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Domain.Entities;

public class Track
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int ArtistId { get; set; }
    public required string ISRC { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public required string Genre { get; set; }
    public TrackStatus Status { get; set; }

    public Artist Artist { get; set; } = null!;
    public ICollection<TrackDistribution> TrackDistributions { get; set; } = new List<TrackDistribution>();
}
