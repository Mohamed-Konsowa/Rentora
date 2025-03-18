using Microsoft.AspNetCore.Mvc;
using Rentora.Application.DTOs.Authentication;
using Rentora.Presentation.Services;

namespace Rentora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _authService;
        public AccountController(IUserService authService)
        {
            _authService = authService;
        }

        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _authService.GetAllUsers());
        }
        [HttpGet]
        [Route("getUserById")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _authService.GetUserById(id);
            if (user == null) return BadRequest("User not found.");
            return Ok(user);
        }
        [HttpGet]
        [Route("checkIfEmailExists")]
        public async Task<IActionResult> CheckIfEmailExists(string email)
        {
            return Ok(await _authService.CheckIfEmailExists(email));
        }
        [HttpGet]
        [Route("checkIfUserNameExists")]
        public async Task<IActionResult> CheckIfUserNameExists(string userName)
        {
            return Ok(await _authService.CheckIfEmailExists(userName));
        }

        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthinticated)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.GetTokenAsync(model);

            if (!result.IsAuthinticated)
                return BadRequest(result.Errors);

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
    }
}
