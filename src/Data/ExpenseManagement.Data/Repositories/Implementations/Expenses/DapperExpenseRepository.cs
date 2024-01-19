using System.Data.Entity.Core.Metadata.Edm;
using System.Net.Http.Headers;
using Dapper;
using ExpenseManagement.Base.Constants.DataBase.Dapper.ExpenseQueries;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.Entities;

namespace ExpenseManagement.Data.Repositories.Implementations.Expenses;

public class DapperExpenseRepository
{
    private readonly DapperSqlConnectionFactory sqlConnectionFactory;

    public DapperExpenseRepository(DapperSqlConnectionFactory sqlConnectionFactory)
    {
        this.sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<List<Expense>> GetAllExpenses()
    {
        using var connection = sqlConnectionFactory.Create();
        var expenses = await connection.QueryAsync<Expense>(GetExpenseQuerySQLStrings.GetAllExpensesSQL);
        return expenses.ToList();
    } 
    public async Task<Expense> GetExpenseById(int id)
    {
        using var connection = sqlConnectionFactory.Create();
        var expense = await connection.QuerySingleOrDefaultAsync<Expense>(
                GetExpenseQuerySQLStrings.GetExpensesByAppUserId(id),
                new { AppUserId = id }
            ) ?? throw new ArgumentNullException(ExceptionMessages.NotFound(id));
        return expense;
    } 
}