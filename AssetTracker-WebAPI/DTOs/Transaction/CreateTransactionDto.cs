using AssetTracker_WebAPI.Common;

namespace AssetTracker_WebAPI.DTOs.Transaction;

/// <summary>
/// Represents the data transfer object for creating a new transaction.
/// </summary>
public class CreateTransactionDto
{
    /// <summary>
    /// The identifier of the asset being transacted.
    /// </summary>
    public int AssetId { get; set; }

    /// <summary>
    /// The quantity of the asset to buy or sell.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// The price per unit of the asset at the time of the transaction.
    /// </summary>
    public decimal PricePerUnit { get; set; }

    /// <summary>
    /// The type of the transaction.
    /// </summary>
    public string TransactionType { get; set; } = AppConstants.DefaultTransactionType;
}