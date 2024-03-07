using Application.Ports.Repositories;
using Domain.Abstractions.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.SqlServer.Databases;

public class LibraryRepository : ILibraryRepository
{
    private readonly DbContext _dbContext;

    public LibraryRepository(DbContext dbContext)
        => _dbContext = dbContext;

    public async Task InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : class
    {
        await _dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
        where TEntity : class
    {
        _dbContext.Set<TEntity>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity> GetAsync<TEntity>(Guid id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includes)
        where TEntity : class, IAggregateRoot, new()
    {
        var context = _dbContext
           .Set<TEntity>();

        IQueryable<TEntity> query = context;

        if(includes is not null)
            query = includes.Aggregate(query, (current, include) => current.Include(include));

        return await query.FirstOrDefaultAsync(aggregate => aggregate.Id == id, cancellationToken) ?? new TEntity();
    }

    public IAsyncEnumerable<TEntity> GetAllAsync<TEntity, TProperty>(params Expression<Func<TEntity, TProperty>>[] includes)
        where TEntity : class, IAggregateRoot
    {
        var context = _dbContext
            .Set<TEntity>();

        foreach (var include in includes)
        {
            context.Include(include);
        }


        return context.AsAsyncEnumerable();
    }
}
