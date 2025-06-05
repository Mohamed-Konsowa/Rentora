using MediatR;
using Rentora.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class AddRoleCommand : IRequest<Response<string>>
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
