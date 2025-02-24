using RentoraAPI.Models;

namespace RentoraAPI.Repository
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        T Add(T entity);
        T Update(T entity);
        bool Delete(int id);
    }
}
