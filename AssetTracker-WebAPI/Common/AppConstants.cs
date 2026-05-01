namespace AssetTracker_WebAPI.Common;

/// <summary>
/// Contains global configuration constants for the application.
/// </summary>
public static class AppConstants
{
    public const string DefaultCurrency = "EUR";
    public const string DefaultPortfolioName = "Main Portfolio";
    public const string DefaultTransactionType = "Buy";

    /// <summary>
    /// Contains names of the external API providers.
    /// </summary>
    public static class ApiNames
    {
        public const string Finnhub = "Finnhub";
        public const string CoinGecko = "CoinGecko";
        public const string NewsApi = "NewsApi";
    }

    /// <summary>
    /// Contains the specific endpoint paths and query string formats for external APIs.
    /// </summary>
    public static class ExternalEndpoints
    {
        public const string FinnhubQuote = "quote?symbol={0}&token={1}";
        public const string CoinGeckoPrice = "simple/price?ids={0}&vs_currencies={1}&include_24hr_change=true";
        public const string NewsApiSearch = "everything?q={0}&sortBy=publishedAt&language=en&apiKey={1}";
    }

    /// <summary>
    /// Contains standard HTTP header keys and values used in external requests.
    /// </summary>
    public static class HttpHeaders
    {
        public const string CoinGeckoApiKey = "x-cg-demo-api-key";
        public const string UserAgent = "User-Agent";
        public const string DefaultUserAgentValue = "AssetTracker/1.0";
    }

    /// <summary>
    /// Security and Authentication constants.
    /// </summary>
    public static class Security
    {
        public const string BearerDefinition = "Bearer";
        public const string BearerScheme = "bearer";
        public const string BearerFormat = "JWT";
    }
}