using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.Expense.Requests;
using ExpenseManagement.Schema.ExpenseApproval.Requests;
using MediatR;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Commands;

public record CreateExpenseApprovalCommand(CreateExpenseApprovalRequest Model) 
    : IRequest<ApiResponse>;
public record UpdateExpenseApprovalCommand(UpdateExpenseApprovalRequest Model) 
    : IRequest<ApiResponse>;
public record DeleteExpenseApprovalCommand(int Id) 
    : IRequest<ApiResponse>;