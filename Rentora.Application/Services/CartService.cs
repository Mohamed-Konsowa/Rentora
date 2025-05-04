using Rentora.Application.IServices;
using Rentora.Application.IRepositories;
using Rentora.Domain.Models;

namespace Rentora.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddInCart(string userId, int productId)
        {
            if(_unitOfWork.carts.GetCart(userId, productId) is not null)
            {
                return false; 
            }
            var result = await _unitOfWork.carts.AddAsync(new RentalCart {
                ApplicationUserId = userId,
                ProductId = productId
            });
            await _unitOfWork.Save();
            return result is not null;
        }

        public List<int> GetUserCartItems(string userId)
        {
            return _unitOfWork.carts.GetUserCartItems(userId);
        }

        public async Task<bool> RemoveFromCart(string userId, int productId)
        {
            var item = _unitOfWork.carts.GetCart(userId, productId);
            
            if(item is null) return false;
            _unitOfWork.carts.Delete(item.RentalCartId);
            await _unitOfWork.Save();
            return true;
        }
    }
}
