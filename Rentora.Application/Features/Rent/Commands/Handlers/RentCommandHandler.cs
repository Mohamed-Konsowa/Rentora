using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Rent.Commands.Models;
using Rentora.Application.IServices;
using Rentora.Domain.Enums.Product;

namespace Rentora.Application.Features.Rent.Commands.Handlers
{
    internal class RentCommandHandler : IRequestHandler<RentProductCommand, Response<string>>
                                      , IRequestHandler<ReturnProductCommand, Response<string>>
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
            var product = await _productService.GetProductByIdAsync((int)request.ProductId);
            
            if (product.ProductStatus != ProductStatus.Available)
            {
                return ResponseHandler.BadRequest<string>("Sorry, this product is not available!");
            }
            var result = await  _rentService.RentProductAsync(request);
            if(result) 
            { 
                return ResponseHandler.Success("Success operation."); 
            }
            return ResponseHandler.BadRequest<string>("Error!");
        }

        public async Task<Response<string>> Handle(ReturnProductCommand request, CancellationToken cancellationToken)
        {
            var rental = _rentService.GetRentalByProductIdAsync((int)request.ProductId);
            var product = await _productService.GetProductByIdAsync((int)request.ProductId);
            if (rental is null || product is null) return ResponseHandler.BadRequest<string>("Error!");

            rental.RentStatus = ProductStatus.Returned;
            await _rentService.UpdateRentalAsync(rental);
            product.ProductStatus = ProductStatus.Available;
            await _productService.UpdateAsync(product);

            return ResponseHandler.Success("Success operation.");
        }
    }
}
