using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Review.Commands.Models;
using Rentora.Application.Features.Review.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class ReviewController : AppControllerBase
    {
        [HttpPost(Router.Review.AddReview)]
        public async Task<IActionResult> AddReview(AddReviewCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        [HttpGet(Router.Review.GetProductReviews)]
        public async Task<IActionResult> GetProductReviews([FromRoute] string ProductId)
        {
            return NewResult(await _mediator.Send(new GetProductReviews() { ProductId = ProductId}));
        }
    }
}
