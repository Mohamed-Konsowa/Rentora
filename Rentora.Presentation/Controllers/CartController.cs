using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentora.Presentation.Services;

namespace Rentora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet]
        [Route("getUserCartItems")]
        public  IActionResult GetUserCartItemsAsync(string userId)
        {
            return Ok(_cartService.GetUserCartItems(userId));
        }
        [HttpPost]
        [Route("addItemToCart")]
        public async Task<IActionResult> AddItemAsync(string UserId, int productId)
        {
            var result = await _cartService.AddInCart(UserId, productId);
            if(result) return Ok("Item added to cart successfully.");
            return BadRequest("Failed to add Item!");
        }
        [HttpDelete]
        [Route("deleteItem")]
        public async Task<IActionResult> DeleteItemAsync(string userId, int productId)
        {
            var result = await _cartService.RemoveFromCart(userId, productId);
            if (result) return Ok("Item deleted successfully.");
            return BadRequest("Failed to delete Item!");
        }
    }
}
