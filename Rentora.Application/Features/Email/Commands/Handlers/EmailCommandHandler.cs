using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Email.Commands.Models;
using Rentora.Presentation.Services;

namespace Rentora.Application.Features.Email.Commands.Handlers
{
    public class EmailCommandHandler : ResponseHandler
                                      , IRequestHandler<SendEmailCommand, Response<string>>
                                      , IRequestHandler<SendOTPCommand, Response<string>>
                                      , IRequestHandler<VerifyOTPCommand, Response<string>>
    {
        private readonly IEmailService _emailService;

        public EmailCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendEmail(request.Email, request.Message, request.Subject);
            if(result == "Accepted") return Success("", "Email sent successfully");
            return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(SendOTPCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendOTP(request.Email);
            if (result) return Success("", "OTP sent successfully");
            return BadRequest<string>("Failed to send OTP!");
        }

        public async Task<Response<string>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.VerifyOtp(request.Email, request.OTPCode);
            if (result.Item1) return Success("");
            return BadRequest<string>(result.Item2);
        }
    }
}
