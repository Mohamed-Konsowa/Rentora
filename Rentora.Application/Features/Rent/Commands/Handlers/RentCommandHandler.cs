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
        private readonly IUserService _userService;

        public RentCommandHandler(IRentService rentService, IProductService productService, IUserService userService)
        {
            _rentService = rentService;
            _productService = productService;
            _userService = userService;
        }

        public async Task<Response<string>> Handle(RentProductCommand request, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(request.ApplicationUserId.ToString()) is null)
                return ResponseHandler.NotFound<string>("User not found!");

            var product = await _productService.GetProductByIdAsync((int)request.ProductId);
            
            if (product is null)
                return ResponseHandler.NotFound<string>("Product not found!");

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
            var product = await _productService.GetProductByIdAsync((int)request.ProductId);
            if (product is null) return ResponseHandler.BadRequest<string>("Product not found!");

            var rental = _rentService.GetRentalByProductIdAsync((int)request.ProductId);
            if (rental is null) return ResponseHandler.BadRequest<string>("Error!");
            
            
            rental.RentStatus = ProductStatus.Returned;
            await _rentService.UpdateRentalAsync(rental);
            product.ProductStatus = ProductStatus.Available;
            await _productService.UpdateAsync(product);

            return ResponseHandler.Success("Success operation.");
        }
    }
}
