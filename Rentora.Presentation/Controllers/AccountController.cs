using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.Features.Account.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class AccountController : AppControllerBase
    {
        /// <summary>
        /// Gets a list of all registered users.
        /// </summary>
        [HttpGet(Router.Account.GetAll)]
        public async Task<IActionResult> GetAllUsers()
        {
            return NewResult(await _mediator.Send(new GetAllUsersQuery()));
        }

        /// <summary>
        /// Gets a user by their unique ID.
        /// </summary>
        [HttpGet(Router.Account.GetById)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            return NewResult(await _mediator.Send(new GetUserByIdQuery { UserId = userId}));
        }

        /// <summary>
        /// Checks if the given email is already registered.
        /// </summary>
        [HttpGet]
        [Route(Router.Account.CheckIfEmailExists)]
        public async Task<IActionResult> CheckIfEmailExists([FromRoute]string email)
        {
            return NewResult(await _mediator.Send(new CheckIfEmailExistsQuery { Email = email}));
        }

        /// <summary>
        /// Checks if the given username is already taken.
        /// </summary>
        [HttpGet]
        [Route(Router.Account.CheckIfUserNameExists)]
        public async Task<IActionResult> CheckIfUserNameExists(string userName)
        {
            return NewResult(await _mediator.Send(new CheckIfUserNameExistsQuery { UserName = userName}));
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost(Router.Account.Register)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterCommand request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Authenticates a user and returns a token.
        /// </summary>
        [HttpPost(Router.Account.Login)]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Resets the password for a user.
        /// </summary>
        [HttpPost(Router.Account.ResetPassword)]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Adds a role to a user.
        /// </summary>
        //[Authorize(Roles = "Admin")]
        [HttpPost(Router.Account.Role)]
        public async Task<IActionResult> AddRoleAsync(AddRoleCommand request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return NewResult(await _mediator.Send(request));
        }
    }
}
