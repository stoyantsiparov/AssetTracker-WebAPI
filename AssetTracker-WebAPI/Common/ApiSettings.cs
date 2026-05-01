namespace AssetTracker_WebAPI.Common;

/// <summary>
/// Represents the root configuration section for all external APIs.
/// </summary>
public class ApiSettings
{
    /// <summary>
    /// Configuration settings for the Finnhub API (Stocks).
    /// </summary>
    public ApiConfig Finnhub { get; set; } = new ApiConfig();

    /// <summary>
    /// Configuration settings for the CoinGecko API (Cryptocurrencies).
    /// </summary>
    public ApiConfig CoinGecko { get; set; } = new ApiConfig();

    /// <summary>
    /// Configuration settings for the News API (Financial News).
    /// </summary>
    public ApiConfig NewsApi { get; set; } = new ApiConfig();
}

/// <summary>
/// Represents the connection details for a single external API service.
/// </summary>
public class ApiConfig
{
    /// <summary>
    /// The base URL of the API.
    /// </summary>
    public string BaseUrl { get; set; } = string.Empty;

    /// <summary>
    /// The secret key used for API authentication.
    /// </summary>
    public string ApiKey { get; set; } = string.Empty;
}