using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Review.Queries.Results;

namespace Rentora.Application.Features.Review.Queries.Models
{
    public class GetProductRateQuery : IRequest<Response<GetProductRateResult>>
    {
        public int? ProductId { get; set; }
    }
}
