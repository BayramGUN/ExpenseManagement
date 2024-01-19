using System.Data.SqlClient;

namespace ExpenseManagement.Data.DbContexts;

public class DapperSqlConnectionFactory
{
    private readonly string connectionString;

    public DapperSqlConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public SqlConnection Create()
    {
        return new SqlConnection(connectionString);
    }
}