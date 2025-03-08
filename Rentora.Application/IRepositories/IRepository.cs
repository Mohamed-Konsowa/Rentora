using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<bool> Delete(int id);
    }
}
