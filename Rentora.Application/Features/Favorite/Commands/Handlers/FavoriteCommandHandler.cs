using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Favorite.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Favorite.Commands.Handlers
{
    public class CartCommandHandler : IRequestHandler<AddToFavoriteCommand, Response<string>>
                                    , IRequestHandler<RemoveFromFavoriteCommand, Response<string>>
    {
        private readonly IFavoriteService _favoriteService;

        public CartCommandHandler(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        public async Task<Response<string>> Handle(AddToFavoriteCommand request, CancellationToken cancellationToken)
        {
            var result = await _favoriteService.AddInFavoriteAsync(request.UserId.ToString(), (int)request.ProductId);
            if (result) return ResponseHandler.Success("", "Product added to favorites successfully.");
            return ResponseHandler.BadRequest<string>("Failed to add Product!");
        }

        public async Task<Response<string>> Handle(RemoveFromFavoriteCommand request, CancellationToken cancellationToken)
        {
            var result = await _favoriteService.RemoveFromFavoriteAsync(request.UserId.ToString(), (int)request.ProductId);
            if (result) return ResponseHandler.Success("", "Product removed successfully.");
            return ResponseHandler.BadRequest<string>("Failed to remove Product!");
        }
    }
}
