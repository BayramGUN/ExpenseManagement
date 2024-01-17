using System.Net;
using System.Text.Json;
using ExpenseManagement.Base.Constants.ContentTypes;
using ExpenseManagement.Base.Constants.Messages;
using ExpenseManagement.Base.LoggingInformation;
using Serilog;

namespace ExpenseManagement.Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch(Exception exception)
        {
            Log.Error(exception, ExceptionMessages.UnexpectedError);
            Log.Fatal(LogInformation.Fatal(context.Request, exception.Message));  
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = ResponseContentType.ApplicationJsonType;
            await context.Response.WriteAsync(JsonSerializer.Serialize(ExceptionMessages.InternalError));
        }
    }
}