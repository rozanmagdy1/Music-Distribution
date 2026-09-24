using MusicDistribution.Application.DTOs.Authentication;

namespace MusicDistribution.Application.Interfaces;

public interface IAuthenticationService
{
    Task<AuthUserDto?> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default);
    Task<AuthTokenDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default);
}
