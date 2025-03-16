
using Google.Apis.Drive.v3.Data;
using Microsoft.CodeAnalysis;
using Rentora.Application.DTOs.Rental;
using Rentora.Application.IRepositories;
using Rentora.Domain.Models;

namespace Rentora.Presentation.Services
{
    public class RentService : IRentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<int> GetUserRents(string userId)
        {
            return _unitOfWork.rentals.GetUserRents(userId);
        }

        public async Task<bool> RentProduct(RentProductDTO rentProductDTO)
        {
            var rental = new Rental() {
                ProductId = rentProductDTO.ProductId,
                ApplicationUserId = rentProductDTO.ApplicationUserId,
                StartDate = rentProductDTO.StartDate,
                EndDate = rentProductDTO.StartDate.AddDays(rentProductDTO.numOfDays),
                TotalPrice = 100,
                Status = rentProductDTO.Status,
                PenaltyFee = rentProductDTO.PenaltyFee
            };
            var result = await _unitOfWork.rentals.Add(rental);
            await _unitOfWork.Save();
            return result is not null;
        }
    }
}
