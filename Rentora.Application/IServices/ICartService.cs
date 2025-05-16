namespace Rentora.Application.IServices
{
    public interface ICartService
    {
        Task<bool> AddInCart(string userId, int productId);
        Task<bool> RemoveFromCart(string userId, int productId);
        Task<List<int>> GetUserCartItemsAsync(string userId);

    }
}
