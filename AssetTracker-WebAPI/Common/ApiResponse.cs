namespace AssetTracker_WebAPI.Common;

/// <summary>
/// A unified model for API responses.
/// </summary>
/// <typeparam name="T">The type of the data returned upon a successful response.</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Indicates whether the request was executed successfully.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// A message describing the result of the operation (e.g., success or error details).
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// The payload data returned upon a successful request.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// A list of errors that occurred, if the request was unsuccessful.
    /// </summary>
    public List<string> Errors { get; set; } = new List<string>();
}