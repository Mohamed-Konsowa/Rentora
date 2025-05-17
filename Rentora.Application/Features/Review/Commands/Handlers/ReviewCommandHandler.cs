using AutoMapper;
using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Review;
using Rentora.Application.Features.Review.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Commands.Handlers
{
    internal class ReviewCommandHandler : IRequestHandler<AddReviewCommand, Response<string>>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewCommandHandler(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var IsUserReviewedBefore = await _reviewService.IsUserReviewedBeforeAsync(request.UserId, request.ProductId);
            if (IsUserReviewedBefore)
            {
                return ResponseHandler.BadRequest<string>("The User Reviewed Before!");
            }
            var review = _mapper.Map<AddReviewDTO>(request);
            var result = await _reviewService.AddReviewAsync(review);
            if(result) return ResponseHandler.Success("Review added successfully.");
            return ResponseHandler.BadRequest<string>("Error!");
        }
    }
}
