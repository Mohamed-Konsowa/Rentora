using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface ICartRepository : IRepository<RentalCart>
    {
        Task<List<int>> GetUserCartItems(string userId);
        Task<RentalCart> GetCart(string userId, int productId);
    }
}
