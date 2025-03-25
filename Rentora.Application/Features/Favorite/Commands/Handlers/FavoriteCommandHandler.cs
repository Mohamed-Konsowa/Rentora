using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Favorite.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Favorite.Commands.Handlers
{
    public class FavoriteCommandHandler : ResponseHandler
                                        , IRequestHandler<AddInFavoriteCommand, Response<string>>
                                        , IRequestHandler<RemoveFromFavoriteCommand, Response<string>>
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteCommandHandler(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        public async Task<Response<string>> Handle(AddInFavoriteCommand request, CancellationToken cancellationToken)
        {
            var result = await _favoriteService.AddInFavorite(request.UserId, request.ProductId);
            if (result) return Success("Product added to favorites successfully.");
            return BadRequest<string>("Failed to add Product!");
        }

        public async Task<Response<string>> Handle(RemoveFromFavoriteCommand request, CancellationToken cancellationToken)
        {
            var result = await _favoriteService.RemoveFromFavorite(request.UserId, request.ProductId);
            if (result) return Success("Product removed successfully.");
            return BadRequest<string>("Failed to remove Product!");
        }
    }
}
