using Microsoft.AspNetCore.Mvc;
using MusicDistribution.Application.DTOs.Dsps;
using MusicDistribution.Application.Interfaces;

namespace MusicDistribution.API.Controllers;

[ApiController]
[Route("api/dsps")]
public class DspsController(IDspService dspService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<DspDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<DspDto>>> GetDsps(CancellationToken cancellationToken)
    {
        var dsps = await dspService.GetAllAsync(cancellationToken);
        return Ok(dsps);
    }
}
