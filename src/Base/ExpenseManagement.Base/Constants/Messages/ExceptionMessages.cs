namespace ExpenseManagement.Base.Constants.Messages;

public class ExceptionMessages
{
    public const string UserIsNotUnique = "User has been created before!";
    public const string BlockedUser = "User has been blocked!";
    public const string InvalidCredentials = "Your credentials are invalid!";
    public const string TokenException = "Token can not created!";
    public const string UnexpectedError = "Unexpected error!";
    public const string InternalError = "Internal error!";
    public static readonly Func<string, string> InvalidRoleString 
        = (roleString) => $"Invalid role string: {roleString}";
    public static readonly Func<string, string> InvalidStatusString
        = (statusString) => $"Invalid role string: {statusString}";

}