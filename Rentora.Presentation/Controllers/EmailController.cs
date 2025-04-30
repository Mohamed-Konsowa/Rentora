using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Email.Commands.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class EmailController : AppControllerBase
    {
        /// <summary>
        /// Sends an email to the specified address.
        /// </summary>
        [HttpPost(Router.Email.Send)]
        public async Task<ActionResult> SendEmail(SendEmailCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Sends a One-Time Password (OTP) to the user's email.
        /// </summary>
        [HttpPost]
        [Route(Router.Email.SendOTP)]
        public async Task<ActionResult> SendOTP(SendOTPCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Verifies the provided OTP code.
        /// </summary>
        [HttpPost]
        [Route(Router.Email.VerifyOTP)]
        public async Task<ActionResult> VerifyOTP(VerifyOTPCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        /// <summary>
        /// Sends a password reset token to the user's email.
        /// </summary>
        [HttpPost(Router.Email.SendResetPasswordToken)]
        public async Task<ActionResult> SendResetPasswordToken(SendResetPasswordTokenCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }
    }
}
