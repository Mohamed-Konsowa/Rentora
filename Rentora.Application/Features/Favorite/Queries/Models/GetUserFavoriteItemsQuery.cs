using MediatR;
using Rentora.Application.Base;


namespace Rentora.Application.Features.Favorite.Queries.Models
{
    public class GetUserCartItemsPaginatedQuery : IRequest<Response<List<int>>>
    {
        public string UserId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
