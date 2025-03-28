using MediatR;
using Rentora.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Product.Commands.Models
{
    public class DeleteProductCommand : IRequest<Response<string>>
    {
        [Required]
        public int ProductId { get; set; }
    }
}


