using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Cart.Commands.Models;
using Rentora.Application.Features.Cart.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    [Authorize]
    public class CartController : AppControllerBase
    {
        /// <summary>
        /// Gets all items in a user's cart with pagination.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        [HttpGet]
        [Route(Router.Cart.GetUserCart)]
        public async Task<IActionResult> GetUserCartItemsPaginatedAsync([FromRoute] Guid userId, int pageNumber, int pageSize)
        {
            return NewResult(await _mediator.Send(new GetUserCartItemsPaginatedQuery 
            {
                UserId = userId,
                PageNumber = pageNumber,
                PageSize = pageSize 
            }));
        }

        /// <summary>
        /// Adds a product to the user's cart.
        /// </summary>
        [HttpPost]
        [Route(Router.Cart.Add)]
        public async Task<IActionResult> AddItemAsync([FromQuery] AddInCartCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Removes a product from the user's cart.
        /// </summary>
        [HttpDelete]
        [Route(Router.Cart.Remove)]
        public async Task<IActionResult> DeleteItemAsync([FromQuery] RemoveFromCartCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }
    }
}
