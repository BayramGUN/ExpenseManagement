using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Data.Repositories.Implementations.AppUsers;

/// <summary>
/// Repository implementation for interacting with AppUser entities in the database.
/// </summary>
public class AppUserRepository : IAppUserRepository
{
    private readonly ExpenseManagementDbContext dbContext;
    
    /// <summary>
    /// Initializes a new instance of the AppUserRepository class.
    /// </summary>
    /// <param name="dbContext">The DbContext for interacting with the database.</param>
    public AppUserRepository(ExpenseManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    ///<summary>
    /// Soft deletes an AppUser by setting the IsActive flag to false.
    /// </summary>
    /// <param name="id">The ID of the AppUser to delete.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task DeleteAppUserAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var appUser = await dbContext.Set<AppUser>()
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync(cancellationToken);
        appUser!.IsActive = false;
        dbContext.Update(appUser);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves an AppUser from the repository based on the specified parameters.
    /// </summary>
    /// <param name="id">The ID of the AppUser to retrieve (optional).</param>
    /// <param name="identityNumber">The identity number of the AppUser to retrieve (optional).</param>
    /// <param name="email">The email of the AppUser to retrieve (optional).</param>
    /// <param name="userName">The username of the AppUser to retrieve (optional).</param>
    /// <param name="phone">The phone of the AppUser to retrieve (optional).</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations (optional).</param>
    /// <returns>The retrieved AppUser.</returns>
    public async Task<AppUser> GetAppUserByParameter(
        int? id = null,
        string? identityNumber = null,
        string? email = null,
        string? userName = null,
        string? phone = null,
        CancellationToken? cancellationToken = null) =>
            await dbContext.Set<AppUser>()
                .Include(e => e.Expenses)
                .Where(
                    x => string.Equals(x.IdentityNumber, identityNumber, StringComparison.OrdinalIgnoreCase) || 
                    x.Id == id ||
                    string.Equals(x.Email, email, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.UserName, userName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(x.Phone, phone, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync((CancellationToken)cancellationToken!) ?? default!;
    /// <summary>
    /// Updates an existing AppUser in the repository.
    /// </summary>
    /// <param name="appUser">The AppUser entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task UpdateAppUserAsync(
        AppUser appUser, 
        CancellationToken cancellationToken)
    {
        dbContext.Update(appUser);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Creates a new AppUser in the repository.
    /// </summary>
    /// <param name="entity">The AppUser entity to be added to the repository.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>The added AppUser entity.</returns>
    public async Task<AppUser> CreateAppUserAsync(
        AppUser entity, 
        CancellationToken cancellationToken)
    {
        
        var result = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    /// <summary>
    /// Checks if an AppUser entity is unique based on certain criteria.
    /// </summary>
    /// <param name="entity">The AppUser entity to check for uniqueness.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>True if the AppUser is unique; false otherwise.</returns>
    public async Task<bool> IsUniqueAsync(
        AppUser entity, 
        CancellationToken cancellationToken) 
            => await dbContext.Set<AppUser>().Where(
                x => x.IdentityNumber == entity.IdentityNumber || 
                x.Email == entity.Email ||
                x.IBAN == entity.IBAN ||
                x.Phone == entity.Phone ||
                x.UserName! == entity.UserName )
            .FirstOrDefaultAsync(cancellationToken) is null;

}