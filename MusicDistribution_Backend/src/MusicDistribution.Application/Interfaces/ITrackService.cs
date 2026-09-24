using MusicDistribution.Application.DTOs.TrackDistributions;
using MusicDistribution.Application.DTOs.Tracks;

namespace MusicDistribution.Application.Interfaces;

public interface ITrackService
{
    Task<IReadOnlyList<TrackDto>> GetAllAsync(TrackFilterDto filter, CancellationToken cancellationToken = default);
    Task<TrackDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TrackMutationResult> CreateAsync(TrackCreateDto request, CancellationToken cancellationToken = default);
    Task<TrackDistributionResult> DistributeAsync(int trackId, TrackDistributionCreateDto request, CancellationToken cancellationToken = default);
    Task<TrackDto?> UpdateStatusAsync(int id, TrackStatusUpdateDto request, CancellationToken cancellationToken = default);
}
