using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Review.Commands.Models;
using Rentora.Application.Features.Review.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class ReviewController : AppControllerBase
    {
        /// <summary>
        /// Adds a review for a product.
        /// </summary>
        [HttpPost(Router.Review.AddReview)]
        public async Task<IActionResult> AddReview(AddReviewCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Get all product's reviews.
        /// </summary>
        [HttpGet(Router.Review.GetProductReviews)]
        public async Task<IActionResult> GetProductReviews([FromRoute] int productId)
        {
            return NewResult(await _mediator.Send(new GetProductReviewsQuery() { ProductId = productId}));
        }
    }
}
