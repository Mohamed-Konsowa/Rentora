
using MediatR;
using Rentora.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Email.Commands.Models
{
    public class SendResetPasswordTokenCommand : IRequest<Response<string>>
    {
        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
