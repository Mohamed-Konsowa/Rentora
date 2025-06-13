using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Rent.Queries.Models
{
    public class GetUserRentsPaginatedQuery : IRequest<Response<List<int>>>
    {
        public Guid? UserId { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}