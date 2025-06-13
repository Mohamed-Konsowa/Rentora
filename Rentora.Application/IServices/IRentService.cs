using Rentora.Application.DTOs.Rental;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Domain.Models;

namespace Rentora.Application.IServices
{
    public interface IRentService
    {
        Task<(IReadOnlyCollection<int>, int)> GetUserRentsPaginatedAsync
            (string userId, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Rental GetRentalByProductIdAsync(int userId);
        Task<bool> RentProductAsync(RentProductCommand rentProduct);
        Task<Rental> UpdateRentalAsync(Rental rental);
    }
}
