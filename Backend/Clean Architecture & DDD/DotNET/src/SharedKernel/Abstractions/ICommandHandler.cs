using ErrorOr;
using MediatR;

namespace SharedKernel.Abstractions;

// Declare an interface for defining a command handler that handles a specific type of command.
// This interface is used to handle commands without a response.
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
    // This interface is generic and used to handle commands of type TCommand.
    // It is constrained to ensure that TCommand implements ICommand.
}

// Declare a generic interface for defining a command handler with a response type.
// This interface is used to handle commands with a response wrapped in ErrorOr.
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, ErrorOr<TResponse>>
    where TCommand : ICommand<TResponse>
{
    // This interface is generic and used to handle commands of type TCommand.
    // It is constrained to ensure that TCommand implements ICommand<TResponse>.
    // The response type is ErrorOr<TResponse>.
}
