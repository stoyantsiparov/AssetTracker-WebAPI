namespace AssetTracker_WebAPI.DTOs.Portfolio;

/// <summary>
/// Represents the data transfer object for creating a new portfolio.
/// </summary>
public class CreatePortfolioDto
{
    /// <summary>
    /// The name of the new portfolio.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}