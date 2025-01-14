using System.Net;
using Newtonsoft.Json;

namespace WebApplication7.Middleware;

public class ExceptionMiddleware(ILogger logger) : IMiddleware
{
    private readonly Dictionary<string, HttpStatusCode> _statusLookup = new()
    {
        { "UnAuthorization", HttpStatusCode.Unauthorized },
    };
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            logger.Information(context.Request.Path);
            await next(context);
        }
        catch (Exception e)
        {
            var errorMessage = e.Message;
            logger.Error(errorMessage);

            context.Response.StatusCode = (int)_statusLookup.GetValueOrDefault(errorMessage, HttpStatusCode.InternalServerError);
            
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new Response
            {
                Message = errorMessage
            }));
        }
    }
}

public class Response
{
    public string Message { get; set; }
}