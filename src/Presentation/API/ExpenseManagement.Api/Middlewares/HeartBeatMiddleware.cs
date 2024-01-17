using System.Net;
using System.Net.Mime;
using System.Text.Json;
using ExpenseManagement.Base.Constants.ContentTypes;
using ExpenseManagement.Base.LoggingInformation;
using ExpenseManagement.Base.Routes;

namespace ExpenseManagement.Api.Middlewares;

public class HeartBeatMiddleware
{
    private readonly RequestDelegate next;

    public HeartBeatMiddleware(RequestDelegate next)
    {
        this.next = next;
    }


    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments(EndpointRoute.Hello))
        {
            context.Response.ContentType = ResponseContentType.ApplicationJsonType;
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            await context.Response.WriteAsync(JsonSerializer.Serialize(LogInformation.SayHello));
            return;
        }

        await next.Invoke(context);
    }
}