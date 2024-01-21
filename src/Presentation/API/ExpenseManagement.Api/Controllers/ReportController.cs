using ExpenseManagement.Base.Constants.Authorization;
using ExpenseManagement.Base.Response;
using ExpenseManagement.Base.Routes;
using ExpenseManagement.Business.PaymentCqrs.Commands;
using ExpenseManagement.Business.PaymentCqrs.Queries;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Implementations.Expenses;
using ExpenseManagement.Schema.Payment.Requests;
using ExpenseManagement.Schema.Payment.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;

[Route(ControllerRoute.BaseRoute)]
[ApiController]
public class ReportController : ControllerBase
{  
    private readonly DapperExpenseReportRepository expenseReportRepository;

    public ReportController(DapperExpenseReportRepository expenseReportRepository)
    {
        this.expenseReportRepository = expenseReportRepository;
    }

    [HttpGet(EndpointRoute.GetByAppUserId)]
    public IEnumerable<EmployeeExpenseReport> GetEmployeeExpenseReport([FromRoute]int appUserId)
    {
        return expenseReportRepository.GetEmployeeExpenseReport(appUserId);
    }
    [HttpGet(EndpointRoute.ApprovalStatusReport)]
    public IEnumerable<ApprovalStatusReport> GetApprovalStatusReport()
    {
        return expenseReportRepository.GetApprovalStatusReports();
    }
    [HttpGet(EndpointRoute.PaymentDifferenceReport)]
    public IEnumerable<PaymentAndApprovedExpenseDifference> GetPaymentAndApprovedExpenseDifference()
    {
        return expenseReportRepository.GetPaymentAndApprovedExpenseDifference();
    }
}