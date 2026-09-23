namespace MusicDistribution.Domain.Entities;

public class Artist
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Country { get; set; }

    public ICollection<Track> Tracks { get; set; } = new List<Track>();
}
