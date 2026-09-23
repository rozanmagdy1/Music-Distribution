using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Domain.Entities;

public class TrackDistribution
{
    public int Id { get; set; }
    public int TrackId { get; set; }
    public int DspId { get; set; }
    public DateTimeOffset SubmittedAt { get; set; }
    public DistributionStatus Status { get; set; }

    public Track Track { get; set; } = null!;
    public Dsp Dsp { get; set; } = null!;
}
