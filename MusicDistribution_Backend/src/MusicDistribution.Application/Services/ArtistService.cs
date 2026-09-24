using MusicDistribution.Application.DTOs;
using MusicDistribution.Application.Interfaces;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Application.Services;

public class ArtistService(IArtistRepository artistRepository) : IArtistService
{
    public async Task<IReadOnlyList<ArtistDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var artists = await artistRepository.GetAllAsync(cancellationToken);
        return artists.Select(ToDto).ToArray();
    }

    public async Task<ArtistDto> CreateAsync(ArtistCreateDto request, CancellationToken cancellationToken = default)
    {
        var artist = new Artist
        {
            Name = request.Name.Trim(),
            Email = request.Email.Trim(),
            Country = request.Country.Trim()
        };

        var createdArtist = await artistRepository.AddAsync(artist, cancellationToken);
        return ToDto(createdArtist);
    }

    private static ArtistDto ToDto(Artist artist) => new()
    {
        Id = artist.Id,
        Name = artist.Name,
        Email = artist.Email,
        Country = artist.Country
    };
}
