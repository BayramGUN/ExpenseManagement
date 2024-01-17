using Microsoft.AspNetCore.Http;

namespace ExpenseManagement.Base.LoggingInformation;

public class LogInformation
{
    public const string StartMessage = "App server is starting.";
    public const string SayHello = "Hello from server";
    public static readonly Func<HttpRequest, string, string> Fatal 
        = (context, exceptionMessage) => (                        
                $"Path={context.Path} || " +                      
                $"Method={context.Method} || " +
                $"Exception={exceptionMessage}");
}