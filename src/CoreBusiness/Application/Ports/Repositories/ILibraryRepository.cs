using Domain.Abstractions.Aggregates;
using System.Linq.Expressions;

namespace Application.Ports.Repositories;

public interface ILibraryRepository
{
    Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : class;

    Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : class;

    Task<TEntity> GetAsync<TEntity>(Guid id, CancellationToken cancellationToken, params Expression<Func<TEntity,object>>[] includes)
        where TEntity : class, IAggregateRoot, new();

    IAsyncEnumerable<TEntity> GetAllAsync<TEntity, TProperty>(params Expression<Func<TEntity, TProperty>>[] includes)
        where TEntity : class, IAggregateRoot;
}
