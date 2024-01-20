using AutoMapper;
using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.ExpenseApproval.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Queries;

/// <summary>
/// Handles the execution of the GetExpenseApprovalByApproverIdQuery, retrieving an expenseApproval by its unique AppUseridentifier.
/// </summary>
public class GetExpenseApprovalsByApproverIdQueryHandler : 
    IRequestHandler<GetExpenseApprovalsByApproverIdQuery, ApiResponse<List<ExpenseApprovalResponse>>>
{
    private readonly IEfExpenseApprovalRepository expenseApprovalRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExpenseApprovalsByApproverIdQueryHandler"/> class.
    /// </summary>
    /// <param name="expenseApprovalRepository">The repository for interacting with expenseApproval entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetExpenseApprovalsByApproverIdQueryHandler(
        IEfExpenseApprovalRepository expenseApprovalRepository, 
        IMapper mapper)
    {
        this.expenseApprovalRepository = expenseApprovalRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving an expenseApproval by its unique Approver identifier.
    /// </summary>
    /// <param name="request">The GetExpenseApprovalByApproverIdQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the expenseApproval response model or an error message if not found.</returns>
    public async Task<ApiResponse<List<ExpenseApprovalResponse>>> Handle(
        GetExpenseApprovalsByApproverIdQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the expenseApproval based on the ApproverId parameter.
        var expenseApprovals = await expenseApprovalRepository.FilterExpenseApprovalsByParameterAsync(
            approverId: request.ApproverId,
            cancellationToken: cancellationToken);

        // If no expenseApproval is found, return an error response.
        if(expenseApprovals is null)
            return new ApiResponse<List<ExpenseApprovalResponse>>(ExceptionMessages.NotFoundWithParameter(request.ApproverId));

        // Map the expenseApproval to the response model and return a successful response.
        return new ApiResponse<List<ExpenseApprovalResponse>>(mapper.Map<List<ExpenseApprovalResponse>>(expenseApprovals));
    }
}

