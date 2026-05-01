using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.Data;
using AssetTracker_WebAPI.DTOs.Transaction;
using AssetTracker_WebAPI.Services.Transaction.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker_WebAPI.Services.Transaction;

/// <summary>
/// Implements the business logic for managing transactions.
/// </summary>
public class TransactionService : ITransactionService
{
    private readonly AssetTrackerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public TransactionService(AssetTrackerDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<TransactionDto>> CreateTransactionAsync(int portfolioId, CreateTransactionDto createTransactionDto)
    {
        var response = new ApiResponse<TransactionDto>();

        try
        {
            // 1. Check if the portfolio exists
            var portfolio = await _context.Portfolios.Include(p => p.Transactions).FirstOrDefaultAsync(p => p.Id == portfolioId);
            if (portfolio == null)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.PortfolioNotFound, portfolioId);
                return response;
            }

            // 2. Check if the asset exists
            var asset = await _context.Assets.FindAsync(createTransactionDto.AssetId);
            if (asset == null)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.AssetNotFound, createTransactionDto.AssetId);
                return response;
            }

            // 3. Create the transaction
            var transaction = new Data.Models.Transaction
            {
                AssetId = createTransactionDto.AssetId,
                Quantity = createTransactionDto.Quantity,
                PricePerUnit = createTransactionDto.PricePerUnit,
                TransactionType = createTransactionDto.TransactionType,
                TransactionDate = DateTime.UtcNow // Set current time automatically
            };

            // Add the transaction to the portfolio
            portfolio.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // 4. Map the response
            response.Data = new TransactionDto
            {
                Id = transaction.Id,
                AssetId = transaction.AssetId,
                AssetTicker = asset.Ticker,
                Quantity = transaction.Quantity,
                PricePerUnit = transaction.PricePerUnit,
                TransactionType = transaction.TransactionType,
                TransactionDate = transaction.TransactionDate
            };

            response.Success = true;
            response.Message = AppMessages.TransactionCreatedSuccessfully;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorCreatingTransaction;
            response.Errors.Add(ex.Message);
        }

        return response;
    }
}