
using Application.Abstractions.Contracts;

namespace Application.Ports.Handles;

public interface IQueryHandler<TRequest, TResponse>
    where TRequest : IQuery
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
