using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Schema.Expense.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseCqrs.Queries;

/// <summary>
/// Handles the execution of the GetExpenseByAppUserIdQuery, retrieving an expense by its unique AppUseridentifier.
/// </summary>
public class GetExpensesByAppUserIdQueryHandler : 
    IRequestHandler<GetExpensesByAppUserIdQuery, ApiResponse<List<ExpenseResponse>>>
{
    private readonly IEfExpenseRepository expenseRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExpensesByAppUserIdQueryHandler"/> class.
    /// </summary>
    /// <param name="expenseRepository">The repository for interacting with expense entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetExpensesByAppUserIdQueryHandler(
        IEfExpenseRepository expenseRepository, 
        IMapper mapper)
    {
        this.expenseRepository = expenseRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving an expense by its unique AppUser identifier.
    /// </summary>
    /// <param name="request">The GetExpenseByAppUserIdQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the expense response model or an error message if not found.</returns>
    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(
        GetExpensesByAppUserIdQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the expense based on the provAppUserided AppUserid parameter.
        var expenses = await expenseRepository.GetExpenseByAppUserIdAsync(request.AppUserId, cancellationToken);

        // If no expense is found, return an error response.
        if(expenses is null)
            return new ApiResponse<List<ExpenseResponse>>(ExceptionMessages.NotFoundWithParameter(request.AppUserId));

        // Map the expense to the response model and return a successful response.
        return new ApiResponse<List<ExpenseResponse>>(mapper.Map<List<ExpenseResponse>>(expenses));
    }
}
