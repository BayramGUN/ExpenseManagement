using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Authentication.Requests;
using ExpenseManagement.Schema.Authentication.Responses;
using MediatR;

namespace ExpenseManagement.Business.Authentication.Commands;

/// <summary>
/// Represents a command to sign up a new user.
/// </summary>
public record SignUpCommand(SignUpRequest Model) : IRequest<ApiResponse<TokenResponse>>;

/// <summary>
/// Represents a command to sign in an existing user.
/// </summary>
public record SignInCommand(SignInRequest Model) : IRequest<ApiResponse<TokenResponse>>;

