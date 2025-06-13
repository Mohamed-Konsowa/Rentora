using MediatR;
using Rentora.Application.Base;


namespace Rentora.Application.Features.Cart.Queries.Models
{
    public class GetUserCartItemsPaginatedQuery : IRequest<Response<List<int>>>
    {
        public Guid? UserId { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
