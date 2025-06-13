using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Account.Queries.Models
{
    public class CheckIfUserNameExistsQuery : IRequest<Response<bool>>
    {
        public string? UserName { get; set; }
    }
}


