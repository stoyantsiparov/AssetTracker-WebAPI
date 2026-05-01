using System.Text.Json.Serialization;

namespace AssetTracker_WebAPI.DTOs.External;

/// <summary>
/// Represents the root response object from the NewsAPI service.
/// </summary>
public class NewsResponseDto
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("articles")]
    public List<NewsArticleDto> Articles { get; set; } = new();
}