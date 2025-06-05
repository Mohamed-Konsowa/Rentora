using Rentora.Application.IRepositories;
using Rentora.Application.IServices;
using Rentora.Domain.Models;

namespace Rentora.Application.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddInFavoriteAsync(string userId, int productId)
        {
            if(_unitOfWork.favorites.GetFavoriteAsync(userId, productId) is not null)
            {
                return false; 
            }
            var result = await _unitOfWork.favorites.AddAsync(new Favorite {
                ApplicationUserId = userId,
                ProductId = productId
            });
            await _unitOfWork.SaveChangesAsync();
            return result is not null;
        }

        public async Task<List<int>> GetUserFavoriteItemsAsync(string userId)
        {
            return await _unitOfWork.favorites.GetUserFavoriteItemsAsync(userId);
        }

        public async Task<bool> RemoveFromFavoriteAsync(string userId, int productId)
        {
            var item = await _unitOfWork.favorites.GetFavoriteAsync(userId, productId);
            
            if(item is null) return false;
            _unitOfWork.favorites.Delete(item.FavoriteId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
