using ExpenseManagement.Data.Entities;

namespace ExpenseManagement.Data.Repositories.Interfaces;

public interface IDapperExpenseReportRepository
{
    IEnumerable<EmployeeExpenseReport> GetEmployeeExpenseReport(int appUserId);
    IEnumerable<ApprovalStatusReport> GetApprovalStatusReports();
    IEnumerable<PaymentAndApprovedExpenseDifference> GetPaymentAndApprovedExpenseDifference();
}