using MusicDistribution.Application.Interfaces;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Application.Services;

public class DspService(IDspRepository dspRepository) : IDspService
{
    public Task<IReadOnlyList<Dsp>> GetDspsByIdsAsync(
        IReadOnlyCollection<int> dspIds,
        CancellationToken cancellationToken = default) =>
        dspRepository.GetDspsByIdsAsync(dspIds, cancellationToken);
}
