using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Review;
using Rentora.Application.Features.Review.Queries.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Queries.Handlers
{
    internal class ReviewQueryHandler : ResponseHandler
                                    , IRequestHandler<GetProductReviewsQuery, Response<List<GetProductReviewsDTO>>>
    {
        private readonly IReviewService _reviewService;

        public ReviewQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<Response<List<GetProductReviewsDTO>>> Handle(GetProductReviewsQuery request, CancellationToken cancellationToken)
        {
            return Success(await _reviewService.GetProductReviewsAsync(request.ProductId));
        }
    }
}
