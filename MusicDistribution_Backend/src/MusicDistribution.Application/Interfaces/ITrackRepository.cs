using MusicDistribution.Domain.Entities;
using MusicDistribution.Domain.Enums;

namespace MusicDistribution.Application.Interfaces;

public interface ITrackRepository
{
    Task<IReadOnlyList<Track>> GetAllAsync(int? artistId, string? genre, TrackStatus? status, CancellationToken cancellationToken = default);
    Task<Track?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ArtistExistsAsync(int artistId, CancellationToken cancellationToken = default);
    Task<bool> IsrcExistsAsync(string isrc, int? excludingTrackId = null, CancellationToken cancellationToken = default);
    Task<Track> AddAsync(Track track, CancellationToken cancellationToken = default);
    Task<Track> UpdateAsync(Track track, CancellationToken cancellationToken = default);
}
