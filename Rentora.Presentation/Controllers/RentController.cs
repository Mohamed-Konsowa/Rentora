using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.DTOs.Rental;
using Rentora.Application.IServices;
using Rentora.Presentation.Services;

namespace Rentora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;
        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }
        [HttpGet]
        [Route("getUserRents")]
        public IActionResult GetUserRents(string  userId)
        {
            return Ok(_rentService.GetUserRents(userId));
        }
        [HttpPost]
        [Route("rentProduct")]
        public async Task<IActionResult> RentProductAsync(RentProductDTO rentProductDTO)
        {
            var result = await _rentService.RentProduct(rentProductDTO);
            if (result) return Ok("Success.");
            return BadRequest("Error!");
        }
    }
}
