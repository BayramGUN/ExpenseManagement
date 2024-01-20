using AutoMapper;
using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Schema.ExpenseApproval.Responses;
using MediatR;

namespace ExpenseManagement.Business.ExpenseApprovalCqrs.Queries;

public class GetAllExpenseApprovalsQueryHandler : 
    IRequestHandler<GetAllExpenseApprovalsQuery, ApiResponse<List<ExpenseApprovalResponse>>>
{
    private readonly IEfExpenseApprovalRepository expenseApprovalRepository;
    private readonly IMapper mapper;

    public GetAllExpenseApprovalsQueryHandler(
        IEfExpenseApprovalRepository expenseApprovalRepository, 
        IMapper mapper)
    {
        this.expenseApprovalRepository = expenseApprovalRepository;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseApprovalResponse>>> Handle(
        GetAllExpenseApprovalsQuery request,
        CancellationToken cancellationToken)
    {
        var expenseApprovals = await expenseApprovalRepository.GetAllExpenseApprovalsAsync(cancellationToken);
        return new ApiResponse<List<ExpenseApprovalResponse>>(
            mapper.Map<List<ExpenseApprovalResponse>>(expenseApprovals));
    }
}
