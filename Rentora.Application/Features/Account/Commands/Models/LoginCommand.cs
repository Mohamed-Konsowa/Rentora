using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class LoginCommand : IRequest<Response<AuthModel>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
