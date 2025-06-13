using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Rent.Commands.Models
{
    public class ReturnProductCommand : IRequest<Response<string>>
    {
        public int? ProductId { get; set; }
    }
}
