using MusicDistribution.Application.DTOs.TrackDistributions;

namespace MusicDistribution.Application.Interfaces;

public interface ITrackDistributionService
{
    Task<IReadOnlyList<TrackDistributionDto>> GetForTrackAsync(
        int trackId,
        CancellationToken cancellationToken = default);

    Task<TrackDistributionResult> DistributeAsync(
        int trackId,
        IReadOnlyCollection<int> dspIds,
        CancellationToken cancellationToken = default);
}
