using AssetTracker_WebAPI.Common;

namespace AssetTracker_WebAPI.Data.Models;

/// <summary>
/// Represents a user's investment portfolio containing their transactions.
/// </summary>
public class Portfolio
{
    /// <summary>
    /// The unique identifier of the portfolio.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The identifier of the user who owns the portfolio (linked to JWT authentication).
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// The name of the portfolio.
    /// </summary>
    public string Name { get; set; } = AppConstants.DefaultPortfolioName;

    /// <summary>
    /// A collection of all transactions (buys/sells) made within this portfolio.
    /// </summary>
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}