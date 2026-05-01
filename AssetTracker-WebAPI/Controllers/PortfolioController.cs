using System.Security.Claims;
using AssetTracker_WebAPI.DTOs.Portfolio;
using AssetTracker_WebAPI.Services.Portfolio.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker_WebAPI.Controllers;

[ApiController]
[Route("api/portfolios")]
[Authorize]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService _portfolioService;

    public PortfolioController(IPortfolioService portfolioService)
    {
        _portfolioService = portfolioService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPortfolioById(int id)
    {
        var response = await _portfolioService.GetPortfolioByIdAsync(id);

        if (!response.Success)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserPortfolios()
    {
        string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized();
        }

        var response = await _portfolioService.GetUserPortfoliosAsync(currentUserId);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePortfolio([FromBody] CreatePortfolioDto createPortfolioDto)
    {
        string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized();
        }

        var response = await _portfolioService.CreatePortfolioAsync(createPortfolioDto, currentUserId);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(GetPortfolioById), new { id = response.Data?.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePortfolio(int id, [FromBody] UpdatePortfolioDto updatePortfolioDto)
    {
        string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized();
        }

        var response = await _portfolioService.UpdatePortfolioAsync(id, updatePortfolioDto, currentUserId);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePortfolio(int id)
    {
        string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrEmpty(currentUserId))
        {
            return Unauthorized();
        }

        var response = await _portfolioService.DeletePortfolioAsync(id, currentUserId);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return NoContent();
    }
}