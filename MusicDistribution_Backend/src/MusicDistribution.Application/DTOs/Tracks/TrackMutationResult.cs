namespace MusicDistribution.Application.DTOs.Tracks;

public enum TrackMutationError
{
    None,
    ArtistNotFound,
    IsrcAlreadyExists
}

public sealed record TrackMutationResult(TrackDto? Track, TrackMutationError Error);
