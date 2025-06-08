using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Review;
using Rentora.Application.Features.Review.Queries.Models;
using Rentora.Application.Features.Review.Queries.Results;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Queries.Handlers
{
    internal class ReviewQueryHandler : IRequestHandler<GetProductReviewsPaginatedQuery, Response<List<GetProductReviewsDTO>>>
                                      , IRequestHandler<GetProductRateQuery, Response<GetProductRateResult>>
    {
        private readonly IReviewService _reviewService;

        public ReviewQueryHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<Response<List<GetProductReviewsDTO>>> Handle(GetProductReviewsPaginatedQuery request, CancellationToken cancellationToken)
        {
            request.PageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            var Ids = await _reviewService.GetProductReviewsPaginatedAsync
            (
                 request.ProductId,
                 request.PageNumber,
                 request.PageSize,
                 cancellationToken
            );

            var response = ResponseHandler.Success(Ids.Item1.ToList());
            response.Meta = new PaginatedMeta
            {
                CurrentPage = request.PageNumber,
                Succeeded = true,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((float)Ids.Item2 / request.PageSize),
                TotalCount = Ids.Item2
            };
            return response;
        }

        public async Task<Response<GetProductRateResult>> Handle(GetProductRateQuery request, CancellationToken cancellationToken)
        {
            var x = await _reviewService.GetProductRateAsync(request.ProductId);
            return ResponseHandler.Success(new GetProductRateResult { NumOfReviews = x.Count, ProductRate = x.Rate});
        }
    }
}
