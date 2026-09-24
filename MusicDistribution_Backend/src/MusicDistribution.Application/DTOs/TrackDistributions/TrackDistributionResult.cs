namespace MusicDistribution.Application.DTOs.TrackDistributions;

public enum TrackDistributionError
{
    None,
    TrackNotFound,
    DspNotFound
}

public sealed record TrackDistributionResult(
    IReadOnlyList<TrackDistributionDto> Submitted,
    IReadOnlyList<int> AlreadyDistributedDspIds,
    TrackDistributionError Error);
