using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicDistribution.Application.DTOs.TrackDistributions;
using MusicDistribution.Application.DTOs.Tracks;
using MusicDistribution.Application.Interfaces;

namespace MusicDistribution.API.Controllers;

[ApiController]
[Route("api/tracks")]
public class TracksController(ITrackService trackService, ITrackDistributionService distributionService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrackDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TrackDto>>> GetTracks(
        [FromQuery] TrackFilterDto filter,
        CancellationToken cancellationToken)
    {
        var tracks = await trackService.GetAllAsync(filter, cancellationToken);
        return Ok(tracks);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrackDto>> GetTrack(int id, CancellationToken cancellationToken)
    {
        var track = await trackService.GetByIdAsync(id, cancellationToken);
        return track is null ? NotFound() : Ok(track);
    }

    [HttpGet("{id:int}/distributions")]
    [ProducesResponseType(typeof(IReadOnlyList<TrackDistributionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<TrackDistributionDto>>> GetTrackDistributions(
        int id,
        CancellationToken cancellationToken)
    {
        if (await trackService.GetByIdAsync(id, cancellationToken) is null)
        {
            return NotFound();
        }

        var distributions = await distributionService.GetForTrackAsync(id, cancellationToken);
        return Ok(distributions);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TrackDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrackDto>> CreateTrack(
        [FromBody] TrackCreateDto request,
        CancellationToken cancellationToken)
    {
        var result = await trackService.CreateAsync(request, cancellationToken);
        if (result.Error != TrackMutationError.None)
        {
            return BadRequest(ToErrorResponse(result.Error));
        }

        return CreatedAtAction(nameof(GetTrack), new { id = result.Track!.Id }, result.Track);
    }

    [HttpPost("{id:int}/distribute")]
    [Authorize]
    [ProducesResponseType(typeof(TrackDistributionResult), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(TrackDistributionResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DistributeTrack(
        int id,
        [FromBody] TrackDistributionCreateDto request,
        CancellationToken cancellationToken)
    {
        var result = await trackService.DistributeAsync(id, request, cancellationToken);
        return result.Error switch
        {
            TrackDistributionError.TrackNotFound => NotFound(new { message = "The track does not exist." }),
            TrackDistributionError.DspNotFound => BadRequest(new { message = "One or more DSP IDs do not exist." }),
            _ when result.Submitted.Count == 0 => Ok(result),
            _ => StatusCode(StatusCodes.Status201Created, result)
        };
    }

    [HttpPatch("{id:int}/status")]
    [Authorize]
    [ProducesResponseType(typeof(TrackDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrackDto>> UpdateTrackStatus(
        int id,
        [FromBody] TrackStatusUpdateDto request,
        CancellationToken cancellationToken)
    {
        var track = await trackService.UpdateStatusAsync(id, request, cancellationToken);
        return track is null ? NotFound() : Ok(track);
    }

    private static object ToErrorResponse(TrackMutationError error) => error switch
    {
        TrackMutationError.ArtistNotFound => new { message = "The specified artist does not exist." },
        TrackMutationError.IsrcAlreadyExists => new { message = "A track with this ISRC already exists." },
        _ => new { message = "The track request is invalid." }
    };
}
