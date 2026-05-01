using System.ComponentModel.DataAnnotations;

namespace AssetTracker_WebAPI.DTOs.Auth;

/// <summary>
/// Data Transfer Object for user registration.
/// </summary>
public class RegisterDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string Password { get; set; } = string.Empty;
}