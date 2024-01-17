using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Business.Configurations.Extensions;

/// <summary>
/// Extension methods for parsing user roles from string representations.
/// </summary>
public static class RoleTypeParserExtension
{
    /// <summary>
    /// Parses a string representing a user role into its corresponding integer value.
    /// </summary>
    /// <param name="roleString">The string representation of the user role.</param>
    /// <returns>The integer value of the parsed user role.</returns>
    public static int ParseUserRole(this string? roleString)
    {        
        if (Enum.TryParse(roleString, out UserRole userRole))
        {
            int userRoleValue = (int)userRole;
            return userRoleValue;
        }
        else
        {
            throw new InvalidDataException(ExceptionMessages.InvalidRoleString(roleString!));
        }
    }
}