using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.Asset;

namespace AssetTracker_WebAPI.Services.External.Contracts;

/// <summary>
/// Defines the contract for interacting with the CoinGecko external API.
/// </summary>
public interface ICoinGeckoService
{
    /// <summary>
    /// Retrieves the real-time price and 24h change for a specific cryptocurrency.
    /// </summary>
    /// <param name="cryptoId">The CoinGecko ID of the cryptocurrency (e.g., bitcoin).</param>
    /// <returns>A task containing the API response with the price data.</returns>
    Task<ApiResponse<AssetPriceDto>> GetCryptoPriceAsync(string cryptoId);
}