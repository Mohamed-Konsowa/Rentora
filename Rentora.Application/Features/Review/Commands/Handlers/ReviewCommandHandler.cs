using AutoMapper;
using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Review;
using Rentora.Application.Features.Review.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Commands.Handlers
{
    internal class ReviewCommandHandler : IRequestHandler<AddOrUpdateReviewCommand, Response<string>>
                                        , IRequestHandler<DeleteReviewCommand, Response<string>>
    {
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ReviewCommandHandler(IReviewService reviewService, IMapper mapper
            , IUserService userService, IProductService productService)
        {
            _reviewService = reviewService;
            _userService = userService;
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddOrUpdateReviewCommand request, CancellationToken cancellationToken)
        {
            if (await _userService.GetUserByIdAsync(request.UserId.ToString()) is null)
                return ResponseHandler.NotFound<string>("User not found!");

            if (await _productService.GetProductByIdAsync((int)request.ProductId) is null)
                return ResponseHandler.NotFound<string>("Product not found!");

            var IsUserReviewedBefore = await _reviewService.IsUserReviewedBeforeAsync(request.UserId.ToString(), (int)request.ProductId);
            var review = _mapper.Map<AddReviewDTO>(request);
            bool result = false;

            if (IsUserReviewedBefore) // Update
            {
                result = await _reviewService.UpdateReviewAsync(review);
            }
            else // Add
            {
                result = await _reviewService.AddReviewAsync(review);
            }
            
            if(result) return ResponseHandler.Success(Messages.Success);
            return ResponseHandler.BadRequest<string>(Messages.BadRequest);
        }

        public async Task<Response<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var result = await _reviewService.DeleteReviewAsync((int)request.reviewId);

            if (result) return ResponseHandler.Success(Messages.Success);
            return ResponseHandler.NotFound<string>("Review not found!");
        }
    }
}
