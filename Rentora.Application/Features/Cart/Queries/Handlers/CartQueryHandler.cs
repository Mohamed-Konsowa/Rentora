using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Cart.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Cart.Queries.Handlers
{
    public class CartQueryHandler : ResponseHandler
                                      , IRequestHandler<GetUserCartItemsQuery, Response<List<int>>>
    {
        private readonly ICartService _cartService;
        public CartQueryHandler(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<Response<List<int>>> Handle(GetUserCartItemsQuery request, CancellationToken cancellationToken)
        {
            var Ids = _cartService.GetUserCartItems(request.UserId);
            return Success(Ids);
        }
    }
}
