using System.ComponentModel.DataAnnotations;
using AssetTracker_WebAPI.Common;

namespace AssetTracker_WebAPI.DTOs.Portfolio;

/// <summary>
/// Data Transfer Object for updating an existing portfolio.
/// </summary>
public class UpdatePortfolioDto
{
    /// <summary>
    /// Gets or sets the new name of the portfolio.
    /// </summary>
    [Required(ErrorMessage = AppMessages.PortfolioNameRequired)]
    [MaxLength(100, ErrorMessage = AppMessages.PortfolioNameMaxLength)]
    public string Name { get; set; } = string.Empty;
}