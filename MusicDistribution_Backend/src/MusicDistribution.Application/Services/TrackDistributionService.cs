using MusicDistribution.Application.DTOs.TrackDistributions;
using MusicDistribution.Application.Interfaces;
using MusicDistribution.Domain.Entities;
using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Application.Services;

public class TrackDistributionService(
    ITrackDistributionRepository trackDistributionRepository) : ITrackDistributionService
{
    public async Task<TrackDistributionResult> DistributeAsync(
        int trackId,
        IReadOnlyCollection<int> requestedDspIds,
        CancellationToken cancellationToken = default)
    {
        var dspIds = requestedDspIds.Distinct().ToArray();
        var existing = await trackDistributionRepository.GetDistributionsAsync(trackId, dspIds, cancellationToken);
        var existingDspIds = existing.Select(distribution => distribution.DspId).ToHashSet();
        var newDistributions = dspIds
            .Where(dspId => !existingDspIds.Contains(dspId))
            .Select(dspId => new TrackDistribution
            {
                TrackId = trackId,
                DspId = dspId,
                SubmittedAt = DateTimeOffset.UtcNow,
                Status = DistributionStatus.Pending
            })
            .ToArray();

        var submitted = newDistributions.Length == 0
            ? Array.Empty<TrackDistribution>()
            : await trackDistributionRepository.AddDistributionsAsync(newDistributions, cancellationToken);

        return new TrackDistributionResult(
            submitted.Select(ToDto).ToArray(),
            existingDspIds.Order().ToArray(),
            TrackDistributionError.None);
    }

    private static TrackDistributionDto ToDto(TrackDistribution distribution) => new()
    {
        Id = distribution.Id,
        TrackId = distribution.TrackId,
        DspId = distribution.DspId,
        SubmittedAt = distribution.SubmittedAt,
        Status = distribution.Status
    };
}
