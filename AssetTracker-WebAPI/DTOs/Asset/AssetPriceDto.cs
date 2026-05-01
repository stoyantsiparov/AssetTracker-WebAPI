namespace AssetTracker_WebAPI.DTOs.Asset;

/// <summary>
/// Data Transfer Object representing the clean, readable price data for any asset (Stock or Crypto).
/// </summary>
public class AssetPriceDto
{
    /// <summary>
    /// Gets or sets the current market price of the asset.
    /// </summary>
    public decimal CurrentPrice { get; set; }

    /// <summary>
    /// Gets or sets the absolute price change.
    /// </summary>
    public decimal Change { get; set; }

    /// <summary>
    /// Gets or sets the percentage price change.
    /// </summary>
    public decimal PercentChange { get; set; }
}