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

        using (LogContext.PushProperty("ClientIp", GenerateIPAddress(httpContext)))
        {
            return _next(httpContext);
        }

    }

    private string GenerateIPAddress(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            return httpContext.Request.Headers["X-Forwarded-For"];
        else
            return httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
    }


}

