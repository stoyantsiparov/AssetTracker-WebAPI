using AssetTracker_WebAPI.DTOs.Asset;
using AssetTracker_WebAPI.Services.Asset.Contracts;
using AssetTracker_WebAPI.Services.External.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker_WebAPI.Controllers;

[ApiController]
[Route("api/assets")]
public class AssetController : ControllerBase
{
    private readonly IAssetService _assetService;
    private readonly IFinnhubService _finnhubService;
    private readonly ICoinGeckoService _coinGeckoService;
    private readonly INewsApiService _newsApiService;

    public AssetController(
        IAssetService assetService,
        IFinnhubService finnhubService,
        ICoinGeckoService coinGeckoService,
        INewsApiService newsApiService)
    {
        _assetService = assetService;
        _finnhubService = finnhubService;
        _coinGeckoService = coinGeckoService;
        _newsApiService = newsApiService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAssets()
    {
        var response = await _assetService.GetAllAssetsAsync();
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssetById(int id)
    {
        var response = await _assetService.GetAssetByIdAsync(id);
        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsset([FromBody] CreateAssetDto createAssetDto)
    {
        var response = await _assetService.CreateAssetAsync(createAssetDto);
        if (!response.Success) return BadRequest(response);

        return CreatedAtAction(nameof(GetAssetById), new { id = response.Data?.Id }, response);
    }

    [HttpGet("stock/{ticker}/price")]
    public async Task<IActionResult> GetStockPrice(string ticker)
    {
        var response = await _finnhubService.GetStockPriceAsync(ticker);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("crypto/{cryptoId}/price")]
    public async Task<IActionResult> GetCryptoPrice(string cryptoId)
    {
        var response = await _coinGeckoService.GetCryptoPriceAsync(cryptoId);
        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{keyword}/news")]
    public async Task<IActionResult> GetAssetNews(string keyword)
    {
        var response = await _newsApiService.GetNewsForAssetAsync(keyword);
        return response.Success ? Ok(response) : BadRequest(response);
    }
}