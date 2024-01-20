using AutoMapper;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Schema.Expense.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseCqrs.Queries;

/// <summary>
/// Handles the execution of the GetExpensesByParameterQuery, retrieving a list of expenses based on specified parameters.
/// </summary>
public class GetExpensesByQueryHandler : 
    IRequestHandler<GetExpensesByParameterQuery, ApiResponse<List<ExpenseResponse>>>
{
    private readonly IEfExpenseRepository expenseRepository;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetExpensesByQueryHandler"/> class.
    /// </summary>
    /// <param name="expenseRepository">The repository for interacting with expense entities.</param>
    /// <param name="mapper">The AutoMapper instance for mapping entities to response models.</param>
    public GetExpensesByQueryHandler(
        IEfExpenseRepository expenseRepository, 
        IMapper mapper)
    {
        this.expenseRepository = expenseRepository;
        this.mapper = mapper;
    }

    /// <summary>
    /// Handles the execution of the query, retrieving a list of expenses based on specified parameters.
    /// </summary>
    /// <param name="request">The GetExpensesByParameterQuery instance.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A response containing the list of expense response models or an error message if not found.</returns>
    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(
        GetExpensesByParameterQuery request, 
        CancellationToken cancellationToken)
    {
        // Retrieve expenses based on the provided parameters.
        var expenses = await expenseRepository.FilterExpensesByParameterAsync(
            status: request.Model.Status,
            expensedDate: request.Model.ExpensedDate,
            amount: request.Model.Amount,
            cancellationToken: cancellationToken
        );

        // If no expenses are found, return an error response.
        if(expenses is null)
            return new ApiResponse<List<ExpenseResponse>>(ExceptionMessages.NotFoundWithParameter(request.Model));

        // Map the expenses to the response models and return a successful response.
        return new ApiResponse<List<ExpenseResponse>>(mapper.Map<List<ExpenseResponse>>(expenses));
    }
}
