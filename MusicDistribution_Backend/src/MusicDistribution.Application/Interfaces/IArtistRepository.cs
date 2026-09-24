using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Application.Interfaces;

public interface IArtistRepository
{
    Task<IReadOnlyList<Artist>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Artist> AddAsync(Artist artist, CancellationToken cancellationToken = default);
}
