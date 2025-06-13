using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Email.Commands.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        public string? Email { get; set; }
        public string? Message { get; set; }
        public string? Subject { get; set; }
    }
}
