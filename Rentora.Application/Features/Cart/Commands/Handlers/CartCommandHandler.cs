using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Cart.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Cart.Commands.Handlers
{
    public class CartCommandHandler : ResponseHandler
                                        , IRequestHandler<AddInCartCommand, Response<string>>
                                        , IRequestHandler<RemoveFromCartCommand, Response<string>>
    {
        private readonly ICartService _cartService;

        public CartCommandHandler(ICartService carteService)
        {
            _cartService = carteService;
        }
        public async Task<Response<string>> Handle(AddInCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartService.AddInCart(request.UserId, request.ProductId);
            if (result) return Success("", "Product added to Cart successfully.");
            return BadRequest<string>("Failed to add Product!");
        }

        public async Task<Response<string>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartService.RemoveFromCart(request.UserId, request.ProductId);
            if (result) return Success("", "Product removed successfully.");
            return BadRequest<string>("Failed to remove Product!");
        }
    }
}
