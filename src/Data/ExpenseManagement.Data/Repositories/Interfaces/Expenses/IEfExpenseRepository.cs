using ExpenseManagement.Base.Enums;
using ExpenseManagement.Data.Entities;

namespace ExpenseManagement.Data.Repositories.Interfaces.Expenses;

/// <summary>
/// Interface for interacting with the repository of Expense entities in the data layer.
/// </summary>
public interface IEfExpenseRepository
{
    /// <summary>
    /// Creates a new Expense asynchronously in the repository.
    /// </summary>
    /// <param name="entity">The Expense entity to be added to the repository.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task<Expense> CreateExpenseAsync(Expense entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing Expense asynchronously in the repository.
    /// </summary>
    /// <param name="entity">The Expense entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task<Expense> UpdateExpenseAsync(Expense entity, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an Expense asynchronously from the repository based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the Expense to delete.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task DeleteExpenseAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an Expense from the repository based on the specified parameters.
    /// </summary>
    /// <param name="id">The ID of the Expense to retrieve (optional).</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations (optional).</param>
    /// <returns>The retrieved Expense.</returns>
    Task<Expense> GetExpenseById(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an Expense from the repository based on the specified parameters.
    /// </summary>
    /// <param name="appUserId">The appUserId of the Expense to retrieve (optional).</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations (optional).</param>
    /// <returns>The retrieved Expense.</returns>
    Task<List<Expense>> GetExpenseByAppUserId(int appUserId, CancellationToken cancellationToken);

    ///<summary>
    /// Get all Expenses from dbContext.
    /// </summary>
    Task<List<Expense>> GetAllExpensesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Filters a list of expenses based on optional parameters such as status, expensed date, and amount.
    /// </summary>
    /// <param name="status">Optional parameter for filtering by expense status.</param>
    /// <param name="expensedDate">Optional parameter for filtering by expense expensed date.</param>
    /// <param name="amount">Optional parameter for filtering by expense amount.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A list of filtered expenses.</returns>
    Task<List<Expense>> FilterExpensesByParameter(
            Status? status = null,
            DateTime? expensedDate = null,
            decimal? amount = null,
            CancellationToken? cancellationToken = null
        );
    /// <summary>
    /// Checks if an Expense entity is unique based on certain criteria.
    /// </summary>
    /// <param name="entity">The Expense entity to check for uniqueness.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>True if the Expense is unique; false otherwise.</returns>
    Task<bool> IsUniqueAsync(Expense entity, CancellationToken cancellationToken);
}