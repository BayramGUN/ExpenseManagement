using System.Data;
using Dapper;
using ExpenseManagement.Base.Constants.DataBase.Dapper.ExpenseQueries;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Interfaces;

namespace ExpenseManagement.Data.Repositories.Implementations.Expenses;

public class DapperExpenseReportRepository : IDapperExpenseReportRepository
{
    private readonly DapperSqlConnectionFactory sqlConnectionFactory;

    public DapperExpenseReportRepository(DapperSqlConnectionFactory sqlConnectionFactory)
    {
        this.sqlConnectionFactory = sqlConnectionFactory;
    }

    public IEnumerable<EmployeeExpenseReport> GetEmployeeExpenseReport(int appUserId)
    {
        using var connection = sqlConnectionFactory.Create();
        connection.Open();
        var results = connection.Query<EmployeeExpenseReport>(
            ExpenseReportsQuerySQLStrings.GetEmployeeExpenseReport, 
            new { AppUserId = appUserId });
        return results;
    }
    public IEnumerable<ApprovalStatusReport> GetApprovalStatusReports()
    {
        using var connection = sqlConnectionFactory.Create();
        connection.Open();
       var results = connection.Query<ApprovalStatusReport>(
                ExpenseReportsQuerySQLStrings.GetApprovalStatusReport, 
                commandType: CommandType.StoredProcedure);
        return results;
    }
    public IEnumerable<PaymentAndApprovedExpenseDifference> GetPaymentAndApprovedExpenseDifference()
    {
        using var connection = sqlConnectionFactory.Create();
        connection.Open();
       var results = connection.Query<PaymentAndApprovedExpenseDifference>(
                ExpenseReportsQuerySQLStrings.GetPaymentAndApprovedExpenseDifference, 
                commandType: CommandType.StoredProcedure);
        return results;
    }
}

