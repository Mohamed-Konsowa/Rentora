using AutoMapper;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Domain.Models;

namespace Rentora.Application.Mapping.Rent
{
    public class RentProfile : Profile
    {
        public RentProfile() 
        {
            CreateMap<RentProductCommand, Rental>();
        }
    }
}
