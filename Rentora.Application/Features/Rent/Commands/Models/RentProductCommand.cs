using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Rental;
using Rentora.Domain.Enums.Product;

namespace Rentora.Application.Features.Rent.Commands.Models
{
    public class RentProductCommand : IRequest<Response<string>>
    {
        public int? ProductId { get; set; }
        public Guid? ApplicationUserId { get; set; }
        public DateTime? StartDate { get; set; }
        public int? numOfDays { get; set; }
        public decimal? TotalPrice { get; set; }
        public ProductStatus? RentStatus { get; set; }
        public decimal? PenaltyFee { get; set; } = 0.00m;
    }
}
