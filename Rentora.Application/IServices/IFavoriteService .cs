namespace Rentora.Application.IServices
{
    public interface IFavoriteService
    {
        Task<bool> AddInFavoriteAsync(string userId, int productId);
        Task<bool> RemoveFromFavoriteAsync(string userId, int productId);
        Task<List<int>> GetUserFavoriteItemsAsync(string userId);

    }
}
