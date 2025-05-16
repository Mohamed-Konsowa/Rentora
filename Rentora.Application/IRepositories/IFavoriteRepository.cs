using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface IFavoriteRepository : IRepository<Favorite>
    {
        Task<List<int>> GetUserFavoriteItemsAsync(string userId);
        Task<Favorite> GetFavoriteAsync(string userId, int productId);
    }
}
