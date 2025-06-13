using MediatR;
using Microsoft.AspNetCore.Http;
using Rentora.Application.Base;

namespace Rentora.Application.Features.Product.Commands.Models
{
    public class AddImageCommand : IRequest<Response<string>>
    {
        public int? ProductId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
