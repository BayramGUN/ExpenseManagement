using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Expense.Requests;
using MediatR;

namespace ExpenseManagement.Business.ExpenseCqrs.Commands;

public record CreateExpenseCommand(CreateExpenseRequest Model) 
    : IRequest<ApiResponse>;
public record ApproveExpenseCommand(ApproveExpenseRequest Model) 
    : IRequest<ApiResponse>;