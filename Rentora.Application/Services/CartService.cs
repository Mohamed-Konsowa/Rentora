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
        public async Task<bool> AddInCartAsync(string userId, int productId)
        {
            if(await _unitOfWork.carts.GetCartAsync(userId, productId) is not null)
            {
                return false; 
            }
            var result = await _unitOfWork.carts.AddAsync(new RentalCart {
                ApplicationUserId = userId,
                ProductId = productId
            });
            await _unitOfWork.SaveChangesAsync();
            return result is not null;
        }

        public async Task<(IReadOnlyCollection<int>, int)> GetUserCartItemsPaginatedAsync(string userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _unitOfWork.carts.PaginateAsync
            (
                pageNumber,
                pageSize,
                c => c.ProductId,
                c => c.ApplicationUserId == userId,
                null,
                null,
                cancellationToken
            );
        }

        public async Task<bool> RemoveFromCartAsync(string userId, int productId)
        {
            var item = await _unitOfWork.carts.GetCartAsync(userId, productId);
            
            if(item is null) return false;
            _unitOfWork.carts.Delete(item.RentalCartId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
