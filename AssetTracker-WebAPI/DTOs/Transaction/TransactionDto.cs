namespace AssetTracker_WebAPI.DTOs.Transaction;

/// <summary>
/// Represents the data transfer object for displaying a transaction.
/// </summary>
public class TransactionDto
{
    /// <summary>
    /// The unique identifier of the transaction.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The identifier of the associated asset.
    /// </summary>
    public int AssetId { get; set; }

    /// <summary>
    /// The ticker symbol of the associated asset.
    /// </summary>
    public string AssetTicker { get; set; } = string.Empty;

    /// <summary>
    /// The quantity of the asset bought or sold.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// The price per unit of the asset at the time of the transaction.
    /// </summary>
    public decimal PricePerUnit { get; set; }

    /// <summary>
    /// The date and time when the transaction occurred.
    /// </summary>
    public DateTime TransactionDate { get; set; }

    /// <summary>
    /// The type of the transaction (e.g., "Buy" or "Sell").
    /// </summary>
    public string TransactionType { get; set; } = string.Empty;
}