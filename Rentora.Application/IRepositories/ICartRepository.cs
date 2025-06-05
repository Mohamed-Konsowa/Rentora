using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface ICartRepository : IRepository<RentalCart>
    {
        Task<List<int>> GetUserCartItemsAsync(string userId);
        Task<RentalCart> GetCartAsync(string userId, int productId);
    }
}
