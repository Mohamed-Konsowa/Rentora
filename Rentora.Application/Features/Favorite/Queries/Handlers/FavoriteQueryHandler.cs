using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Favorite.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Favorite.Queries.Handlers
{
    public class FavoriteQueryHandler : ResponseHandler
                                      , IRequestHandler<GetUserFavoriteItemsQuery, Response<List<int>>>
    {
        private readonly IFavoriteService _favoriteService;
        public FavoriteQueryHandler(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }
        public async Task<Response<List<int>>> Handle(GetUserFavoriteItemsQuery request, CancellationToken cancellationToken)
        {
            var Ids = _favoriteService.GetUserFavoriteItems(request.UserId);
            return Success(Ids);
        }
    }
}
