using Rentora.Application.DTOs.Rental;

namespace Rentora.Presentation.Services
{
    public interface IRentService
    {
        List<int> GetUserRents(string userId);
        Task<bool> RentProduct(RentProductDTO rentProductDTO);
    }
}
