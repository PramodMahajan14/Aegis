using Adveshta.Utility.Common;
using FluentValidation;
using MediatR;

namespace Adveshta.Services.Behaviors
{
    /// <summary>
    /// MediatR pipeline behavior that runs all registered FluentValidation
    /// validators before a command/query handler executes.
    /// Returns a structured <see cref="ApiResponse{T}"/> with validation errors
    /// instead of throwing an exception, keeping the API surface consistent.
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count == 0)
                return await next();

            // Build a grouped error dictionary: field -> [messages]
            var errors = failures
                .GroupBy(f => f.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            // Try to cast TResponse to ApiResponse<object> and return it.
            // This works for any command that returns ApiResponse<T>.
            var responseType = typeof(TResponse);

            if (responseType.IsGenericType &&
                responseType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
            {
                var innerType = responseType.GetGenericArguments()[0];
                var errorResponseMethod = typeof(ApiResponse<>)
                    .MakeGenericType(innerType)
                    .GetMethod(nameof(ApiResponse<object>.ErrorResponse));

                var result = errorResponseMethod!.Invoke(
                    null,
                    new object?[] { "Validation failed.", errors, StatusCodes.Status422UnprocessableEntity });

                return (TResponse)result!;
            }

            // Fallback: throw a validation exception (caught by global middleware)
            throw new ValidationException(failures);
        }
    }
}
