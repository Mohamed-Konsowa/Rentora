using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.Account;


namespace Rentora.Application.Features.Account.Queries.Models
{
    public class GetAllUsersQuery : IRequest<Response<List<UserDTO>>>
    {
    }
}
