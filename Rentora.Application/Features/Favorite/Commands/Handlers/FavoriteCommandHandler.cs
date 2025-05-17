using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Favorite.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Favorite.Commands.Handlers
{
    public class CartCommandHandler : IRequestHandler<AddInCartCommand, Response<string>>
                                    , IRequestHandler<RemoveFromCartCommand, Response<string>>
    {
        private readonly IFavoriteService _favoriteService;

        public CartCommandHandler(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        public async Task<Response<string>> Handle(AddInCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _favoriteService.AddInFavorite(request.UserId, request.ProductId);
            if (result) return ResponseHandler.Success("", "Product added to favorites successfully.");
            return ResponseHandler.BadRequest<string>("Failed to add Product!");
        }

        public async Task<Response<string>> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
        {
            var result = await _favoriteService.RemoveFromFavorite(request.UserId, request.ProductId);
            if (result) return ResponseHandler.Success("", "Product removed successfully.");
            return ResponseHandler.BadRequest<string>("Failed to remove Product!");
        }
    }
}
