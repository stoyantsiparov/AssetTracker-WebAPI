using AssetTracker_WebAPI.DTOs.Transaction;
using AssetTracker_WebAPI.Services.Transaction.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker_WebAPI.Controllers;

[ApiController]
[Route("api/portfolios/{portfolioId}/transactions")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(int portfolioId, [FromBody] CreateTransactionDto createTransactionDto)
    {
        var response = await _transactionService.CreateTransactionAsync(portfolioId, createTransactionDto);
        if (!response.Success) return BadRequest(response);

        return Ok(response);
    }
}