using AssetTracker_WebAPI.DTOs.Auth;

namespace AssetTracker_WebAPI.Services.Auth.Contracts;

/// <summary>
/// Defines the contract for user authentication and authorization operations.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);

    /// <summary>
    /// Authenticates a user and generates a JWT token.
    /// </summary>
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}