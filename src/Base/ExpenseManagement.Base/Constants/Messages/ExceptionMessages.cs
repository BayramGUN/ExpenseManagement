using System.Linq.Expressions;

namespace ExpenseManagement.Base.Constants.Messages;

public class ExceptionMessages
{
    public const string UserIsNotUnique = "User has been created before!";
    public const string PaymentCouldNotBeAble = "Some problem with the payment system!";
    public const string ExpenseHasNotUniqueId = "There is a expense with same id. That is our problem sorry!";
    public const string PaymentHasNotUniqueId = "There is a payment with same id. That is our problem sorry!";
    public const string ExpenseApprovalHasNotUniqueId = "There is a expenseApproval with same id. That is our problem sorry!";
    public const string BlockedUser = "User has been blocked!";
    public const string NotBlockedUser = "User is not blocked!";
    public const string DeletedUser = "User has deleted after 3 wrong password entering!";
    public const string InvalidCredentials = "Your credentials are invalid!";
    public const string TokenException = "Token can not created!";
    public const string UnexpectedError = "Unexpected error!";
    public const string InternalError = "Internal error!";
    public const string NullConnectionString = "Connection string is null!";
    public static readonly Func<int, string> NotFound 
        = (thing) => $"{thing} can not found!";
    public static readonly Func<string, string> InvalidRoleString 
        = (roleString) => $"Invalid role string: {roleString}";
    public static readonly Func<string, string> InvalidStatusString
        = (statusString) => $"Invalid role string: {statusString}";
    public static readonly Func<object, string> NotFoundWithParameter 
        = (thing) => $"{thing} can not found!";

}