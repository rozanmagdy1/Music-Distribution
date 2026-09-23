namespace MusicDistribution.Domain.Entities;

public class Dsp
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public ICollection<TrackDistribution> TrackDistributions { get; set; } = new List<TrackDistribution>();
}
