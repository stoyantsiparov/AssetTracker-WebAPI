using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.Data;
using AssetTracker_WebAPI.DTOs.Asset;
using AssetTracker_WebAPI.Services.Asset.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AssetTracker_WebAPI.Services.Asset;

/// <summary>
/// Implements the business logic for managing assets.
/// </summary>
public class AssetService : IAssetService
{
    private readonly AssetTrackerDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssetService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public AssetService(AssetTrackerDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<List<AssetDto>>> GetAllAssetsAsync()
    {
        var response = new ApiResponse<List<AssetDto>>();

        try
        {
            var assets = await _context.Assets
                .Select(a => new AssetDto
                {
                    Id = a.Id,
                    Ticker = a.Ticker,
                    Name = a.Name,
                    AssetType = a.AssetType
                })
                .ToListAsync();

            response.Data = assets;
            response.Success = true;
            response.Message = AppMessages.AssetsRetrievedSuccessfully;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorRetrievingAssets;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<AssetDto>> GetAssetByIdAsync(int id)
    {
        var response = new ApiResponse<AssetDto>();

        try
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.AssetNotFound, id);
                return response;
            }

            response.Data = new AssetDto
            {
                Id = asset.Id,
                Ticker = asset.Ticker,
                Name = asset.Name,
                AssetType = asset.AssetType
            };
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorRetrievingAssets;
            response.Errors.Add(ex.Message);
        }

        return response;
    }

    /// <inheritdoc />
    public async Task<ApiResponse<AssetDto>> CreateAssetAsync(CreateAssetDto createAssetDto)
    {
        var response = new ApiResponse<AssetDto>();

        try
        {
            bool tickerExists = await _context.Assets.AnyAsync(a => a.Ticker == createAssetDto.Ticker);
            if (tickerExists)
            {
                response.Success = false;
                response.Message = string.Format(AppMessages.AssetAlreadyExists, createAssetDto.Ticker);
                return response;
            }

            var asset = new AssetTracker_WebAPI.Data.Models.Asset
            {
                Ticker = createAssetDto.Ticker,
                Name = createAssetDto.Name,
                AssetType = createAssetDto.AssetType
            };

            await _context.Assets.AddAsync(asset);
            await _context.SaveChangesAsync();

            response.Data = new AssetDto
            {
                Id = asset.Id,
                Ticker = asset.Ticker,
                Name = asset.Name,
                AssetType = asset.AssetType
            };
            response.Success = true;
            response.Message = AppMessages.AssetCreatedSuccessfully;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = AppMessages.ErrorCreatingAsset;
            response.Errors.Add(ex.Message);
        }

        return response;
    }
}