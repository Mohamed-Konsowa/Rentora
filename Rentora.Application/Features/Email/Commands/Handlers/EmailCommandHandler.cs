using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Email.Commands.Models;
using Rentora.Application.IServices;
using Rentora.Presentation.Services;

namespace Rentora.Application.Features.Email.Commands.Handlers
{
    public class EmailCommandHandler : IRequestHandler<SendEmailCommand, Response<string>>
                                     , IRequestHandler<SendOTPCommand, Response<string>>
                                     , IRequestHandler<VerifyOTPCommand, Response<string>>
                                     , IRequestHandler<SendResetPasswordTokenCommand, Response<string>>
    {
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;

        public EmailCommandHandler(IEmailService emailService, IUserService userService)
        {
            _emailService = emailService;
            _userService = userService;
        }
        public async Task<Response<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendEmail(request.Email, request.Message, request.Subject);
            if(result == "Accepted") return ResponseHandler.Success("", "Email sent successfully");
            return ResponseHandler.BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(SendOTPCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.SendOTP(request.Email);
            if (result) return ResponseHandler.Success("", "OTP sent successfully");
            return ResponseHandler.BadRequest<string>("Failed to send OTP!");
        }

        public async Task<Response<string>> Handle(VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            var result = await _emailService.VerifyOtp(request.Email, request.OTPCode);
            if (result.Item1) return ResponseHandler.Success("");
            return ResponseHandler.BadRequest<string>(result.Item2);
        }

        public async Task<Response<string>> Handle(SendResetPasswordTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);
            if (user == null)
                return ResponseHandler.BadRequest<string>("The email not exist!");

            if(!user.EmailConfirmed)
                return ResponseHandler.BadRequest<string>("Email not confirmed!");

            var token = await _userService.GeneratePasswordResetTokenAsync(user);
            var result = await _emailService.SendEmail(request.Email, "Use this tocken to reset your password "+  token, "Reset your password.");
            
            if(result == "Accepted") return ResponseHandler.Success("Success");
            return ResponseHandler.BadRequest<string>("Failed to send token!");
        }
    }
}
