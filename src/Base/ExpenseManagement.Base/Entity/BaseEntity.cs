namespace ExpenseManagement.Base.Entity;


/// <summary>
/// Contains base entity classes for modeling data entities with common properties 
/// such as insertion and update metadata.
/// </summary>
public abstract class BaseEntityWithId : BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entities.
    /// </summary>
    public required int Id { get; set; }
}
public abstract class BaseEntity
{
    public required int InsertUserId { get; set; }
    public required DateTime InsertDate { get; set; }
    public int? UpdateUserId { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsActive { get; set; }
}