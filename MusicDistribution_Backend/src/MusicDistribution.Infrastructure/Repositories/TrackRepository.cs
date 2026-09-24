using Microsoft.EntityFrameworkCore;
using MusicDistribution.Application.Interfaces;
using MusicDistribution.Domain.Entities;
using MusicDistribution.Domain.Enums;
using MusicDistribution.Infrastructure.Data;

namespace MusicDistribution.Infrastructure.Repositories;

public class TrackRepository(AppDbContext dbContext) : ITrackRepository
{
    public async Task<IReadOnlyList<Track>> GetAllAsync(int? artistId, string? genre, TrackStatus? status, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Tracks.AsNoTracking();
        if (artistId.HasValue)
        {
            query = query.Where(track => track.ArtistId == artistId.Value);
        }
        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(track => track.Genre == genre);
        }
        if (status.HasValue)
        {
            query = query.Where(track => track.Status == status.Value);
        }

        return await query.OrderBy(track => track.Title).ToListAsync(cancellationToken);
    }

    public Task<Track?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        dbContext.Tracks.FirstOrDefaultAsync(track => track.Id == id, cancellationToken);

    public Task<bool> ArtistExistsAsync(int artistId, CancellationToken cancellationToken = default) =>
        dbContext.Artists.AnyAsync(artist => artist.Id == artistId, cancellationToken);

    public Task<bool> IsrcExistsAsync(string isrc, int? excludingTrackId = null, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Tracks.AsQueryable();
        if (excludingTrackId.HasValue)
        {
            query = query.Where(track => track.Id != excludingTrackId.Value);
        }

        return query.AnyAsync(track => track.ISRC == isrc, cancellationToken);
    }

    public async Task<Track> AddAsync(Track track, CancellationToken cancellationToken = default)
    {
        dbContext.Tracks.Add(track);
        await dbContext.SaveChangesAsync(cancellationToken);
        return track;
    }

    public async Task<Track> UpdateAsync(Track track, CancellationToken cancellationToken = default)
    {
        dbContext.Tracks.Update(track);
        await dbContext.SaveChangesAsync(cancellationToken);
        return track;
    }
   
}
