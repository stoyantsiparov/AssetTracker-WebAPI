namespace AssetTracker_WebAPI.Data.Models;

/// <summary>
/// Represents a specific financial asset (e.g., a stock or a cryptocurrency).
/// </summary>
public class Asset
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