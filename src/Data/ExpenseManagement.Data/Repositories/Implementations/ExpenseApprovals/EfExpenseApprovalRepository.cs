using ExpenseApprovalManagement.Data.Repositories.Interfaces.Expenses;
using ExpenseManagement.Base.Enums;
using ExpenseManagement.Data.DbContexts;
using ExpenseManagement.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseManagement.Data.Repositories.Implementations.Expenses;

/// <summary>
/// Repository implementation for interacting with ExpenseApproval entities in the database.
/// </summary>
public class EfExpenseApprovalRepository : IEfExpenseApprovalRepository
{
    private readonly EfExpenseManagementDbContext dbContext;

    /// <summary>
    /// Initializes a new instance of the ExpenseApprovalRepository class.
    /// </summary>
    /// <param name="dbContext">The DbContext for interacting with the database.</param>
    public EfExpenseApprovalRepository(EfExpenseManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    /// <summary>
    /// Gets all ExpenseApprovals from the DbContext.
    /// </summary>
    public async Task<List<ExpenseApproval>> GetAllExpenseApprovalsAsync(CancellationToken cancellationToken) =>
        await dbContext.Set<ExpenseApproval>()
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.FirstName)
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.LastName)
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.IBAN)
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.Id)
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.IdentityNumber)
                       .Include(x => x.Expense)
                       .ToListAsync(cancellationToken);
    /// <summary>
    /// Soft deletes an ExpenseApproval by setting the IsActive flag to false.
    /// </summary>
    /// <param name="id">The ID of the ExpenseApproval to delete.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task DeleteExpenseApprovalAsync(int id, CancellationToken cancellationToken)
    {
        var expenseApproval = await dbContext.Set<ExpenseApproval>()
                                             .Where(x => x.Id == id)
                                             .FirstOrDefaultAsync(cancellationToken);
        expenseApproval!.IsActive = false;
        dbContext.Update(expenseApproval);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Retrieves an ExpenseApproval from the repository based on the specified parameters.
    /// </summary>
    /// <param name="id">The ID of the ExpenseApproval to retrieve.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>The retrieved ExpenseApproval.</returns>
    public async Task<ExpenseApproval> GetExpenseApprovalById(int id, CancellationToken cancellationToken) =>
        await dbContext.Set<ExpenseApproval>()
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.FirstName)
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.LastName)
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.IBAN)
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.Id)
                       .Include(x => x.Approver)
                       .ThenInclude(a => a!.IdentityNumber)
                       .Include(x => x.Expense)
                       .Where(x => x.Id == id)
                       .FirstOrDefaultAsync((CancellationToken)cancellationToken!) ?? default!;
    /// <summary>
    /// Updates an existing ExpenseApproval in the repository.
    /// </summary>
    /// <param name="expenseApproval">The ExpenseApproval entity to update.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    public async Task<ExpenseApproval> UpdateExpenseApprovalAsync(ExpenseApproval expenseApproval, CancellationToken cancellationToken)
    {
        var result = dbContext.Update(expenseApproval);
        await dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Creates a new ExpenseApproval in the repository.
    /// </summary>
    /// <param name="entity">The ExpenseApproval entity to be added to the repository.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>The added ExpenseApproval entity.</returns>
    public async Task<ExpenseApproval> CreateExpenseApprovalAsync(ExpenseApproval entity, CancellationToken cancellationToken)
    {
        var result = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Checks if an ExpenseApproval entity is unique based on certain criteria.
    /// </summary>
    /// <param name="entity">The ExpenseApproval entity to check for uniqueness.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>True if the ExpenseApproval is unique; false otherwise.</returns>
    public async Task<bool> IsUniqueAsync(ExpenseApproval entity, CancellationToken cancellationToken) =>
        await dbContext.Set<ExpenseApproval>()
                      .Where(x => x.Id == entity.Id)
                      .FirstOrDefaultAsync(cancellationToken) is null;
                      
    /// <summary>
    /// Filters a list of ExpenseApprovals based on optional parameters such as status and approver ID.
    /// </summary>
    /// <param name="status">Optional parameter for filtering by expenseApprovals status.</param>
    /// <param name="approverId">Optional parameter for filtering by approver ID.</param>
    /// <param name="cancellationToken">The cancellation token for handling asynchronous operations.</param>
    /// <returns>A list of filtered ExpenseApprovals.</returns>
    public async Task<List<ExpenseApproval>> FilterExpenseApprovalsByParameter(
        Status? status = null,
        int? approverId = null,
        CancellationToken? cancellationToken = null)
    {
        var query = dbContext.Set<ExpenseApproval>()
                             .Include(x => x.Approver)
                             .ThenInclude(a => a!.FirstName)
                             .Include(x => x.Approver)
                             .ThenInclude(a => a!.LastName)
                             .Include(x => x.Approver)
                             .ThenInclude(a => a!.IBAN)
                             .Include(x => x.Approver)
                             .ThenInclude(a => a!.Id)
                             .Include(x => x.Approver)
                             .ThenInclude(a => a!.IdentityNumber)
                             .Include(x => x.Expense)
                             .AsQueryable();
        if (status is not null)
            query = query.Where(x => x.ApprovalStatus == status);
        if (approverId is not null)
            query = query.Where(x => x.ApproverId == approverId);
        return await query.ToListAsync(cancellationToken ?? CancellationToken.None);
    }
}
