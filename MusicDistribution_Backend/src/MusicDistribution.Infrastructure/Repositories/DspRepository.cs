using Microsoft.EntityFrameworkCore;
using MusicDistribution.Application.Interfaces;
using MusicDistribution.Domain.Entities;
using MusicDistribution.Infrastructure.Data;

namespace MusicDistribution.Infrastructure.Repositories;

public class DspRepository(AppDbContext dbContext) : IDspRepository
{
    public async Task<IReadOnlyList<Dsp>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Dsps.AsNoTracking()
            .OrderBy(dsp => dsp.Name)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Dsp>> GetDspsByIdsAsync(
        IReadOnlyCollection<int> dspIds,
        CancellationToken cancellationToken = default) =>
        await dbContext.Dsps.AsNoTracking()
            .Where(dsp => dspIds.Contains(dsp.Id))
            .ToListAsync(cancellationToken);
}
