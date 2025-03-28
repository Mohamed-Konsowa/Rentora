using MediatR;
using Microsoft.AspNetCore.Http;
using Rentora.Application.Base;
using System.ComponentModel.DataAnnotations;

namespace Rentora.Application.Features.Product.Commands.Models
{
    public class AddImageCommand : IRequest<Response<string>>
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
