using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface IRentRepository : IRepository<Rental>
    {
        Task<List<int>> GetUserRents(string userId);
    }
}
