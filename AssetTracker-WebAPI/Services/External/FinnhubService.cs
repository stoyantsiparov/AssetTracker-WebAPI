using System.Text.Json;
using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.Asset;
using AssetTracker_WebAPI.DTOs.External;
using AssetTracker_WebAPI.Services.External.Contracts;
using Microsoft.Extensions.Options;

namespace AssetTracker_WebAPI.Services.External;

/// <summary>
/// Implements the business logic for fetching stock data from the Finnhub API.
/// </summary>
public class FinnhubService : IFinnhubService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _apiSettings;

    public FinnhubService(HttpClient httpClient, IOptions<ApiSettings> options)
    {
        _httpClient = httpClient;
        _apiSettings = options.Value;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<AssetPriceDto>> GetStockPriceAsync(string ticker)
    {
        var response = new ApiResponse<AssetPriceDto>();

        try
        {
            string endpoint = string.Format(AppConstants.ExternalEndpoints.FinnhubQuote, ticker.ToUpper(), _apiSettings.Finnhub.ApiKey);
            string url = $"{_apiSettings.Finnhub.BaseUrl}{endpoint}";

            var httpResponse = await _httpClient.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.ExternalApiFetchError, AppConstants.ApiNames.Finnhub, httpResponse.StatusCode);
                return response;
            }

            string jsonString = await httpResponse.Content.ReadAsStringAsync();

            // 1. Deserialize into the raw internal DTO to map 'c', 'd', 'dp'
            var rawData = JsonSerializer.Deserialize<FinnhubRawResponseDto>(jsonString);

            if (rawData == null || rawData.CurrentPrice == 0)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.ExternalApiInvalidData, ticker, AppConstants.ApiNames.Finnhub);
                return response;
            }

            // 2. Map to the clean, unified DTO for the client
            response.Data = new AssetPriceDto
            {
                CurrentPrice = rawData.CurrentPrice,
                Change = rawData.Change,
                PercentChange = rawData.PercentChange
            };

            response.Success = true;
            response.Message = string.Format(AppMessages.ExternalApiDataRetrieved, ticker, AppConstants.ApiNames.Finnhub);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = string.Format(AppMessages.ExternalApiCommunicationError, AppConstants.ApiNames.Finnhub);
            response.Errors.Add(ex.Message);
        }

        return response;
    }
}