namespace Rentora.Application.IServices
{
    public interface ICartService
    {
        Task<bool> AddInCartAsync(string userId, int productId);
        Task<bool> RemoveFromCartAsync(string userId, int productId);
        Task<List<int>> GetUserCartItemsAsync(string userId);

    }
}
