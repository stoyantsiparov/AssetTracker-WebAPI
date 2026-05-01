namespace AssetTracker_WebAPI.DTOs.Auth;

/// <summary>
/// Response object returning the JWT token or authentication errors.
/// </summary>
public class AuthResponseDto
{
    public bool Success { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
}