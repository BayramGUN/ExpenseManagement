using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Schema.Expense.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseCqrs.Queries;

/// <summary>
/// Handles the execution of the GetExpenseByIdQuery, retrieving an expense by its unique identifier.
/// </summary>
public class GetExpenseByIdQueryHandler : 
    IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>
{
    private readonly IEfExpenseRepository expenseRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExpenseByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="expenseRepository">The repository for interacting with expense entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetExpenseByIdQueryHandler(
        IEfExpenseRepository expenseRepository, 
        IMapper mapper)
    {
        this.expenseRepository = expenseRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving an expense by its unique identifier.
    /// </summary>
    /// <param name="request">The GetExpenseByIdQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the expense response model or an error message if not found.</returns>
    public async Task<ApiResponse<ExpenseResponse>> Handle(
        GetExpenseByIdQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve the expense based on the provided id parameter.
        var expense = await expenseRepository.GetExpenseById(request.Id, cancellationToken);

        // If no expense is found, return an error response.
        if(expense is null)
            return new ApiResponse<ExpenseResponse>(ExceptionMessages.NotFoundWithParameter(request.Id));

        // Map the expense to the response model and return a successful response.
        return new ApiResponse<ExpenseResponse>(mapper.Map<ExpenseResponse>(expense));
    }
}
