using MediatR;
using Rentora.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Email.Commands.Models
{
    public class VerifyOTPCommand : IRequest<Response<string>>
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string OTPCode { get; set; }
    }
}
