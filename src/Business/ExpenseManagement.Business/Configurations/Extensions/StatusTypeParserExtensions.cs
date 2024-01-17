using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.Enums;

namespace ExpenseManagement.Business.Configurations.Extensions;

/// <summary>
/// Extension methods for parsing status type from string representations.
/// </summary>
public static class StatusTypeParserExtension
{
    /// <summary>
    /// Parses a string representing a status type into its corresponding integer value.
    /// </summary>
    /// <param name="statusString">The string representation of the status type.</param>
    /// <returns>The integer value of the parsed status type.</returns>
    public static int ParseStatus(this string? statusString)
    {        
        if (Enum.TryParse(statusString, out Status status))
        {
            int statusValue = (int)status;
            return statusValue;
        }
        else
        {
            throw new InvalidDataException(ExceptionMessages.InvalidStatusString(statusString!));
        }
    }
}