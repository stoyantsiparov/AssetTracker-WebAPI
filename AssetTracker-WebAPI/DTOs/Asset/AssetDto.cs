namespace AssetTracker_WebAPI.DTOs.Asset;

/// <summary>
/// Represents the data transfer object for displaying an asset.
/// </summary>
public class AssetDto
{
    /// <summary>
    /// The unique identifier of the asset.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The ticker symbol of the asset (e.g., AAPL, BTC).
    /// </summary>
    public string Ticker { get; set; } = string.Empty;

    /// <summary>
    /// The full name of the asset (e.g., Apple Inc., Bitcoin).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The type of the asset (e.g., Stock, Crypto).
    /// </summary>
    public string AssetType { get; set; } = string.Empty;
}