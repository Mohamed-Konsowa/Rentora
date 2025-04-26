using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Reviev;

namespace Rentora.Application.Features.Review.Queries.Models
{
    public class GetProductReviews : IRequest<Response<List<GetProductReviewsDTO>>>
    {
        public string ProductId { get; set; }
    }
}