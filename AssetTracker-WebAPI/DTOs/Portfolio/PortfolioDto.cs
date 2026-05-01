using AssetTracker_WebAPI.DTOs.Transaction;

namespace AssetTracker_WebAPI.DTOs.Portfolio;

/// <summary>
/// Represents the data transfer object for displaying a user's portfolio.
/// </summary>
public class PortfolioDto
{
    /// <summary>
    /// The unique identifier of the portfolio.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The identifier of the user who owns the portfolio.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// The name of the portfolio.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A list of transactions associated with this portfolio.
    /// </summary>
    public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
}