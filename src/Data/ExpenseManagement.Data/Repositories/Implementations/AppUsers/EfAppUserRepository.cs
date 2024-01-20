using ExpenseManagement.Base.Enums;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.Repositories.Interfaces.AppUsers;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Data.Repositories.Implementations.AppUsers;

/// <summary>
/// Repository implementation for interacting with AppUser entities in the database.
/// </summary>
public class EfAppUserRepository : IEfAppUserRepository
{
    private readonly EfExpenseManagementDbContext dbContext;
    
    /// <summary>
    /// Initializes a new instance of the EfAppUserRepository class.
    /// </summary>
    /// <param name="dbContext">The DbContext for interacting with the database.</param>
    public EfAppUserRepository(EfExpenseManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    ///<summary>
    /// Get all AppUsers from dbContext.
    /// </summary>
    public async Task<List<AppUser>> GetAllAppUsersAsync(CancellationToken cancellationToken) =>
        await dbContext.Set<AppUser>().ToListAsync(cancellationToken);
    

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
    public async Task<AppUser> GetAppUserByParameterAsync(
        int? id = null,
        string? identityNumber = null,
        string? email = null,
        string? userName = null,
        string? phone = null,
        CancellationToken? cancellationToken = null) =>
            await dbContext.Set<AppUser>()
                .Include(e => e.Expenses)
                .Where(
                        x => identityNumber!.Equals(x.IdentityNumber) || 
                        x.Id == id ||
                        email!.Equals(x.Email) ||
                        userName!.Equals(x.UserName) ||
                        phone!.Equals(x.Phone)
                    )
                .FirstOrDefaultAsync((CancellationToken)cancellationToken!) ?? default!;
    /// <summary>
    /// Updates an existing AppUser in the repository.
    /// </summary>
    /// <param name="appUser">The AppUser entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task<AppUser> UpdateAppUserAsync(
        AppUser appUser, 
        CancellationToken cancellationToken)
    {
        var result = dbContext.Update(appUser);
        await dbContext.SaveChangesAsync();
        return result.Entity;
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

    /// <summary>
    /// Filters a list of AppUsers based on optional parameters such as first name, last name, role, activity status, and user status.
    /// </summary>
    /// <param name="firstName">Optional parameter for filtering by first name.</param>
    /// <param name="lastName">Optional parameter for filtering by last name.</param>
    /// <param name="role">Optional parameter for filtering by user role.</param>
    /// <param name="isActive">Optional parameter for filtering by user activity status.</param>
    /// <param name="status">Optional parameter for filtering by user status.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>A list of filtered AppUsers.</returns>
    public async Task<List<AppUser>> FilterAppUsersByParameterAsync(
        string? firstName = null,
        string? lastName = null,
        UserRole? role = null,
        bool? isActive = null,
        bool? status = null,
        CancellationToken? cancellationToken = null)
    {
        var query = dbContext.Set<AppUser>().Include(e => e.Expenses).AsQueryable();

        if (firstName is not null)
            query = query.Where(x => string.Equals(x.FirstName.ToLower(), firstName.ToLower()));

        if (lastName is not null)
            query = query.Where(x => string.Equals(x.LastName.ToLower(), lastName.ToLower()));

        if (role is not null)
            query = query.Where(x => x.Role == role.Value);

        if (isActive is not null)
            query = query.Where(x => x.IsActive == isActive);

        if (status is not null)
            query = query.Where(x => x.Status == status);

        return await query.ToListAsync(cancellationToken ?? CancellationToken.None);
    }
}