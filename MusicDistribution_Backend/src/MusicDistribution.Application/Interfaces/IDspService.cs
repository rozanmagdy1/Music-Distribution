using MusicDistribution.Domain.Entities;
using MusicDistribution.Application.DTOs.Dsps;

namespace MusicDistribution.Application.Interfaces;

public interface IDspService
{
    Task<IReadOnlyList<DspDto>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Dsp>> GetDspsByIdsAsync(
        IReadOnlyCollection<int> dspIds,
        CancellationToken cancellationToken = default);
}
