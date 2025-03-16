using Rentora.Domain.Models;

namespace Rentora.Presentation.Services
{
    public interface ICartService
    {
        Task<bool> AddInCart(string userId, int productId);
        Task<bool> RemoveFromCart(string userId, int productId);
        List<int> GetUserCartItems(string userId);

    }
}
