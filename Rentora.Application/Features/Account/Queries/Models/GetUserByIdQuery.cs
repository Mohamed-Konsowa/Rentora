using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;

namespace Rentora.Application.Features.Account.Queries.Models
{
    public class GetUserByIdQuery : IRequest<Response<UserDTO>>
    {
        public string UserId { get; set; }
    }
}
