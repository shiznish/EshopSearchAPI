using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Application.Core.Abstractions.Behaviors;
internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{

    private readonly ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public RequestLoggingPipelineBehavior(ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        _logger.LogInformation("Processing request {requestName}", requestName);
        TResponse result = await next();
        if (result.IsSuccess)
        {
            _logger.LogInformation("Completed request {requestName}", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                _logger.LogError("Completed request {requestName} with errors", requestName);
            }
        }
        return result;
    }
}
