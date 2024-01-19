using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Expense.Requests;
using ExpenseManagement.Schema.Expense.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseCqrs.Queries;

public record GetExpensesByParameterQuery(GetExpensesByParameterRequest Model) 
    : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetAllExpensesQuery() 
    : IRequest<ApiResponse<List<ExpenseResponse>>>;
public record GetExpenseByIdQuery(int Id) 
    : IRequest<ApiResponse<ExpenseResponse>>;
public record GetExpensesByAppUserIdQuery(int AppUserId) 
    : IRequest<ApiResponse<List<ExpenseResponse>>>;
