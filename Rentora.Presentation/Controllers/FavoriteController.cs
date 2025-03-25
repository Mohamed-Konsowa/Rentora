using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Favorite.Commands.Models;
using Rentora.Application.Features.Favorite.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class FavoriteController : AppControllerBase
    {
        [HttpGet]
        [Route(Router.Favorite.GetUserFav)]
        public async Task<IActionResult> GetUserFavoriteItemsAsync([FromRoute]string userId)
        {
            return NewResult(await _mediator.Send(new GetUserFavoriteItemsQuery { UserId = userId }));
        }
        [HttpPost]
        [Route(Router.Favorite.Add)]
        public async Task<IActionResult> AddItemAsync([FromQuery] AddInFavoriteCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }
        [HttpDelete]
        [Route(Router.Favorite.Remove)]
        public async Task<IActionResult> DeleteItemAsync([FromQuery]RemoveFromFavoriteCommand request)
        {
            return NewResult(await _mediator.Send(request));            
        }
    }
}
