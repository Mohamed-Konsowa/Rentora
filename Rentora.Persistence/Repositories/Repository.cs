using Microsoft.EntityFrameworkCore;
using Rentora.Application.IRepositories;
using Rentora.Persistence.Data.DbContext;

namespace Rentora.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public bool Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if(entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
    }
}
