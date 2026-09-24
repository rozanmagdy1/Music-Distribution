using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Application.Interfaces;

public interface ITrackDistributionRepository
{
    Task<IReadOnlyList<TrackDistribution>> GetDistributionsAsync(
        int trackId,
        IReadOnlyCollection<int> dspIds,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TrackDistribution>> AddDistributionsAsync(
        IReadOnlyCollection<TrackDistribution> distributions,
        CancellationToken cancellationToken = default);
}
