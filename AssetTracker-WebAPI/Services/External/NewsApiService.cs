using System.Text.Json;
using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.External;
using AssetTracker_WebAPI.Services.External.Contracts;
using Microsoft.Extensions.Options;

namespace AssetTracker_WebAPI.Services.External;

/// <summary>
/// Implements the business logic for fetching financial news from the NewsAPI.
/// </summary>
public class NewsApiService : INewsApiService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _apiSettings;

    public NewsApiService(HttpClient httpClient, IOptions<ApiSettings> options)
    {
        _httpClient = httpClient;
        _apiSettings = options.Value;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<List<NewsArticleDto>>> GetNewsForAssetAsync(string keyword)
    {
        var response = new ApiResponse<List<NewsArticleDto>>();

        try
        {
            string endpoint = string.Format(AppConstants.ExternalEndpoints.NewsApiSearch, keyword, _apiSettings.NewsApi.ApiKey);
            string url = $"{_apiSettings.NewsApi.BaseUrl}{endpoint}";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add(AppConstants.HttpHeaders.UserAgent, AppConstants.HttpHeaders.DefaultUserAgentValue);

            var httpResponse = await _httpClient.SendAsync(request);

            if (!httpResponse.IsSuccessStatusCode)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.ExternalApiFetchError, AppConstants.ApiNames.NewsApi, httpResponse.StatusCode);
                return response;
            }

            string jsonString = await httpResponse.Content.ReadAsStringAsync();
            var newsData = JsonSerializer.Deserialize<NewsResponseDto>(jsonString);

            if (newsData == null || newsData.Articles == null || !newsData.Articles.Any())
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.ExternalApiInvalidData, keyword, AppConstants.ApiNames.NewsApi);
                return response;
            }

            // Return only the top 5 articles to avoid overwhelming the client
            response.Data = newsData.Articles.Take(5).ToList();
            response.Success = true;
            response.Message = string.Format(AppMessages.ExternalApiDataRetrieved, keyword, AppConstants.ApiNames.NewsApi);
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = string.Format(AppMessages.ExternalApiCommunicationError, AppConstants.ApiNames.NewsApi);
            response.Errors.Add(ex.Message);
        }

        return response;
    }
}