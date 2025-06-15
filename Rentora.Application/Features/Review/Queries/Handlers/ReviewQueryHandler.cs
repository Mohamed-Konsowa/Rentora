using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Review;
using Rentora.Application.Features.Review.Queries.Models;
using Rentora.Application.Features.Review.Queries.Results;
using Rentora.Application.IServices;
using Rentora.Application.Services;

namespace Rentora.Application.Features.Review.Queries.Handlers
{
    internal class ReviewQueryHandler : IRequestHandler<GetProductReviewsPaginatedQuery, Response<List<GetProductReviewsDTO>>>
                                      , IRequestHandler<GetProductRateQuery, Response<GetProductRateResult>>
    {
        private readonly IReviewService _reviewService;
        private readonly IProductService _productService;

        public ReviewQueryHandler(IReviewService reviewService, IProductService productService)
        {
            _reviewService = reviewService;
            _productService = productService;
        }

        public async Task<Response<List<GetProductReviewsDTO>>> Handle(GetProductReviewsPaginatedQuery request, CancellationToken cancellationToken)
        {
            if (await _productService.GetProductByIdAsync((int)request.ProductId) is null)
                return ResponseHandler.NotFound<List<GetProductReviewsDTO>>("Product not found!");

            request.PageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;
            var Ids = await _reviewService.GetProductReviewsPaginatedAsync
            (
                 (int)request.ProductId,
                 (int)request.PageNumber,
                 (int)request.PageSize,
                 cancellationToken
            );

            var response = ResponseHandler.Success(Ids.Item1.ToList());
            response.Meta = new PaginatedMeta
            {
                CurrentPage = (int)request.PageNumber,
                Succeeded = true,
                PageSize = (int)request.PageSize,
                TotalPages = (int)Math.Ceiling((float)Ids.Item2 / (int)request.PageSize),
                TotalCount = Ids.Item2
            };
            return response;
        }

        public async Task<Response<GetProductRateResult>> Handle(GetProductRateQuery request, CancellationToken cancellationToken)
        {
            if (await _productService.GetProductByIdAsync((int)request.ProductId) is null)
                return ResponseHandler.NotFound<GetProductRateResult>("Product not found!");

            var x = await _reviewService.GetProductRateAsync((int)request.ProductId);
            return ResponseHandler.Success(new GetProductRateResult { NumOfReviews = x.Count, ProductRate = x.Rate});
        }
    }
}
