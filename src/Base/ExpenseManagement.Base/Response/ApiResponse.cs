

using System.Text.Json;

namespace ExpenseManagement.Base.Response;

/// <summary>
/// Represents a standard response structure for API operations, providing information about the success status,
/// a message, server timestamp, and a unique reference number. This class can be used for both generic and
/// non-generic responses.
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Gets or sets a flag indicating the success status of the API operation.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Gets or sets a message associated with the API operation, providing additional information in case of an error.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the timestamp representing the server's date and time when the response was generated.
    /// Defaults to the UTC time at the moment of response creation.
    /// </summary>
    public DateTime ServerDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets a unique reference number associated with the API response.
    /// Defaults to a new globally unique identifier (GUID) when not explicitly provided.
    /// </summary>
    public Guid ReferenceNo { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse"/> class.
    /// </summary>
    /// <param name="message">An optional message associated with the API response. If null or whitespace, the response is considered successful.</param>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApiResponse(string? message = null)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            Success = true;
        }
        else
        {
            Success = false;
            Message = message;
        }
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}

/// <summary>
/// Represents a generic version of the ApiResponse class, including a typed response payload.
/// </summary>
/// <typeparam name="T">The type of the response payload.</typeparam>
public class ApiResponse<T> : ApiResponse
{
    /// <summary>
    /// Gets or sets the typed response payload associated with the API operation.
    /// </summary>
    public T Response { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with a success status.
    /// </summary>
    /// <param name="isSuccess">A flag indicating whether the API operation was successful.</param>
    public ApiResponse(bool isSuccess)
    {
        Response = default!;
        Message = isSuccess ? "Success" : "Error";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with a typed response payload.
    /// </summary>
    /// <param name="data">The typed response payload.</param>
    public ApiResponse(T data)
    {
        Response = data;
        Message = "Success";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiResponse{T}"/> class with an error message.
    /// </summary>
    /// <param name="message">An error message associated with the API response.</param>
    public ApiResponse(string message)
    {
        Response = default!;
        Message = message;
    }
}
