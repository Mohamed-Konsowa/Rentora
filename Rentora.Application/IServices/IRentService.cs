using Rentora.Application.DTOs.Rental;
using Rentora.Domain.Models;

namespace Rentora.Application.IServices
{
    public interface IRentService
    {
        Task<(IReadOnlyCollection<int>, int)> GetUserRentsPaginatedAsync
            (string userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Rental GetRentalByProductIdAsync(int userId);
        Task<bool> RentProductAsync(RentProductDTO rentProductDTO);
        Task<Rental> UpdateRentalAsync(Rental rental);
    }
}
