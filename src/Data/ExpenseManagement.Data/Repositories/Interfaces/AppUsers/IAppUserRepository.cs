namespace ExpenseManagement.Data.Repositories.Interfaces.AppUsers;

/// <summary>
/// Interface for interacting with the repository of AppUser entities in the data layer.
/// </summary>
public interface IAppUserRepository
{
    /// <summary>
    /// Creates a new AppUser asynchronously in the repository.
    /// </summary>
    /// <param name="entity">The AppUser entity to be added to the repository.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task<AppUser> CreateAppUserAsync(AppUser entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing AppUser asynchronously in the repository.
    /// </summary>
    /// <param name="entity">The AppUser entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task<AppUser> UpdateAppUserAsync(AppUser entity, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an AppUser asynchronously from the repository based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the AppUser to delete.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task DeleteAppUserAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an AppUser from the repository based on the specified parameters.
    /// </summary>
    /// <param name="id">The ID of the AppUser to retrieve (optional).</param>
    /// <param name="identityNumber">The identity number of the AppUser to retrieve (optional).</param>
    /// <param name="email">The email of the AppUser to retrieve (optional).</param>
    /// <param name="userName">The username of the AppUser to retrieve (optional).</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations (optional).</param>
    /// <returns>The retrieved AppUser.</returns>
    Task<AppUser> GetAppUserByParameter(
            int? id = null, 
            string? identityNumber = null,
            string? email = null,
            string? userName = null,
            string? phone = null,
            CancellationToken? cancellationToken = null
        );

    ///<summary>
    /// Get all AppUsers from dbContext.
    /// </summary>
    Task<List<AppUser>> GetAllAppUsersAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Checks if an AppUser entity is unique based on certain criteria.
    /// </summary>
    /// <param name="entity">The AppUser entity to check for uniqueness.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>True if the AppUser is unique; false otherwise.</returns>
    Task<bool> IsUniqueAsync(AppUser entity, CancellationToken cancellationToken);
}