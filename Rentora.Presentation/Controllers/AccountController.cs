using Microsoft.AspNetCore.Mvc;
using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Queries.Models;
using Rentora.Application.IServices;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class AccountController : AppControllerBase
    {
        private readonly IUserService _authService;
        public AccountController(IUserService authService)
        {
            _authService = authService;
        }

        [HttpGet(Router.Account.GetAll)]
        public async Task<IActionResult> GetAllUsers()
        {
            return NewResult(await _mediator.Send(new GetAllUsersQuery()));
        }
        [HttpGet]
        [Route(Router.Account.GetById)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            return NewResult(await _mediator.Send(new GetUserByIdQuery { UserId = userId}));
        }
        [HttpGet]
        [Route(Router.Account.CheckIfEmailExists)]
        public async Task<IActionResult> CheckIfEmailExists([FromRoute]string email)
        {
            return NewResult(await _mediator.Send(new CheckIfEmailExistsQuery { Email = email}));
        }
        [HttpGet]
        [Route(Router.Account.CheckIfUserNameExists)]
        public async Task<IActionResult> CheckIfUserNameExists(string userName)
        {
            return NewResult(await _mediator.Send(new CheckIfUserNameExistsQuery { UserName = userName}));
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
