using AutoMapper;
using Rentora.Presentation.DTOs.Product;
using Rentora.Domain.Models;
using Rentora.Application.Features.Product.Commands.Models;

namespace Rentora.Application.Mapping.Product
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            Map();
        }
        private void Map()
        {
            CreateMap<AddProductCommand, AddProductDTO > ().ReverseMap();
            CreateMap<UpdateProductCommand, Rentora.Domain.Models.Product> ().ReverseMap();

        }
    }
}
