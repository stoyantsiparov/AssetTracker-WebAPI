using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.Transaction;

namespace AssetTracker_WebAPI.Services.Transaction.Contracts;

/// <summary>
/// Defines the contract for managing transaction-related business logic.
/// </summary>
public interface ITransactionService
{
    /// <summary>
    /// Creates a new transaction and adds it to the specified portfolio.
    /// </summary>
    /// <param name="portfolioId">The ID of the portfolio to which the transaction belongs.</param>
    /// <param name="createTransactionDto">The data required to create the transaction.</param>
    /// <returns>A task containing the API response with the created transaction.</returns>
    Task<ApiResponse<TransactionDto>> CreateTransactionAsync(int portfolioId, CreateTransactionDto createTransactionDto);
}