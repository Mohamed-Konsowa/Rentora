using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Favorite.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Favorite.Queries.Handlers
{
    public class CartQueryHandler : IRequestHandler<GetUserFavoriteItemsPaginatedQuery, Response<List<int>>>
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IUserService _userService;

        public CartQueryHandler(IFavoriteService favoriteService, IUserService userService)
        {
            _favoriteService = favoriteService;
            _userService = userService;
        }
        public async Task<Response<List<int>>> Handle(GetUserFavoriteItemsPaginatedQuery request, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(request.UserId.ToString()) is null)
                return ResponseHandler.NotFound<List<int>>("User not found!");

            request.PageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            var Ids = await _favoriteService.GetUserFavoriteItemsPaginatedAsync
            (
                 request.UserId.ToString(),
                 (int)request.PageNumber,
                 (int)request.PageSize,
                 cancellationToken
            );

            var response = ResponseHandler.Success(Ids.Item1.ToList());
            response.Meta = new PaginatedMeta
            {
                CurrentPage = (int)request.PageNumber,
                Succeeded = true,
                PageSize = (int)request.PageSize,
                TotalPages = (int)Math.Ceiling((float)Ids.Item2 / (int)request.PageSize),
                TotalCount = Ids.Item2
            };
            return response;
        }
    }
}
