using AutoMapper;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Schema.Expense.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseCqrs.Queries;

public class GetAllExpensesQueryHandler : 
    IRequestHandler<GetAllExpensesQuery, ApiResponse<List<ExpenseResponse>>>
{
    private readonly IEfExpenseRepository expenseRepository;
    private readonly IMapper mapper;

    public GetAllExpensesQueryHandler(
        IEfExpenseRepository expenseRepository, 
        IMapper mapper)
    {
        this.expenseRepository = expenseRepository;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(
        GetAllExpensesQuery request,
        CancellationToken cancellationToken)
    {
        var Expenses = await expenseRepository.GetAllExpensesAsync(cancellationToken);
        return new ApiResponse<List<ExpenseResponse>>(mapper.Map<List<ExpenseResponse>>(Expenses));
    }
}
