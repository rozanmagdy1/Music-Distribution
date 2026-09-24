using Microsoft.EntityFrameworkCore;
using MusicDistribution.Application.Interfaces;
using MusicDistribution.Domain.Entities;
using MusicDistribution.Infrastructure.Data;

namespace MusicDistribution.Infrastructure.Repositories;

public class ArtistRepository(AppDbContext dbContext) : IArtistRepository
{
    public async Task<IReadOnlyList<Artist>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await dbContext.Artists.AsNoTracking()
            .OrderBy(artist => artist.Name)
            .ToListAsync(cancellationToken);

    public async Task<Artist> AddAsync(Artist artist, CancellationToken cancellationToken = default)
    {
        dbContext.Artists.Add(artist);
        await dbContext.SaveChangesAsync(cancellationToken);
        return artist;
    }
}
