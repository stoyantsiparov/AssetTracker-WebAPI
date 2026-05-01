namespace AssetTracker_WebAPI.Common;

/// <summary>
/// Contains standard response and error messages used across the application API.
/// </summary>
public static class AppMessages
{
    // Asset Messages
    public const string AssetsRetrievedSuccessfully = "Assets retrieved successfully.";
    public const string ErrorRetrievingAssets = "An error occurred while retrieving assets.";
    public const string AssetNotFound = "Asset with ID {0} was not found.";
    public const string AssetAlreadyExists = "An asset with ticker '{0}' already exists.";
    public const string AssetCreatedSuccessfully = "Asset created successfully.";
    public const string ErrorCreatingAsset = "An error occurred while creating the asset.";

    // Transaction Messages
    public const string TransactionCreatedSuccessfully = "Transaction created successfully.";
    public const string ErrorCreatingTransaction = "An error occurred while creating the transaction.";

    // Portfolio Messages
    public const string PortfoliosRetrievedSuccessfully = "Portfolios retrieved successfully.";
    public const string ErrorRetrievingPortfolios = "An error occurred while retrieving portfolios.";
    public const string PortfolioNotFound = "Portfolio with ID {0} was not found.";
    public const string PortfolioCreatedSuccessfully = "Portfolio created successfully.";
    public const string ErrorCreatingPortfolio = "An error occurred while creating the portfolio.";

    // Portfolio Validation Messages
    public const string PortfolioNameRequired = "Portfolio name is required.";
    public const string PortfolioNameMaxLength = "Portfolio name cannot exceed 100 characters.";

    // Portfolio Action Messages
    public const string PortfolioNotFoundOrAccessDenied = "Portfolio not found or access denied.";
    public const string PortfolioUpdatedSuccessfully = "Portfolio updated successfully.";
    public const string PortfolioDeletedSuccessfully = "Portfolio deleted successfully.";
    public const string ErrorUpdatingPortfolio = "An error occurred while updating the portfolio.";
    public const string ErrorDeletingPortfolio = "An error occurred while deleting the portfolio.";

    // External API Messages
    public const string ExternalApiFetchError = "Failed to fetch data from {0}. Status Code: {1}";
    public const string ExternalApiInvalidData = "Invalid data or no data found for '{0}' from {1}.";
    public const string ExternalApiDataRetrieved = "Data for {0} retrieved successfully from {1}.";
    public const string ExternalApiCommunicationError = "An error occurred while communicating with {0}.";

    // Swagger Messages
    public const string SwaggerJwtDescription = "Enter your JWT token here. Example: \"eyJhbGciOiJIUzI1...\"";
}