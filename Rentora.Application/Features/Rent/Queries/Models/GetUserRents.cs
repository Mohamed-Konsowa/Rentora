using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Rent.Queries.Models
{
    public class GetUserRents : IRequest<Response<List<int>>>
    {
        public string UserId { get; set; }
    }
}