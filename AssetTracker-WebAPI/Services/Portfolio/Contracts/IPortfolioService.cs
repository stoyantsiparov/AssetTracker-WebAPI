using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.Portfolio;

namespace AssetTracker_WebAPI.Services.Portfolio.Contracts;

/// <summary>
/// Defines the contract for managing portfolio-related business logic.
/// </summary>
public interface IPortfolioService
{
    /// <summary>
    /// Retrieves a specific portfolio by its identifier, including its transactions.
    /// </summary>
    /// <param name="id">The unique identifier of the portfolio.</param>
    /// <returns>A task containing the API response with the portfolio data.</returns>
    Task<ApiResponse<PortfolioDto>> GetPortfolioByIdAsync(int id);

    /// <summary>
    /// Retrieves all portfolios belonging to a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A task containing the API response with a list of portfolios.</returns>
    Task<ApiResponse<IEnumerable<PortfolioDto>>> GetUserPortfoliosAsync(string userId);

    /// <summary>
    /// Creates a new portfolio in the system.
    /// </summary>
    /// <param name="createPortfolioDto">The data required to create the portfolio.</param>
    /// <param name="userId">The ID of the user creating the portfolio.</param>
    /// <returns>A task containing the API response with the created portfolio.</returns>
    Task<ApiResponse<PortfolioDto>> CreatePortfolioAsync(CreatePortfolioDto createPortfolioDto, string userId);

    /// <summary>
    /// Updates the details of an existing portfolio.
    /// </summary>
    /// <param name="id">The ID of the portfolio to update.</param>
    /// <param name="updatePortfolioDto">The updated portfolio data.</param>
    /// <param name="userId">The ID of the user requesting the update.</param>
    /// <returns>A task containing the API response with the updated portfolio data.</returns>
    Task<ApiResponse<PortfolioDto>> UpdatePortfolioAsync(int id, UpdatePortfolioDto updatePortfolioDto, string userId);

    /// <summary>
    /// Deletes an existing portfolio and its associated transactions.
    /// </summary>
    /// <param name="id">The ID of the portfolio to delete.</param>
    /// <param name="userId">The ID of the user requesting the deletion.</param>
    /// <returns>A task containing the API response indicating the success of the operation.</returns>
    Task<ApiResponse<bool>> DeletePortfolioAsync(int id, string userId);
}