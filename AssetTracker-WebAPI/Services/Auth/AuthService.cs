using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AssetTracker_WebAPI.Common;
using AssetTracker_WebAPI.DTOs.Auth;
using AssetTracker_WebAPI.Services.Auth.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AssetTracker_WebAPI.Services.Auth;

/// <summary>
/// Implements the business logic for user authentication and JWT generation.
/// </summary>
public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public AuthService(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    /// <inheritdoc />
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        // 1. Check if a user with the same email already exists
        var existingEmail = await _userManager.FindByEmailAsync(registerDto.Email);
        if (existingEmail != null)
        {
            return new AuthResponseDto { Success = false, Message = "User with this email already exists." };
        }

        // 2. Check if a user with the same username already exists
        var existingUsername = await _userManager.FindByNameAsync(registerDto.Username);
        if (existingUsername != null)
        {
            return new AuthResponseDto { Success = false, Message = "Username is already taken." };
        }

        // 3. Create a new IdentityUser instance with the provided username and email
        var newUser = new IdentityUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email
        };

        // UserManager automatically hashes the password when creating the user, so we just pass the plain password here
        var result = await _userManager.CreateAsync(newUser, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return new AuthResponseDto { Success = false, Message = "User registration failed.", Errors = errors };
        }

        return new AuthResponseDto { Success = true, Message = "User registered successfully." };
    }

    /// <inheritdoc />
    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        // 1. Find the user by username
        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user == null)
        {
            return new AuthResponseDto { Success = false, Message = "Invalid authentication request." };
        }

        // 2. Password verification is handled by UserManager, which compares the provided password with the stored hashed password
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
        {
            return new AuthResponseDto { Success = false, Message = "Invalid authentication request." };
        }

        // 3. Generate JWT token
        var token = GenerateJwtToken(user);

        return new AuthResponseDto
        {
            Success = true,
            Message = "Login successful.",
            Token = token
        };
    }

    /// <summary>
    /// Generates a JWT token for the authenticated user.
    /// </summary>
    private string GenerateJwtToken(IdentityUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Claims are pieces of information about the user that we want to include in the token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}