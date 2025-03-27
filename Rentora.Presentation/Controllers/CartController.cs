//using Microsoft.AspNetCore.Mvc;
//using Rentora.Application.IServices;

//namespace Rentora.Presentation.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CartController : ControllerBase
//    {
//        private readonly ICartService _cartService;
//        public CartController(ICartService cartService)
//        {
//            _cartService = cartService;
//        }
//        [HttpGet]
//        [Route("getUserCartItems")]
//        public  IActionResult GetUserCartItemsAsync(string userId)
//        {
//            return Ok(_cartService.GetUserCartItems(userId));
//        }
//        [HttpPost]
//        [Route("addProductToCart")]
//        public async Task<IActionResult> AddItemAsync(string UserId, int productId)
//        {
//            var result = await _cartService.AddInCart(UserId, productId);
//            if(result) return Ok("Product added to cart successfully.");
//            return BadRequest("Failed to add product!");
//        }
//        [HttpDelete]
//        [Route("removeFromCart")]
//        public async Task<IActionResult> DeleteItemAsync(string userId, int productId)
//        {
//            var result = await _cartService.RemoveFromCart(userId, productId);
//            if (result) return Ok("Product removed successfully.");
//            return BadRequest("Failed to remove product!");
//        }
//    }
//}
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Cart.Commands.Models;
using Rentora.Application.Features.Cart.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class CartController : AppControllerBase
    {
        [HttpGet]
        [Route(Router.Cart.GetUserFav)]
        public async Task<IActionResult> GetUserCartItemsAsync([FromRoute] string userId)
        {
            return NewResult(await _mediator.Send(new GetUserCartItemsQuery { UserId = userId }));
        }
        [HttpPost]
        [Route(Router.Cart.Add)]
        public async Task<IActionResult> AddItemAsync([FromQuery] AddInCartCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }
        [HttpDelete]
        [Route(Router.Cart.Remove)]
        public async Task<IActionResult> DeleteItemAsync([FromQuery] RemoveFromCartCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }
    }
}
