using ExpenseManagement.Base.Enums;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.Entities;
using ExpenseManagement.Data.Repositories.Interfaces.Expenses;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Data.Repositories.Implementations.Expenses;

/// <summary>
/// Repository implementation for interacting with Expense entities in the database.
/// </summary>
public class EfExpenseRepository : IEfExpenseRepository
{
    private readonly EfExpenseManagementDbContext dbContext;
    
    /// <summary>
    /// Initializes a new instance of the ExpenseRepository class.
    /// </summary>
    /// <param name="dbContext">The DbContext for interacting with the database.</param>
    public EfExpenseRepository(EfExpenseManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    ///<summary>
    /// Get all Expenses from dbContext.
    /// </summary>
    public async Task<List<Expense>> GetAllExpensesAsync(CancellationToken cancellationToken) =>
        await dbContext.Set<Expense>()
                       .Include(x => x.AppUser)
                       .Include(x => x.ExpenseApprovals)
                       .ToListAsync(cancellationToken);
    

    ///<summary>
    /// Soft deletes an Expense by setting the IsActive flag to false.
    /// </summary>
    /// <param name="id">The ID of the Expense to delete.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task DeleteExpenseAsync(
        int id, 
        CancellationToken cancellationToken)
    {
        var expense = await dbContext.Set<Expense>()
                                     .Where(x => x.Id == id)
                                     .FirstOrDefaultAsync(cancellationToken);
        expense!.IsActive = false;
        dbContext.Update(expense);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves an Expense from the repository based on the specified parameters.
    /// </summary>
    /// <param name="id">The ID of the Expense to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>The retrieved Expense.</returns>
    public async Task<Expense> GetExpenseByIdAsync(
        int id,
        CancellationToken cancellationToken) =>
            await dbContext.Set<Expense>()
                .Include(x => x.AppUser)
                .Include(x => x.ExpenseApprovals)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync((CancellationToken)cancellationToken!) ?? default!;
    /// <summary>
    /// Updates an existing Expense in the repository.
    /// </summary>
    /// <param name="Expense">The Expense entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task<Expense> UpdateExpenseAsync(
        Expense expense, 
        CancellationToken cancellationToken)
    {
        var result = dbContext.Update(expense);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Creates a new Expense in the repository.
    /// </summary>
    /// <param name="entity">The Expense entity to be added to the repository.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>The added Expense entity.</returns>
    public async Task<Expense> CreateExpenseAsync(
        Expense entity, 
        CancellationToken cancellationToken)
    {
        
        var result = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }
    
    /// <summary>
    /// Checks if an Expense entity is unique based on certain criteria.
    /// </summary>
    /// <param name="entity">The Expense entity to check for uniqueness.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>True if the Expense is unique; false otherwise.</returns>
    public async Task<bool> IsUniqueAsync(
        Expense entity, 
        CancellationToken cancellationToken) 
            => await dbContext.Set<Expense>()
                              .Where(x => x.Id == entity.Id)
                              .FirstOrDefaultAsync(cancellationToken) is null;

    /// <summary>
    /// Filters a list of Expenses based on optional parameters such as first name, last name, role, activity status, and user status.
    /// </summary>
    /// <param name="amount">Optional parameter for filtering by expenses amount.</param>
    /// <param name="expensedDate">Optional parameter for filtering by expenses expensed date.</param>
    /// <param name="status">Optional parameter for filtering by expenses status.</param>
    /// <returns>A list of filtered Expenses.</returns>
    public async Task<List<Expense>> FilterExpensesByParameterAsync(
        Status? status = null,
        DateTime? expensedDate = null,
        DateTime? fromExpensedDate = null,
        DateTime? toExpensedDate = null,
        decimal? amount = null,
        CancellationToken? cancellationToken = null)
    {
        var query = dbContext.Set<Expense>()
                             .Include(x => x.AppUser)
                             .Include(x => x.ExpenseApprovals)
                             .AsQueryable();

        if (status is not null)
            query = query.Where(x => x.Status == status);

        if (expensedDate is not null)
            query = query.Where(x => 
                (x.ExpensedDate.Year == expensedDate.Value.Year) && 
                (x.ExpensedDate.Month == expensedDate.Value.Month) && 
                (x.ExpensedDate.Day == expensedDate.Value.Day));

        if(fromExpensedDate is not null)
            query = query.Where(x => x.ExpensedDate > fromExpensedDate);

        if(toExpensedDate is not null)
            query = query.Where(x => x.ExpensedDate < toExpensedDate);

        //if(toExpensedDate is not null && fromExpensedDate is not null)
            //query = query.Where(x => x.ExpensedDate > fromExpensedDate &&
               //                 x.ExpensedDate < toExpensedDate);

        if (amount is not null)
            query = query.Where(x => x.Amount == amount);

        return await query.ToListAsync(cancellationToken ?? CancellationToken.None);
    }

    /// <summary>
    /// Retrieves a list of expenses associated with a specific application user ID.
    /// </summary>
    /// <param name="appUserId">The ID of the application user for whom expenses are retrieved.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A list of expenses associated with the specified application user ID.</returns>
    public async Task<List<Expense>> GetExpenseByAppUserIdAsync(
        int appUserId, 
        CancellationToken cancellationToken) => 
            await dbContext.Set<Expense>()
                           .Include(x => x.AppUser)
                           .Include(x => x.ExpenseApprovals)
                           .AsQueryable()
                           .Where(x => x.AppUserId == appUserId)
                           .ToListAsync();
}