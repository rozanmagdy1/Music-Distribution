using Microsoft.AspNetCore.Mvc;
using MusicDistribution.Application.DTOs.Artists;
using MusicDistribution.Application.Interfaces;

namespace MusicDistribution.API.Controllers;

[ApiController]
[Route("api/artists")]
public class ArtistsController(IArtistService artistService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ArtistDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ArtistDto>>> GetArtists(CancellationToken cancellationToken)
    {
        var artists = await artistService.GetAllAsync(cancellationToken);
        return Ok(artists);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ArtistDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ArtistDto>> CreateArtist(
        [FromBody] ArtistCreateDto request,
        CancellationToken cancellationToken)
    {
        var artist = await artistService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetArtists), artist);
    }
}
