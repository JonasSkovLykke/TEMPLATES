using ErrorOr;
using FluentValidation; 
using MediatR;

namespace Application.Behaviors;

// Define a sealed class for a validation behavior that implements IPipelineBehavior.
// This behavior is designed to be used in the Mediator pipeline for handling requests and responses.
public sealed class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? _validator = null) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    // The Handle method is required by IPipelineBehavior and is used to perform validation before handling a request.
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // If no validator is provided, proceed to the next step in the pipeline.
        if (_validator is null)
        {
            return await next();
        }

        // Validate the request using the provided validator.
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        // If the validation result is valid, proceed to the next step in the pipeline.
        if (validationResult.IsValid)
        {
            return await next();
        }

        // If there are validation errors, convert them into ErrorOr objects.
        var errors = validationResult.Errors
            .ConvertAll(validationFailure => Error.Validation(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage));

        // Return the errors as an ErrorOr<TResponse>.
        return (dynamic)errors;
    }
}
