using Microsoft.AspNetCore.Mvc;
using Rentora.Presentation.Services;

namespace Rentora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _FavoriteService;
        public FavoriteController(IFavoriteService FavoriteService)
        {
            _FavoriteService = FavoriteService;
        }
        [HttpGet]
        [Route("getUserFavoriteItems")]
        public  IActionResult GetUserCartItemsAsync(string userId)
        {
            return Ok(_FavoriteService.GetUserFavoriteItems(userId));
        }
        [HttpPost]
        [Route("addProductToFavorite")]
        public async Task<IActionResult> AddItemAsync(string UserId, int productId)
        {
            var result = await _FavoriteService.AddInFavorite(UserId, productId);
            if(result) return Ok("Product added to favorites successfully.");
            return BadRequest("Failed to add Product!");
        }
        [HttpDelete]
        [Route("removeFromFavorite")]
        public async Task<IActionResult> DeleteItemAsync(string userId, int productId)
        {
            var result = await _FavoriteService.RemoveFromFavorite(userId, productId);
            if (result) return Ok("Product removed successfully.");
            return BadRequest("Failed to remove Product!");
        }
    }
}
