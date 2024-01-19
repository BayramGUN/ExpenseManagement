using ExpenseManagement.Base.Enums;
using ExpenseManagement.Data.Entities;

namespace ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;

/// <summary>
/// Interface for interacting with the repository of ExpenseApproval entities in the data layer.
/// </summary>
public interface IEfExpenseApprovalRepository
{
    /// <summary>
    /// Creates a new ExpenseApproval asynchronously in the repository.
    /// </summary>
    /// <param name="entity">The ExpenseApproval entity to be added to the repository.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task<ExpenseApproval> CreateExpenseApprovalAsync(ExpenseApproval entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing ExpenseApproval asynchronously in the repository.
    /// </summary>
    /// <param name="entity">The ExpenseApproval entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task<ExpenseApproval> UpdateExpenseApprovalAsync(ExpenseApproval entity, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an ExpenseApproval asynchronously from the repository based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the ExpenseApproval to delete.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task DeleteExpenseApprovalAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an ExpenseApproval from the repository based on the specified parameters.
    /// </summary>
    /// <param name="id">The ID of the ExpenseApproval to retrieve (optional).</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations (optional).</param>
    /// <returns>The retrieved ExpenseApproval.</returns>
    Task<ExpenseApproval> GetExpenseApprovalById(int id, CancellationToken cancellationToken);

    ///<summary>
    /// Get all ExpenseApprovals from dbContext.
    /// </summary>
    Task<List<ExpenseApproval>> GetAllExpenseApprovalsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an ExpenseApproval from the repository based on the specified parameters.
    /// </summary>
    /// <param name="status">The status of the ExpenseApproval to retrieve (optional).</param>
    /// <param name="approverId">The approverId of the ExpenseApproval to retrieve (optional).</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations (optional).</param>
    /// <returns>The retrieved ExpenseApproval.</returns>
    Task<List<ExpenseApproval>> FilterExpenseApprovalsByParameter(
            Status? status = null,
            int? approverId = null!,
            CancellationToken? cancellationToken = null
        );

    /// <summary>
    /// Checks if an ExpenseApproval entity is unique based on certain criteria.
    /// </summary>
    /// <param name="entity">The ExpenseApproval entity to check for uniqueness.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>True if the ExpenseApproval is unique; false otherwise.</returns>
    Task<bool> IsUniqueAsync(ExpenseApproval entity, CancellationToken cancellationToken);
}