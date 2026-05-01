using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.External;

namespace AssetTracker_WebAPI.Services.External.Contracts;

/// <summary>
/// Defines the contract for interacting with the NewsAPI service.
/// </summary>
public interface INewsApiService
{
    /// <summary>
    /// Retrieves recent financial news articles related to a specific keyword or asset.
    /// </summary>
    /// <param name="keyword">The search keyword (e.g., AAPL, Bitcoin).</param>
    /// <returns>A task containing the API response with a list of news articles.</returns>
    Task<ApiResponse<List<NewsArticleDto>>> GetNewsForAssetAsync(string keyword);
}