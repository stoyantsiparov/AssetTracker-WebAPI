namespace AssetTracker_WebAPI.DTOs.Asset;

/// <summary>
/// Represents the data transfer object for creating a new asset.
/// </summary>
public class CreateAssetDto
{
    /// <summary>
    /// The ticker symbol of the new asset.
    /// </summary>
    public string Ticker { get; set; } = string.Empty;

    /// <summary>
    /// The full name of the new asset.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The type of the new asset.
    /// </summary>
    public string AssetType { get; set; } = string.Empty;
}