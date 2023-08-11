using ChatAI.Domain.Entities;
using System.Linq.Expressions;

namespace ChatAI.Application.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<IList<TEntity>> GetAll(
        Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        List<string>? includes = null);
    Task<IList<TEntity>> GetAllPaged(
        int skip,
        int take,
        Expression<Func<TEntity, bool>>? expression = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        List<string>? includes = null);
    Task<TEntity?> Get(Expression<Func<TEntity, bool>> expression, List<string>? includes = null);
    Task<TEntity?> Get(Guid id, List<string>? includes = null);
    Task Insert(TEntity entity);
    Task Delete(params object?[]? keyValues);
    Task Delete(TEntity entityToDelete);
    Task Delete(Expression<Func<TEntity, bool>> expression);
    Task Update(TEntity entity);
    Task UpdateRange(IEnumerable<TEntity> entities);
    Task InsertRange(IEnumerable<TEntity> entities);
    Task DeleteRange(IEnumerable<TEntity> entities);
    Task<int> Count();
    Task<int> Count(Expression<Func<TEntity, bool>> expression);
    Task Save();
}
