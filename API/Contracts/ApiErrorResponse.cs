using Domain.Shared;

namespace API.Contracts;

public class ApiErrorResponse
{
    public ApiErrorResponse(IReadOnlyCollection<Error> errors) => Errors = errors;

    /// <summary>
    /// Gets the errors.
    /// </summary>
    public IReadOnlyCollection<Error> Errors { get; }
}
