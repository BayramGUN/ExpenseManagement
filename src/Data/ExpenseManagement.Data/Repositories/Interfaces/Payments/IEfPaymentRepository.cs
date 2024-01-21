
using ExpenseManagement.Data.Entities;

namespace ExpenseManagement.Data.Repositories.Interfaces.Payments;

/// <summary>
/// Interface for interacting with the repository of Payment entities in the data layer.
/// </summary>
public interface IEfPaymentRepository
{
    /// <summary>
    /// Creates a new Payment asynchronously in the repository.
    /// </summary>
    /// <param name="entity">The Payment entity to be added to the repository.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task<Payment> CreatePaymentAsync(Payment entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates an existing Payment asynchronously in the repository.
    /// </summary>
    /// <param name="entity">The Payment entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task<Payment> UpdatePaymentAsync(Payment entity, CancellationToken cancellationToken);

    /// <summary>
    /// Deletes an Payment asynchronously from the repository based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the Payment to delete.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    Task DeletePaymentAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an Payment from the repository based on the specified parameters.
    /// </summary>
    /// <param name="id">The ID of the Payment to retrieve (optional).</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations (optional).</param>
    /// <returns>The retrieved Payment.</returns>
    Task<Payment> GetPaymentByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an Payment from the repository based on the specified parameters.
    /// </summary>
    /// <param name="expenseId">The expenseId of the Payment to retrieve (optional).</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations (optional).</param>
    /// <returns>The retrieved Payment.</returns>
    Task<Payment> GetPaymentByExpenseIdAsync(
        int expenseId, 
        CancellationToken cancellationToken);

    ///<summary>
    /// Get all Payments from dbContext.
    /// </summary>
    Task<List<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Filters a list of Payments based on optional parameters such as status, Paymentd date, and amount.
    /// </summary>
    /// <param name="status">Optional parameter for filtering by Payment status.</param>
    /// <param name="PaymentdDate">Optional parameter for filtering by Payment Paymentd date.</param>
    /// <param name="amount">Optional parameter for filtering by Payment amount.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A list of filtered Payments.</returns>
    Task<List<Payment>> FilterPaymentsByParameterAsync(
            DateTime? paymentDate = null,
            decimal? amount = null,
            string? paymentMethod = null,
            int? appUserId = null,
            CancellationToken? cancellationToken = null
        );
    /// <summary>
    /// Checks if an Payment entity is unique based on certain criteria.
    /// </summary>
    /// <param name="entity">The Payment entity to check for uniqueness.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>True if the Payment is unique; false otherwise.</returns>
    Task<bool> IsUniqueAsync(Payment entity, CancellationToken cancellationToken);
}