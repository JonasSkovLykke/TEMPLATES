using ErrorOr;
using MediatR;

namespace SharedKernel.Abstractions;

// Declare a generic interface for defining a query handler that handles a specific type of query.
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
{
    // This interface is generic and used to define query handlers.
    // It specifies the type of query (TQuery) and the response type wrapped in ErrorOr<TResponse>.
    // It is constrained to ensure that TQuery implements IQuery<TResponse>.
    // Query handlers are responsible for handling query requests and returning responses.
}
