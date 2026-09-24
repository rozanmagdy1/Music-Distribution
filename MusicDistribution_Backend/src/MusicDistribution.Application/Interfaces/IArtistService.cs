using MusicDistribution.Application.DTOs;

namespace MusicDistribution.Application.Interfaces;

public interface IArtistService
{
    Task<IReadOnlyList<ArtistDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ArtistDto> CreateAsync(ArtistCreateDto request, CancellationToken cancellationToken = default);
}
