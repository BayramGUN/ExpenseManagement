using ExpenseManagement.Base.Constants.Database;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.Repositories.Implementations.AppUsers;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseManagement.Data;

/// <summary>
/// Provides extension methods for configuring dependency injection related to data access in the ExpenseManagement application.
/// </summary>
public static class DependencyInjection
{

    /// <summary>
    /// Adds data-related services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which data services are added.</param>
    /// <param name="configuration">The configuration containing necessary settings.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddDataInjection(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddPersistence(configuration);
        services.AddScoped<IAppUserRepository, AppUserRepository>();
        return services;
    }

    /// <summary>
    /// Adds data persistence services, including DbContext configuration, to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to which persistence services are added.</param>
    /// <param name="configuration">The configuration containing necessary settings.</param>
    /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
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