using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Rentora.Application.IRepositories;
using Rentora.Persistence.Data.DbContext;
using System.Linq.Expressions;

namespace Rentora.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _entities.AsQueryable();
        }

        public IQueryable<TEntity> GetAllAsNoTracking()
        {
            return _entities.AsQueryable().AsNoTracking();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _entities.Find(id);
            if(entity != null)
            {
                _entities.Remove(entity);
                return true;
            }
            return false;
        }

        public TEntity Update(TEntity entity)
        {
            _entities.Update(entity);
            return entity;
        }

        public async Task<(IReadOnlyCollection<TSelctor>, int count)> PaginateAsync<TSelctor>(
            int pageNumber,
            int pageSize,
            Expression<Func<TEntity, TSelctor>> Selctor,
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            Expression<Func<TEntity, object>> ordering = null!,
            CancellationToken cancellationToken = default
        )
        {
            var query = _entities.AsQueryable().AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includes != null)
                query = includes(query);

            int count = query.Count();

            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (ordering != null)
                query = query.OrderByDescending(ordering);

            return (await query.Select(Selctor).ToListAsync(cancellationToken), count);
        }
    }
}
