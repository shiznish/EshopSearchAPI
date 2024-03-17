using API.Contracts;
using Application.Core.Exceptions;
using Domain.Shared;
using System.Net;
using System.Text.Json;

namespace API.Middleware;

internal class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this._next = next;
        this._logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            if (ex is ValidationException validationException)
            {
                _logger.LogError(ex, "An exception occurred: {Message} {@Errors} {@ex}",
                    ex.Message,
                    validationException.Errors,
                    validationException);
            }
            else
            {
                _logger.LogError(ex, "An exception occurred: {Message}", ex.Message);
            }

            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error> errors) = GetHttpStatusCodeAndErrors(exception);

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = (int)httpStatusCode;

        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        string response = JsonSerializer.Serialize(new ApiErrorResponse(errors), serializerOptions);

        await httpContext.Response.WriteAsync(response);
    }
    private static (HttpStatusCode httpStatusCode, IReadOnlyCollection<Error>) GetHttpStatusCodeAndErrors(Exception exception) =>
           exception switch
           {
               ValidationException validationException => (HttpStatusCode.BadRequest, validationException.Errors),
               Domain.Exceptions.DomainException domainException => (HttpStatusCode.BadRequest, new[] { domainException.Error }),
               _ => (HttpStatusCode.InternalServerError, new[] { Domain.Errors.DomainErrors.General.ServerError })
           };
}

