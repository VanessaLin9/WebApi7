namespace WebApplication7.Filter;

public class AuthorizationFilter: IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Auth", out var token))
        {
            throw new Exception("UnAuthorization");
        }

        return await next(context);
    }
}