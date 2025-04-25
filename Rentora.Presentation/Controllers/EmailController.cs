using Microsoft.AspNetCore.Mvc;
using Rentora.Application.Features.Email.Commands.Models;
using Rentora.Domain.AppMetaData;
using Rentora.Presentation.Base;

namespace Rentora.Presentation.Controllers
{
    public class EmailController : AppControllerBase
    {
        [HttpPost(Router.Email.Send)]
        public async Task<ActionResult> SendEmail(SendEmailCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        [HttpPost]
        [Route(Router.Email.SendOTP)]
        public async Task<ActionResult> SendOTP(SendOTPCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }

        [HttpPost]
        [Route(Router.Email.VerifyOTP)]
        public async Task<ActionResult> VerifyOTP(VerifyOTPCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }
        
        [HttpPost(Router.Email.SendResetPasswordToken)]
        public async Task<ActionResult> SendResetPasswordToken(SendResetPasswordTokenCommand request)
        {
            return NewResult(await _mediator.Send(request));
        }
    }
}
