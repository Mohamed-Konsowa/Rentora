using Rentora.Application.DTOs.Rental;
using Rentora.Domain.Models;

namespace Rentora.Application.IServices
{
    public interface IRentService
    {
        Task<List<int>> GetUserRentsAsync(string userId);
        Rental GetRentalByProductIdAsync(int userId);
        Task<bool> RentProductAsync(RentProductDTO rentProductDTO);
        Task<Rental> UpdateRentalAsync(Rental rental);
    }
}
