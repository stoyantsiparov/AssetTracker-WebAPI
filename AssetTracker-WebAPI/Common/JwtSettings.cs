namespace AssetTracker_WebAPI.Common;

/// <summary>
/// Represents the configuration settings for JWT Authentication.
/// </summary>
public class JwtSettings
{
    public string ValidIssuer { get; set; } = string.Empty;
    public string ValidAudience { get; set; } = string.Empty;
    public string Secret { get; set; } = string.Empty;
    public int ExpiryInMinutes { get; set; }
}