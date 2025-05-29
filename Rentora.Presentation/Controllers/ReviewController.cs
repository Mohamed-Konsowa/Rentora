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
        /// Add or update a review for a product.
        /// </summary>
        [HttpPost(Router.Review.AddOrUpdateReview)]
        public async Task<IActionResult> AddReview(AddOrUpdateReviewCommand request)
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

        /// <summary>
        /// Get product's rate.
        /// </summary>
        [HttpGet(Router.Review.GetProductRate)]
        public async Task<IActionResult> GetProductRate([FromRoute] int productId)
        {
            return NewResult(await _mediator.Send(new GetProductRateQuery() { ProductId = productId }));
        }

        /// <summary>
        /// Delete product's review.
        /// </summary>
        [HttpDelete(Router.Review.DeleteReview)]
        public async Task<IActionResult> DeleteReviewAsync([FromRoute] int reviewId)
        {
            return NewResult(await _mediator.Send(new DeleteReviewCommand() { reviewId = reviewId }));
        }
    }
}
