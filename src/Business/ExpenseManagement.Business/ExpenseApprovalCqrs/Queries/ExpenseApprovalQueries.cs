using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.ExpenseApproval.Requests;
using ExpenseManagement.Schema.ExpenseApproval.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Queries;

public record GetExpenseApprovalsByParameterQuery(GetExpenseApprovalsByParameterRequest Model) 
    : IRequest<ApiResponse<List<ExpenseApprovalResponse>>>;
public record GetAllExpenseApprovalsQuery() 
    : IRequest<ApiResponse<List<ExpenseApprovalResponse>>>;
public record GetExpenseApprovalByIdQuery(int Id) 
    : IRequest<ApiResponse<ExpenseApprovalResponse>>;
public record GetExpenseApprovalsByApproverIdQuery(int ApproverId) 
    : IRequest<ApiResponse<List<ExpenseApprovalResponse>>>;
