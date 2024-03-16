using Serilog.Context;

namespace API.Middleware;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class RequestLogContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext httpContext)
    {
        using (LogContext.PushProperty("CorrelationId", httpContext.TraceIdentifier))
        {
            return _next(httpContext);
        }
    }
}

