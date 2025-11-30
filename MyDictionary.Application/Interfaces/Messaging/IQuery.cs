using MediatR;
using MyDictionary.Domain.Common;

namespace MyDictionary.Application.Interfaces.Messaging;

public interface IQuery : IRequest<Result>
{
}

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

public interface IQueryPages<TResponse> : IQuery<TResponse>
{
    public int Page { get; }
    public int PageSize { get; }
}

