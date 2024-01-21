using ExpenseManagement.Base.Routes;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Implementations.Expenses;
using ExpenseManagement.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagement.Api.Controllers;

[Route(ControllerRoute.BaseRoute)]
[ApiController]
public class ReportController : ControllerBase
{  
    private readonly IDapperExpenseReportRepository expenseReportRepository;

    public ReportController(IDapperExpenseReportRepository expenseReportRepository)
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