using MediatR;
using Rentora.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Email.Commands.Models
{
    public class SendEmailCommand : IRequest<Response<string>>
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
    }
}
