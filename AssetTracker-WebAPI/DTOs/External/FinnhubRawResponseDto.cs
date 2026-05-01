using System.Text.Json.Serialization;

namespace AssetTracker_WebAPI.DTOs.External;

/// <summary>
/// Internal DTO used strictly for deserializing Finnhub's specific JSON format.
/// </summary>
public class FinnhubRawResponseDto
{
    [JsonPropertyName("c")]
    public decimal CurrentPrice { get; set; }

    [JsonPropertyName("d")]
    public decimal Change { get; set; }

    [JsonPropertyName("dp")]
    public decimal PercentChange { get; set; }
}