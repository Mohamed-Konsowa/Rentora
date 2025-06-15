using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Cart.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Cart.Commands.Handlers
{
    public class CartCommandHandler : IRequestHandler<AddInCartCommand, Response<string>>
                                    , IRequestHandler<RemoveFromCartCommand, Response<string>>
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public CartCommandHandler(ICartService carteService, IUserService userService, IProductService productService)
        {
            _cartService = carteService;
            _userService = userService;
            _productService = productService;
        }
        public async Task<Response<string>> Handle(AddInCartCommand request, CancellationToken cancellationToken)
        {
            if(await _userService.GetUserByIdAsync(request.UserId.ToString()) is null)
                return ResponseHandler.NotFound<string>("User not found!");

            if (await _productService.GetProductByIdAsync((int)request.ProductId) is  null)
                return ResponseHandler.NotFound<string>("Product not found!");

            var result = await _cartService.AddInCartAsync(request.UserId.ToString(), (int)request.ProductId);
            if (result) return ResponseHandler.Success("", "Product added to Cart successfully.");
            return ResponseHandler.BadRequest<string>("Failed to add Product!");
        }

        public async Task<Response<string>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(request.UserId.ToString()) is null)
                return ResponseHandler.NotFound<string>("User not found!");

            if (await _productService.GetProductByIdAsync((int)request.ProductId) is null)
                return ResponseHandler.NotFound<string>("Product not found!");

            var result = await _cartService.RemoveFromCartAsync(request.UserId.ToString(), (int)request.ProductId);
            if (result) return ResponseHandler.Success("", "Product removed successfully.");
            return ResponseHandler.BadRequest<string>("Failed to remove Product!");
        }
    }
}
