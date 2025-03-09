using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.DTOs.Authentication;
using Rentora.Presentation.Services;

namespace Rentora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _authService;
        public UserController(IUserService authService)
        {
            _authService = authService;
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _authService.GetAllUsers());
        }

        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthinticated)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthinticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);
            return Ok(model);
        }

        [HttpPost("sendEmail")]
        public async Task<ActionResult> SendEmail(SendEmailDTO model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var statuscode = await _authService.SendEmail(model.Email, model.Message, model.Subject);
            
            if (statuscode == "Accepted")
                return Ok(statuscode);
            return BadRequest(statuscode);
        }
    }
}
