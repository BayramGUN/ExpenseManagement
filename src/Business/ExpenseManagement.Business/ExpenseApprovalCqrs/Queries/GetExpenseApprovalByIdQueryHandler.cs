using AutoMapper;
using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.ExpenseApproval.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Queries;

/// <summary>
/// Handles the execution of the GetExpenseApprovalByIdQuery, retrieving an expenseApproval by its unique identifier.
/// </summary>
public class GetExpenseApprovalByIdQueryHandler : 
    IRequestHandler<GetExpenseApprovalByIdQuery, ApiResponse<ExpenseApprovalResponse>>
{
    private readonly IEfExpenseApprovalRepository expenseApprovalRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExpenseApprovalByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="expenseApprovalRepository">The repository for interacting with expenseApproval entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetExpenseApprovalByIdQueryHandler(
        IEfExpenseApprovalRepository expenseApprovalRepository, 
        IMapper mapper)
    {
        this.expenseApprovalRepository = expenseApprovalRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving an expenseApproval by its unique identifier.
    /// </summary>
    /// <param name="request">The GetExpenseApprovalByIdQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the expenseApproval response model or an error message if not found.</returns>
    public async Task<ApiResponse<ExpenseApprovalResponse>> Handle(
        GetExpenseApprovalByIdQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the expenseApproval based on the provided id parameter.
        var expenseApproval = await expenseApprovalRepository.GetExpenseApprovalByIdAsync(
                                            request.Id, cancellationToken);

        // If no expenseApproval is found, return an error response.
        if(expenseApproval is null)
            return new ApiResponse<ExpenseApprovalResponse>(
                ExceptionMessages.NotFoundWithParameter(request.Id));

        // Map the expenseApproval to the response model and return a successful response.
        return new ApiResponse<ExpenseApprovalResponse>(
            mapper.Map<ExpenseApprovalResponse>(expenseApproval));
    }
}
