using AssetTracker_WebAPI.DTOs.Auth;
using AssetTracker_WebAPI.Services.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AssetTracker_WebAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var response = await _authService.RegisterAsync(registerDto);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var response = await _authService.LoginAsync(loginDto);
        if (!response.Success) return Unauthorized(response);
        return Ok(response);
    }
}