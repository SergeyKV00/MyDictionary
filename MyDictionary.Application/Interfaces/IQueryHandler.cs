using MediatR;
using MyDictionary.Domain.Common;

namespace MyDictionary.Application.Interfaces;

public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result>
    where TQuery : IQuery
{
    Task<Result> Handle(TQuery query, CancellationToken cancellation);
}

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellation);
}
