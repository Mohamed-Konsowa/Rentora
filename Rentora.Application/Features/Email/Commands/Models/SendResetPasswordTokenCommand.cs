using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Email.Commands.Models
{
    public class SendResetPasswordTokenCommand : IRequest<Response<string>>
    {
        public string? Email { get; set; }
    }
}
