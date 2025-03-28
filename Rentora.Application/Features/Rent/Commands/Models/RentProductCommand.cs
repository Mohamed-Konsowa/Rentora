
using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Rental;

namespace Rentora.Application.Features.Rent.Commands.Models
{
    public class RentProductCommand : IRequest<Response<string>>
    {
        public RentProductDTO DTO { get; set; }
    }
}
