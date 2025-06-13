using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class ResetPasswordCommand : IRequest<Response<string>>
    {
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? NewPassword { get; set; }
    }
}
