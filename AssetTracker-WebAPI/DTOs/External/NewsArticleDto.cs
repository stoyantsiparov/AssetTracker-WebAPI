using System.Text.Json.Serialization;

namespace AssetTracker_WebAPI.DTOs.External;

/// <summary>
/// Represents a single news article retrieved from an external API.
/// </summary>
public class NewsArticleDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public string Url { get; set; } = string.Empty;

    [JsonPropertyName("publishedAt")]
    public DateTime PublishedAt { get; set; }
}