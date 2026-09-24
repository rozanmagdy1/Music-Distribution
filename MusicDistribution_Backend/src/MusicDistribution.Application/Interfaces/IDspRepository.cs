using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Application.Interfaces;

public interface IDspRepository
{
    Task<IReadOnlyList<Dsp>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Dsp>> GetDspsByIdsAsync(
        IReadOnlyCollection<int> dspIds,
        CancellationToken cancellationToken = default);
}
