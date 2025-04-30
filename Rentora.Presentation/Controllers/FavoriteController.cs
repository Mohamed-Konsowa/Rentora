using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Favorite.Commands.Models;
using Rentora.Application.Features.Favorite.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class FavoriteController : AppControllerBase
    {
        /// <summary>
        /// Gets all favorite items for a user.
        /// </summary>
        [HttpGet]
        [Route(Router.Favorite.GetUserFav)]
        public async Task<IActionResult> GetUserFavoriteItemsAsync([FromRoute]string userId)
        {
            return NewResult(await _mediator.Send(new GetUserCartItemsQuery { UserId = userId }));
        }

        /// <summary>
        /// Adds a product to the user's favorites.
        /// </summary>
        [HttpPost]
        [Route(Router.Favorite.Add)]
        public async Task<IActionResult> AddItemAsync([FromQuery] AddInCartCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Removes a product from the user's favorites.
        /// </summary>
        [HttpDelete]
        [Route(Router.Favorite.Remove)]
        public async Task<IActionResult> DeleteItemAsync([FromQuery]RemoveFromCartCommand request)
        {
            return NewResult(await _mediator.Send(request));            
        }
    }
}
