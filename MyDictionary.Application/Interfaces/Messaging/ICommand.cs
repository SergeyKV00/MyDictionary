using MediatR;
using MyDictionary.Domain.Common;

namespace MyDictionary.Application.Interfaces.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

