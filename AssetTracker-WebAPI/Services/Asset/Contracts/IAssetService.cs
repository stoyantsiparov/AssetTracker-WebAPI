using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.Asset;

namespace AssetTracker_WebAPI.Services.Asset.Contracts;

/// <summary>
/// Defines the contract for managing asset-related business logic.
/// </summary>
public interface IAssetService
{
    /// <summary>
    /// Retrieves all available assets.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation, containing the API response with a list of assets.</returns>
    Task<ApiResponse<List<AssetDto>>> GetAllAssetsAsync();

    /// <summary>
    /// Retrieves a specific asset by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the asset.</param>
    /// <returns>A task that represents the asynchronous operation, containing the API response with the asset data.</returns>
    Task<ApiResponse<AssetDto>> GetAssetByIdAsync(int id);

    /// <summary>
    /// Creates a new asset in the system.
    /// </summary>
    /// <param name="createAssetDto">The data required to create the asset.</param>
    /// <returns>A task that represents the asynchronous operation, containing the API response with the created asset.</returns>
    Task<ApiResponse<AssetDto>> CreateAssetAsync(CreateAssetDto createAssetDto);
}