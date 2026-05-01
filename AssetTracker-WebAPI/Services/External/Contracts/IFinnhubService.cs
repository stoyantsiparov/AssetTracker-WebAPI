using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.Asset;

namespace AssetTracker_WebAPI.Services.External.Contracts;

/// <summary>
/// Defines the contract for interacting with the Finnhub external API.
/// </summary>
public interface IFinnhubService
{
    /// <summary>
    /// Retrieves the real-time quote (price) for a specific stock ticker.
    /// </summary>
    /// <param name="ticker">The stock ticker symbol (e.g., AAPL).</param>
    /// <returns>A task containing the API response with the price data.</returns>
    Task<ApiResponse<AssetPriceDto>> GetStockPriceAsync(string ticker);
}