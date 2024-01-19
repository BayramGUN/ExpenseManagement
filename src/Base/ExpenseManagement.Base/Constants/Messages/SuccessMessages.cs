using System.Linq.Expressions;

namespace ExpenseManagement.Base.Constants.Messages;

public class SuccessMessages
{
    public static readonly Func<int, string> DeletedSuccess 
        = (id) => $"{id} is deleted successfully!";
    public static readonly Func<string, string> UpdatedSuccess 
        = (id) => $"{id} is updated successfully!";
    public static readonly Func<string, string> CreatedSuccess 
        = (item) => $"{item} is created successfully!";
   
}