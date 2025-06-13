using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Account.Queries.Models
{
    public class CheckIfEmailExistsQuery : IRequest<Response<bool>>
    {
        public string? Email { get; set; }
    }
}
