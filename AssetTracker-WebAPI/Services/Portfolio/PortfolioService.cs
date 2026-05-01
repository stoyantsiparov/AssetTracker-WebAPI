using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.Data;
using AssetTracker_WebAPI.DTOs.Portfolio;
using AssetTracker_WebAPI.DTOs.Transaction;
using AssetTracker_WebAPI.Services.Portfolio.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker_WebAPI.Services.Portfolio;

/// <summary>
/// Implements the business logic for managing portfolios.
/// </summary>
public class PortfolioService : IPortfolioService
{
    private readonly AssetTrackerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="PortfolioService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public PortfolioService(AssetTrackerDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<PortfolioDto>> GetPortfolioByIdAsync(int id)
    {
        var response = new ApiResponse<PortfolioDto>();

        try
        {
            var portfolio = await _context.Portfolios
                .Include(p => p.Transactions)
                    .ThenInclude(t => t.Asset)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (portfolio == null)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.PortfolioNotFound, id);
                return response;
            }

            response.Data = new PortfolioDto
            {
                Id = portfolio.Id,
                UserId = portfolio.UserId,
                Name = portfolio.Name,
                Transactions = portfolio.Transactions.Select(t => new TransactionDto
                {
                    Id = t.Id,
                    AssetId = t.AssetId,
                    AssetTicker = t.Asset.Ticker,
                    Quantity = t.Quantity,
                    PricePerUnit = t.PricePerUnit,
                    TransactionDate = t.TransactionDate,
                    TransactionType = t.TransactionType
                }).ToList()
            };
            response.Success = true;
            response.Message = AppMessages.PortfoliosRetrievedSuccessfully;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorRetrievingPortfolios;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<IEnumerable<PortfolioDto>>> GetUserPortfoliosAsync(string userId)
    {
        var response = new ApiResponse<IEnumerable<PortfolioDto>>();

        try
        {
            var portfolios = await _context.Portfolios
                .Include(p => p.Transactions)
                .ThenInclude(t => t.Asset)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            var portfolioDtos = portfolios.Select(p => new PortfolioDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Name = p.Name,
                Transactions = p.Transactions.Select(t => new TransactionDto
                {
                    Id = t.Id,
                    AssetId = t.AssetId,
                    AssetTicker = t.Asset.Ticker,
                    Quantity = t.Quantity,
                    PricePerUnit = t.PricePerUnit,
                    TransactionDate = t.TransactionDate,
                    TransactionType = t.TransactionType
                }).ToList()
            }).ToList();

            response.Data = portfolioDtos;
            response.Success = true;
            response.Message = AppMessages.PortfoliosRetrievedSuccessfully;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorRetrievingPortfolios;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<PortfolioDto>> CreatePortfolioAsync(CreatePortfolioDto createPortfolioDto, string userId)
    {
        var response = new ApiResponse<PortfolioDto>();

        try
        {
            var portfolio = new Data.Models.Portfolio
            {
                Name = string.IsNullOrWhiteSpace(createPortfolioDto.Name)
                    ? AppConstants.DefaultPortfolioName
                    : createPortfolioDto.Name,
                UserId = userId
            };

            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();

            response.Data = new PortfolioDto
            {
                Id = portfolio.Id,
                UserId = portfolio.UserId,
                Name = portfolio.Name,
                Transactions = new List<TransactionDto>()
            };
            response.Success = true;
            response.Message = AppMessages.PortfolioCreatedSuccessfully;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorCreatingPortfolio;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<PortfolioDto>> UpdatePortfolioAsync(int id, UpdatePortfolioDto updatePortfolioDto, string userId)
    {
        var response = new ApiResponse<PortfolioDto>();

        try
        {
            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (portfolio == null)
            {
                response.Success = false;
                response.Message = AppMessages.PortfolioNotFoundOrAccessDenied;
                return response;
            }

            portfolio.Name = updatePortfolioDto.Name;

            await _context.SaveChangesAsync();

            response.Data = new PortfolioDto
            {
                Id = portfolio.Id,
                UserId = portfolio.UserId,
                Name = portfolio.Name,
                Transactions = new List<TransactionDto>()
            };

            response.Success = true;
            response.Message = AppMessages.PortfolioUpdatedSuccessfully;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorUpdatingPortfolio;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<bool>> DeletePortfolioAsync(int id, string userId)
    {
        var response = new ApiResponse<bool>();

        try
        {
            var portfolio = await _context.Portfolios
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (portfolio == null)
            {
                response.Success = false;
                response.Message = AppMessages.PortfolioNotFoundOrAccessDenied;
                return response;
            }

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            response.Data = true;
            response.Success = true;
            response.Message = AppMessages.PortfolioDeletedSuccessfully;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorDeletingPortfolio;
            response.Errors.Add(ex.Message);
        }

        return response;
    }
}