using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Email.Commands.Models
{
    public class VerifyOTPCommand : IRequest<Response<string>>
    {
        public string? Email { get; set; }
        public string? OTPCode { get; set; }
    }
}
