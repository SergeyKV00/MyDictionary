using MyDictionary.Domain;

namespace MyDictionary.Api.Middleware;

public class SessionContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, SessionContext sessionContext)
    {
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var claims = context.User.Claims;
            sessionContext.Init(claims);
        }

        await next(context);
    }
}
