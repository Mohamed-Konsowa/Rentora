using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Cart.Commands.Models
{
    public class AddInCartCommand : IRequest<Response<string>>
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
    }
}
