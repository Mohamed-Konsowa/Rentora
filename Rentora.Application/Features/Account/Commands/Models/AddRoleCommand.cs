using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        public Guid? UserId { get; set; }
        public string? Role { get; set; }
    }
}
