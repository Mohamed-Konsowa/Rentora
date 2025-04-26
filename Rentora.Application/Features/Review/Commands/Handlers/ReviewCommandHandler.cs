using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Review.Commands.Models;
using Rentora.Application.IServices;

namespace Rentora.Application.Features.Review.Commands.Handlers
{
    internal class ReviewCommandHandler : ResponseHandler
                                    , IRequestHandler<AddReviewCommand, Response<string>>
    {
        private readonly IReviewService _reviewService;

        public ReviewCommandHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public Task<Response<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            //_reviewService.;
            throw new NotImplementedException();
        }
    }
}
