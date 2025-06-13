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

        public CartCommandHandler(ICartService carteService)
        {
            _cartService = carteService;
        }
        public async Task<Response<string>> Handle(AddInCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartService.AddInCartAsync(request.UserId.ToString(), (int)request.ProductId);
            if (result) return ResponseHandler.Success("", "Product added to Cart successfully.");
            return ResponseHandler.BadRequest<string>("Failed to add Product!");
        }

        public async Task<Response<string>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _cartService.RemoveFromCartAsync(request.UserId.ToString(), (int)request.ProductId);
            if (result) return ResponseHandler.Success("", "Product removed successfully.");
            return ResponseHandler.BadRequest<string>("Failed to remove Product!");
        }
    }
}
