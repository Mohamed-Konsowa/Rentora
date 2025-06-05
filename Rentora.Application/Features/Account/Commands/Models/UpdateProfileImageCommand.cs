using MediatR;
using Microsoft.AspNetCore.Http;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Account.Commands.Models
{
    public class UpdateProfileImageCommand : IRequest<Response<string>>
    {
        public Guid UserId { get; set; }
        public IFormFile Image { get; set; }
    }
}
