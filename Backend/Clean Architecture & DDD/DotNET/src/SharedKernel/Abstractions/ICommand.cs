using ErrorOr;
using MediatR;

namespace SharedKernel.Abstractions;

// Declare an interface for defining command messages.
public interface ICommand : IRequest
{
    // This is the base interface for command messages in the Mediator pattern.
}

// Declare a generic interface for defining command messages with a response type.
public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
    // This interface extends ICommand and specifies a response type wrapped in ErrorOr.
}
