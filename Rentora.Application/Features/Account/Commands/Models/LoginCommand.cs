using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class LoginCommand : IRequest<Response<AuthModel>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
