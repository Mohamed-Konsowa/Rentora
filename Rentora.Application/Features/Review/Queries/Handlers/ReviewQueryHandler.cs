using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Reviev;
using Rentora.Application.Features.Review.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Queries.Handlers
{
    internal class ReviewQueryHandler : ResponseHandler
                                    , IRequestHandler<GetProductReviews, Response<List<GetProductReviewsDTO>>>
    {
        private readonly IReviewService _reviewService;

        public ReviewQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public Task<Response<List<GetProductReviewsDTO>>> Handle(GetProductReviews request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
