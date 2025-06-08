
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Rentora.Application.IRepositories
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAllAsNoTracking();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        bool Delete(int id);
        Task<(IReadOnlyCollection<TSelctor>, int count)> PaginateAsync<TSelctor>(
            int pageNumber,
            int pageSize,
            Expression<Func<TEntity, TSelctor>> Selctor,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            Expression<Func<TEntity, object>> ordering = null!,
            CancellationToken cancellationToken = default
        );
    }
}