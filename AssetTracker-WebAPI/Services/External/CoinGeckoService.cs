using System.Text.Json.Nodes;
using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.Asset;
using AssetTracker_WebAPI.Services.External.Contracts;
using Microsoft.Extensions.Options;

namespace AssetTracker_WebAPI.Services.External;

/// <summary>
/// Implements the business logic for fetching cryptocurrency data from the CoinGecko API.
/// </summary>
public class CoinGeckoService : ICoinGeckoService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _apiSettings;

    public CoinGeckoService(HttpClient httpClient, IOptions<ApiSettings> options)
    {
        _httpClient = httpClient;
        _apiSettings = options.Value;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<AssetPriceDto>> GetCryptoPriceAsync(string cryptoId)
    {
        var response = new ApiResponse<AssetPriceDto>();

        try
        {
            string targetCurrency = AppConstants.DefaultCurrency.ToLower();
            string endpoint = string.Format(AppConstants.ExternalEndpoints.CoinGeckoPrice, cryptoId.ToLower(), targetCurrency);
            string url = $"{_apiSettings.CoinGecko.BaseUrl}{endpoint}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add(AppConstants.HttpHeaders.CoinGeckoApiKey, _apiSettings.CoinGecko.ApiKey);

            var httpResponse = await _httpClient.SendAsync(request);

            if (!httpResponse.IsSuccessStatusCode)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.ExternalApiFetchError, AppConstants.ApiNames.CoinGecko, httpResponse.StatusCode);
                return response;
            }

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            var jsonNode = JsonNode.Parse(jsonString);
            var rootNode = jsonNode?[cryptoId.ToLower()];

            if (rootNode == null)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.ExternalApiInvalidData, cryptoId, AppConstants.ApiNames.CoinGecko);
                return response;
            }

            string changeKey = $"{targetCurrency}_24h_change";

            // Map directly to the clean, unified DTO
            response.Data = new AssetPriceDto
            {
                CurrentPrice = rootNode[targetCurrency]?.GetValue<decimal>() ?? 0,
                Change = 0, // Absolute change is not provided natively by this endpoint
                PercentChange = rootNode[changeKey]?.GetValue<decimal>() ?? 0
            };

            response.Success = true;
            response.Message = string.Format(AppMessages.ExternalApiDataRetrieved, cryptoId, AppConstants.ApiNames.CoinGecko);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = string.Format(AppMessages.ExternalApiCommunicationError, AppConstants.ApiNames.CoinGecko);
            response.Errors.Add(ex.Message);
        }

        return response;
    }
}