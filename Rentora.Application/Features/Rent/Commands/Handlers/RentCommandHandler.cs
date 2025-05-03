using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Rental;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Application.IRepositories;
using Rentora.Application.IServices;
using Rentora.Domain.Enums.Product;

namespace Rentora.Application.Features.Rent.Commands.Handlers
{
    internal class RentCommandHandler : ResponseHandler
                                    , IRequestHandler<RentProductCommand, Response<string>>
    {
        private readonly IRentService _rentService;
        private readonly IProductService _productService;

        public RentCommandHandler(IRentService rentService, IProductService productService)
        {
            _rentService = rentService;
            _productService = productService;
        }

        public async Task<Response<string>> Handle(RentProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductById(request.DTO.ProductId);
            if (product.ProductStatus != ProductStatus.Available)
            {
                return BadRequest<string>("Sorry, this product is not available!");
            }
            var result = await  _rentService.RentProduct(request.DTO);
            if(result) 
            { 
                
                return Success("Success operation."); 
            }
            return BadRequest<string>("Error!");
        }
    }
}
