using Microsoft.EntityFrameworkCore;
using MusicDistribution.Application.Interfaces;
using MusicDistribution.Domain.Entities;
using MusicDistribution.Infrastructure.Data;

namespace MusicDistribution.Infrastructure.Repositories;

public class TrackDistributionRepository(AppDbContext dbContext) : ITrackDistributionRepository
{
    public async Task<IReadOnlyList<TrackDistribution>> GetByTrackIdAsync(
        int trackId,
        CancellationToken cancellationToken = default) =>
        await dbContext.TrackDistributions.AsNoTracking()
            .Where(distribution => distribution.TrackId == trackId)
            .OrderBy(distribution => distribution.DspId)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<TrackDistribution>> GetDistributionsAsync(
        int trackId,
        IReadOnlyCollection<int> dspIds,
        CancellationToken cancellationToken = default) =>
        await dbContext.TrackDistributions.AsNoTracking()
            .Where(distribution => distribution.TrackId == trackId && dspIds.Contains(distribution.DspId))
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<TrackDistribution>> AddDistributionsAsync(
        IReadOnlyCollection<TrackDistribution> distributions,
        CancellationToken cancellationToken = default)
    {
        dbContext.TrackDistributions.AddRange(distributions);
        await dbContext.SaveChangesAsync(cancellationToken);
        return distributions.ToArray();
    }
}
