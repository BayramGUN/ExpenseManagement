using ExpenseManagement.Base.Constants.Database;
using ExpenseManagement.Data.DbContexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManagement.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataInjection(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddPersistence(configuration);
        
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        string connection = configuration.GetConnectionString(SqlServerDbConnectionKeys.ConnectionString)!; 
        services.AddDbContext<ExpenseManagementDbContext>(options =>
            options.UseSqlServer(connection,
                b => b.MigrationsAssembly(MigrationsAssemblies.ApiMigrationAssembly)
        ));
        return services;
    }
}