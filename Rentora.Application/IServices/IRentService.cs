using Rentora.Application.DTOs.Rental;

namespace Rentora.Application.IServices
{
    public interface IRentService
    {
        Task<List<int>> GetUserRentsAsync(string userId);
        Task<bool> RentProduct(RentProductDTO rentProductDTO);
    }
}
