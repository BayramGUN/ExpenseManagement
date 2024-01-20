using AutoMapper;
using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Schema.Expense.Responses;
using ExpenseManagement.Schema.ExpenseApproval.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Queries;

/// <summary>
/// Handles the execution of the GetExpenseApprovalsByParameterQuery, retrieving a list of expenseApprovals based on specified parameters.
/// </summary>
public class GetExpenseApprovalsByQueryHandler : 
    IRequestHandler<GetExpenseApprovalsByParameterQuery, ApiResponse<List<ExpenseApprovalResponse>>>
{
    private readonly IEfExpenseApprovalRepository expenseApprovalRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExpenseApprovalsByQueryHandler"/> class.
    /// </summary>
    /// <param name="expenseApprovalRepository">The repository for interacting with expenseApproval entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetExpenseApprovalsByQueryHandler(
        IEfExpenseApprovalRepository expenseApprovalRepository, 
        IMapper mapper)
    {
        this.expenseApprovalRepository = expenseApprovalRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving a list of expenseApprovals based on specified parameters.
    /// </summary>
    /// <param name="request">The GetExpenseApprovalsByParameterQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the list of expenseApproval response models or an error message if not found.</returns>
    public async Task<ApiResponse<List<ExpenseApprovalResponse>>> Handle(
        GetExpenseApprovalsByParameterQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve expenseApprovals based on the provided parameters.
        var expenseApprovals = await expenseApprovalRepository.FilterExpenseApprovalsByParameterAsync(
            status: request.Model.Status,
            approverId: request.Model.ApproverId,
            cancellationToken: cancellationToken
        );

        // If no expenseApprovals are found, return an error response.
        if(expenseApprovals is null)
            return new ApiResponse<List<ExpenseApprovalResponse>>(ExceptionMessages.NotFoundWithParameter(request.Model));

        // Map the expenseApprovals to the response models and return a successful response.
        return new ApiResponse<List<ExpenseApprovalResponse>>(mapper.Map<List<ExpenseApprovalResponse>>(expenseApprovals));
    }
}
