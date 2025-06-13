using MediatR;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Product.Commands.Models
{
    public class DeleteProductCommand : IRequest<Response<string>>
    {
        public int? ProductId { get; set; }
    }
}


