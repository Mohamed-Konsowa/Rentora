using Rentora.Application.DTOs.Rental;

namespace Rentora.Application.IServices
{
    public interface IRentService
    {
        List<int> GetUserRents(string userId);
        Task<bool> RentProduct(RentProductDTO rentProductDTO);
    }
}
