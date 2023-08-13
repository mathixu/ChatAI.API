using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ChatAI.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ChatAIDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(ChatAIDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<int> Count()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<int> Count(Expression<Func<TEntity, bool>> expression)
    {
        return await _dbSet.CountAsync(expression);
    }

    public async Task Delete(params object?[]? keyValues)
    {
        var entity = await _dbSet.FindAsync(keyValues);

        if (entity is not null)
        {
            await Delete(entity);
        }
    }

    public async Task Delete(TEntity entityToDelete)
    {
        _dbSet.Remove(entityToDelete);

        await Save();
    }

    public async Task Delete(Expression<Func<TEntity, bool>> expression)
    {
        var entities = await GetAll(expression);

        if (entities is not null)
        {
            await DeleteRange(entities);
        }
    }

    public async Task DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);

        await Save();
    }

    public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression, List<string>? includes = null)
    {
        var query = _dbSet.AsQueryable();

        if (includes is not null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        return await query.FirstOrDefaultAsync(expression);
    }

    public Task<TEntity?> Get(Guid id, List<string>? includes = null)
    {
        return Get(x => x.Id == id, includes);
    }

    public async Task<IList<TEntity>> GetAll(Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, List<string>? includes = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (expression is not null)
        {
            query = query.Where(expression);
        }

        if (includes is not null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<IList<TEntity>> GetAllPaged(int skip, int take, Expression<Func<TEntity, bool>>? expression = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, List<string>? includes = null)
    {
        IQueryable<TEntity> query = _dbSet;

        if (expression is not null)
        {
            query = query.Where(expression);
        }

        if (includes is not null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        query = query.Skip(skip).Take(take);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task Insert(TEntity entity)
    {
        await _dbSet.AddAsync(entity);

        await Save();
    }

    public async Task InsertRange(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);

        await Save();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    public async Task Update(TEntity entity)
    {
        _dbSet.Update(entity);

        await Save();
    }

    public async Task UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);

        await Save();
    }
}
