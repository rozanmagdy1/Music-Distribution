using MusicDistribution.Application.DTOs.TrackDistributions;
using MusicDistribution.Application.DTOs.Tracks;
using MusicDistribution.Application.Interfaces;
using MusicDistribution.Domain.Entities;

namespace MusicDistribution.Application.Services;

public class TrackService(
    ITrackRepository trackRepository,
    IDspService dspService,
    ITrackDistributionService trackDistributionService) : ITrackService
{
    public async Task<IReadOnlyList<TrackDto>> GetAllAsync(TrackFilterDto filter, CancellationToken cancellationToken = default)
    {
        var genre = string.IsNullOrWhiteSpace(filter.Genre) ? null : filter.Genre.Trim();
        var tracks = await trackRepository.GetAllAsync(filter.ArtistId, genre, filter.Status, cancellationToken);
        return tracks.Select(ToDto).ToArray();
    }

    public async Task<TrackDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var track = await trackRepository.GetByIdAsync(id, cancellationToken);
        return track is null ? null : ToDto(track);
    }

    public async Task<TrackMutationResult> CreateAsync(TrackCreateDto request, CancellationToken cancellationToken = default)
    {
        if (!await trackRepository.ArtistExistsAsync(request.ArtistId, cancellationToken))
        {
            return new TrackMutationResult(null, TrackMutationError.ArtistNotFound);
        }

        if (await trackRepository.IsrcExistsAsync(request.ISRC, cancellationToken: cancellationToken))
        {
            return new TrackMutationResult(null, TrackMutationError.IsrcAlreadyExists);
        }

        var track = new Track
        {
            Title = request.Title.Trim(),
            ArtistId = request.ArtistId,
            ISRC = request.ISRC,
            ReleaseDate = request.ReleaseDate!.Value,
            Genre = request.Genre.Trim(),
            Status = request.Status!.Value
        };

        var createdTrack = await trackRepository.AddAsync(track, cancellationToken);
        return new TrackMutationResult(ToDto(createdTrack), TrackMutationError.None);
    }

    public async Task<TrackDistributionResult> DistributeAsync(
        int trackId,
        TrackDistributionCreateDto request,
        CancellationToken cancellationToken = default)
    {
        if (await trackRepository.GetByIdAsync(trackId, cancellationToken) is null)
        {
            return new TrackDistributionResult([], [], TrackDistributionError.TrackNotFound);
        }

        var dspIds = request.DspIds.Distinct().ToArray();
        var dsps = await dspService.GetDspsByIdsAsync(dspIds, cancellationToken);
        if (dsps.Count != dspIds.Length)
        {
            return new TrackDistributionResult([], [], TrackDistributionError.DspNotFound);
        }

        return await trackDistributionService.DistributeAsync(trackId, dspIds, cancellationToken);
    }

    public async Task<TrackDto?> UpdateStatusAsync(
        int id,
        TrackStatusUpdateDto request,
        CancellationToken cancellationToken = default)
    {
        var track = await trackRepository.GetByIdAsync(id, cancellationToken);
        if (track is null)
        {
            return null;
        }

        track.Status = request.Status!.Value;
        await trackRepository.UpdateAsync(track);
        return ToDto(track);
    }

    private static TrackDto ToDto(Track track) => new()
    {
        Id = track.Id,
        Title = track.Title,
        ArtistId = track.ArtistId,
        ISRC = track.ISRC,
        ReleaseDate = track.ReleaseDate,
        Genre = track.Genre,
        Status = track.Status
    };

}
