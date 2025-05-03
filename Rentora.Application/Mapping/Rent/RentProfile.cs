
using AutoMapper;
using Rentora.Application.DTOs.Rental;
using Rentora.Domain.Models;

namespace Rentora.Application.Mapping.Rent
{
    public class RentProfile : Profile
    {
        public RentProfile() 
        {
            CreateMap<RentProductDTO, Rental>();
        }
    }
}
