using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Review;

namespace Rentora.Application.Features.Review.Queries.Models
{
    public class GetProductReviewsPaginatedQuery : IRequest<Response<List<GetProductReviewsDTO>>>
    {
        public int? ProductId { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}