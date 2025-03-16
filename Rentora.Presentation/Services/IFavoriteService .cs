namespace Rentora.Presentation.Services
{
    public interface IFavoriteService
    {
        Task<bool> AddInFavorite(string userId, int productId);
        Task<bool> RemoveFromFavorite(string userId, int productId);
        List<int> GetUserFavoriteItems(string userId);

    }
}
