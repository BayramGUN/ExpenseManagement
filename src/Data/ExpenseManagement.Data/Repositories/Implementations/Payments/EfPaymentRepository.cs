using ExpenseManagement.Base.Enums;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Interfaces.Payments;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Data.Repositories.Implementations.Expenses;

/// <summary>
/// Repository implementation for interacting with Payment entities in the database.
/// </summary>
public class EfPaymentRepository : IEfPaymentRepository
{
    private readonly EfExpenseManagementDbContext dbContext;
    
    /// <summary>
    /// Initializes a new instance of the PaymentRepository class.
    /// </summary>
    /// <param name="dbContext">The DbContext for interacting with the database.</param>
    public EfPaymentRepository(EfExpenseManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    ///<summary>
    /// Get all Payments from dbContext.
    /// </summary>
    public async Task<List<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken) =>
        await dbContext.Set<Payment>()
                       .Include(x => x.Expense)
                       .ThenInclude(e => e!.AppUser)
                       .ToListAsync(cancellationToken);
    

    ///<summary>
    /// Soft deletes an Payment by setting the IsActive flag to false.
    /// </summary>
    /// <param name="id">The ID of the Payment to delete.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task DeletePaymentAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Payment>()
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync(cancellationToken);
        entity!.IsActive = false;
        dbContext.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves an Payment from the repository based on the specified parameters.
    /// </summary>
    /// <param name="id">The ID of the Payment to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>The retrieved Payment.</returns>
    public async Task<Payment> GetPaymentByIdAsync(
        int id,
        CancellationToken cancellationToken) =>
            await dbContext.Set<Payment>()
                .Include(x => x.Expense)
                .ThenInclude(e => e!.AppUser)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync((CancellationToken)cancellationToken!) ?? default!;
    /// <summary>
    /// Updates an existing Payment in the repository.
    /// </summary>
    /// <param name="Payment">The Payment entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task<Payment> UpdatePaymentAsync(
        Payment entity, 
        CancellationToken cancellationToken)
    {
        var result = dbContext.Update(entity);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Creates a new Payment in the repository.
    /// </summary>
    /// <param name="entity">The Payment entity to be added to the repository.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>The added Payment entity.</returns>
    public async Task<Payment> CreatePaymentAsync(
        Payment entity, 
        CancellationToken cancellationToken)
    {
        
        var result = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    /// <summary>
    /// Checks if an Payment entity is unique based on certain criteria.
    /// </summary>
    /// <param name="entity">The Payment entity to check for uniqueness.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>True if the Payment is unique; false otherwise.</returns>
    public async Task<bool> IsUniqueAsync(
        Payment entity, 
        CancellationToken cancellationToken) 
            => await dbContext.Set<Payment>()
                              .Where(x => x.Id == entity.Id)
                              .FirstOrDefaultAsync(cancellationToken) is null;

    /// <summary>
    /// Filters a list of Payments based on optional parameters such as first name, last name, role, activity status, and user status.
    /// </summary>
    /// <param name="amount">Optional parameter for filtering by Payments amount.</param>
    /// <param name="paymentDate">Optional parameter for filtering by Payments Payment date.</param>
    /// <returns>A list of filtered Payments.</returns>
    public async Task<List<Payment>> FilterPaymentsByParameterAsync(
        DateTime? paymentDate = null, 
        decimal? amount = null, 
        string? paymentMethod = null, 
        int? appUserId = null,
        CancellationToken? cancellationToken = null)
    {
               var query = dbContext.Set<Payment>()
                             .Include(x => x.Expense)
                             .ThenInclude(e => e!.AppUser)
                             .AsQueryable();

        if (paymentDate is not null)
            query = query.Where(x => 
                (x.PaymentDate.Year == paymentDate.Value.Year) && 
                (x.PaymentDate.Month == paymentDate.Value.Month) && 
                (x.PaymentDate.Day == paymentDate.Value.Day));
   
        if (amount is not null)
            query = query.Where(x => x.Amount == amount);

        if (paymentMethod is not null)
            query = query.Where(x => x.PaymentMethod == paymentMethod);

        if (appUserId is not null)
            query = query.Where(x => x.Expense!.AppUserId == appUserId);

        return await query.ToListAsync(cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Retrieves a list of Payments associated with a specific application expense ID.
    /// </summary>
    /// <param name="expenseId">The ID of the application expense for whom Payments are retrieved.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A list of Payments associated with the specified application expense ID.</returns>
    public async Task<Payment> GetPaymentByExpenseIdAsync(
        int expenseId, 
        CancellationToken cancellationToken) => 
            await dbContext.Set<Payment>()
                           .Include(x => x.Expense)
                           .ThenInclude(e => e!.AppUser)
                           .Where(x => x.ExpenseId == expenseId)
                           .FirstOrDefaultAsync(cancellationToken) ?? null!;

}