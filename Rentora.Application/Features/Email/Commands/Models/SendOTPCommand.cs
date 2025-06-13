using MediatR;
using Rentora.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Email.Commands.Models
{
    public class SendOTPCommand : IRequest<Response<string>>
    {
        public string? Email { get; set; }
    }
}
