
namespace Rentora.Application.IRepositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}