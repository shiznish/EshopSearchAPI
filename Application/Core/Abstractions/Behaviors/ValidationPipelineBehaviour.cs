﻿using Application.Core.Abstractions.Messaging;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.Core.Abstractions.Behaviors;
internal sealed class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
 where TRequest : class, IRequest<TResponse>
 where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is IQuery<TResponse>)
        {
            return await next();
        }
        var context = new ValidationContext<TRequest>(request);

        List<ValidationFailure> failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
