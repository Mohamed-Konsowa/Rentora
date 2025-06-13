using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Favorite.Queries.Models
{
    public class GetUserFavoriteItemsPaginatedQuery : IRequest<Response<List<int>>>
    {
        public Guid? UserId { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
