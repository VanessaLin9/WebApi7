namespace WebApplication7.Filter;

public class AuthorizationFilter(AuthConfig authConfig) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue("Auth", out var token))
        {
            throw new Exception("UnAuthorization");
        }

        if (token != authConfig.signature)
        {
            throw new Exception("InvalidUser");
        }

        return await next(context);
    }
}