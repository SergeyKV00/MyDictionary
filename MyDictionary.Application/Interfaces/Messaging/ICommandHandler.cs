using MediatR;
using MyDictionary.Domain.Common;

namespace MyDictionary.Application.Interfaces.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
    Task<Result> Handle(TCommand command, CancellationToken cancellation);
}

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellation);
}
