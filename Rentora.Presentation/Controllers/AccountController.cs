using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using Rentora.Application.Features.Account.Commands.Models;
using Rentora.Application.Features.Account.Queries.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;
using Rentora.Presentation.Swagger.SwaggerExamples.AccountExamples;
using Rentora.Presentation.Swagger.SwaggerExamples.Helper;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Rentora.Presentation.Controllers
{
    //[Authorize]
    public class AccountController : AppControllerBase
    {
        /// <summary>
        /// Gets a list of all registered users.
        /// </summary>
        [HttpGet(Router.Account.GetAll)]
        [SwaggerResponse(StatusCodes.Status200OK, "List of users", typeof(Response<List<UserDTO>>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllExample))]
        public async Task<IActionResult> GetAllUsers()
        {
            return NewResult(await _mediator.Send(new GetAllUsersQuery()));
        }

        /// <summary>
        /// Gets a user by their unique ID.
        /// </summary>
        [HttpGet(Router.Account.GetById)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<UserDTO>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetUserByIdOkExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundExample))]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return NewResult(await _mediator.Send(new GetUserByIdQuery { UserId = userId.ToString()}));
        }

        /// <summary>
        /// Checks if the given email is already registered.
        /// </summary>
        [HttpGet]
        [Route(Router.Account.CheckIfEmailExists)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(BooleanExample))]
        public async Task<IActionResult> CheckIfEmailExists([FromRoute]string email)
        {
            return NewResult(await _mediator.Send(new CheckIfEmailExistsQuery { Email = email}));
        }

        /// <summary>
        /// Checks if the given username is already taken.
        /// </summary>
        [HttpGet]
        [Route(Router.Account.CheckIfUserNameExists)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<bool>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(BooleanExample))]
        public async Task<IActionResult> CheckIfUserNameExists(string userName)
        {
            return NewResult(await _mediator.Send(new CheckIfUserNameExistsQuery { UserName = userName}));
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        [HttpPost(Router.Account.Register)]
        [Consumes("multipart/form-data")]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Response<string>))]
        [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedExample))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidationFailedExample))]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Updates an exist user.
        /// </summary>
        [HttpPut(Router.Account.UpdateProfile)]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] UpdateProfileCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Authenticates a user and returns a token.
        /// </summary>
        [HttpPost(Router.Account.Login)]
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<AuthModel>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginOkExample))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(LoginBadRequestExample))]
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
        /// Update user's Profile Image.
        /// </summary>
        [HttpPut(Router.Account.UpdateProfileImage)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfileImageAsync([FromForm] UpdateProfileImageCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Adds a role to a user.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost(Router.Account.Role)]
        public async Task<IActionResult> AddRoleAsync(AddRoleCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }
    }
}
