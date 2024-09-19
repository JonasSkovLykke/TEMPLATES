using ErrorOr;
using MediatR;

namespace SharedKernel.Abstractions;

// Declare a generic interface for defining query messages with a response type.
public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
    // This interface is generic and used to define query messages.
    // It specifies a response type wrapped in ErrorOr<TResponse>.
    // Query messages are typically used to retrieve data or information from the application.
}
