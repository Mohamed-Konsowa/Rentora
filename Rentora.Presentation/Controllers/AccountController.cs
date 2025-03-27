using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.Features.Account.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class AccountController : AppControllerBase
    {
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

        [HttpPost(Router.Account.Register)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterCommand request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NewResult(await _mediator.Send(request));
        }

        [HttpPost(Router.Account.Login)]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginCommand request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NewResult(await _mediator.Send(request));
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost(Router.Account.Role)]
        public async Task<IActionResult> AddRoleAsync(AddRoleCommand request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NewResult(await _mediator.Send(request));
        }
    }
}
