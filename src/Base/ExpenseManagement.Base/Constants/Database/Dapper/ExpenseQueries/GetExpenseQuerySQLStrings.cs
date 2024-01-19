namespace ExpenseManagement.Base.Constants.DataBase.Dapper.ExpenseQueries;

public class GetExpenseQuerySQLStrings
{
    public const string GetAllExpensesSQL = "SELECT * FROM [dbo].[Expenses]";
    public static readonly Func<int, string> GetExpensesByAppUserId
        = (id) => 
            """
                SELECT * FROM [dbo].[Expenses] 
                WHERE [dbo].[Expenses].[AppUserId] = @AppUserId
            """;
    
}