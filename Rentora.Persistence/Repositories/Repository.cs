using Microsoft.EntityFrameworkCore;
using Rentora.Application.Repositories;
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

        public async Task<List<T>> GetAll()
        {
            return _dbSet.ToList();
        }
        public async Task<T> GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public async Task<T> Add(T entity)
        {
            _dbSet.Add(entity);
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if(entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            return false;
        }


        public async  Task<T> Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
    }
}
