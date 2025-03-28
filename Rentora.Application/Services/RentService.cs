﻿using Rentora.Application.DTOs.Rental;
using Rentora.Application.IRepositories;
using Rentora.Application.IServices;
using Rentora.Domain.Models;

namespace Rentora.Application.Services
{
    public class RentService : IRentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<int>> GetUserRentsAsync(string userId)
        {
            return await _unitOfWork.rentals.GetUserRents(userId);
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
