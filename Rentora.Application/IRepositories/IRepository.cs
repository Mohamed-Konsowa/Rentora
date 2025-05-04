
namespace Rentora.Application.IRepositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
