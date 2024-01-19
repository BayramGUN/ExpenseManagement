using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Expense.Requests;
using MediatR;

namespace ExpenseManagement.Business.ExpenseCqrs.Commands;

public record CreateExpenseCommand(CreateExpenseRequest Model) 
    : IRequest<ApiResponse>;
public record UpdateExpenseCommand(UpdateExpenseRequest Model) 
    : IRequest<ApiResponse>;
public record DeleteExpenseCommand(int Id) 
    : IRequest<ApiResponse>;
public record ApproveExpenseCommand(ApproveExpenseRequest Model) 
    : IRequest<ApiResponse>;