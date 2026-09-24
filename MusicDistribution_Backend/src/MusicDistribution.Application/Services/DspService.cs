using MusicDistribution.Application.Interfaces;
using MusicDistribution.Application.DTOs.Dsps;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Application.Services;

public class DspService(IDspRepository dspRepository) : IDspService
{
    public async Task<IReadOnlyList<DspDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var dsps = await dspRepository.GetAllAsync(cancellationToken);
        return dsps.Select(dsp => new DspDto(dsp.Id, dsp.Name)).ToArray();
    }

    public Task<IReadOnlyList<Dsp>> GetDspsByIdsAsync(
        IReadOnlyCollection<int> dspIds,
        CancellationToken cancellationToken = default) =>
        dspRepository.GetDspsByIdsAsync(dspIds, cancellationToken);
}
