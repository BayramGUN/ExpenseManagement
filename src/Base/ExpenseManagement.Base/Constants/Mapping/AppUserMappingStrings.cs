namespace ExpenseManagement.Base.Mapping;

public class AppUserMappingStrings
{
    public static readonly Func<string, string, string> AppUserFullName = 
        (firstName, lastName) => $"{firstName} {lastName}";
    
}