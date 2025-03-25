using MediatR;
using Rentora.Application.Base;


namespace Rentora.Application.Features.Favorite.Queries.Models
{
    public class GetUserFavoriteItemsQuery : IRequest<Response<List<int>>>
    {
        public string UserId { get; set; }
    }
}
