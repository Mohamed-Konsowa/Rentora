using AutoMapper;
using Rentora.Application.DTOs.Rental;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Application.IRepositories;
using Rentora.Application.IServices;
using Rentora.Domain.Enums.Product;
using Rentora.Domain.Models;

namespace Rentora.Application.Services
{
    public class RentService : IRentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<(IReadOnlyCollection<int>, int)> GetUserRentsPaginatedAsync(string userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return await _unitOfWork.rentals.PaginateAsync
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

        public Rental GetRentalByProductIdAsync(int productId)
        {
            return _unitOfWork.rentals.GetAll().FirstOrDefault(r => r.ProductId == productId && r.RentStatus == ProductStatus.Rented);
        }

        public async Task<bool> RentProductAsync(RentProductCommand rentProduct)
        {
            var rental = _mapper.Map<Rental>(rentProduct);
            var product = await _unitOfWork.products.GetByIdAsync((int)rentProduct.ProductId);
            rental.EndDate = ((DateTime)rentProduct.StartDate).AddDays((int)rentProduct.numOfDays);
            rental.TotalPrice = (decimal)(rentProduct.numOfDays * product.Price);
            rental.RentStatus = ProductStatus.Rented;

            var result = _unitOfWork.rentals.AddAsync(rental);
            product.ProductStatus = ProductStatus.Rented;
            await _unitOfWork.SaveChangesAsync();
            return result is not null;
        }

        public async Task<Rental> UpdateRentalAsync(Rental rental)
        {
            var r = _unitOfWork.rentals.Update(rental);
            await _unitOfWork.SaveChangesAsync();
            return r;
        }
    }
}
