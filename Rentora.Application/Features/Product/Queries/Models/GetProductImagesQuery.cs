using MediatR;
using Rentora.Application.Base;
using Rentora.Domain.Models;

namespace Rentora.Application.Features.Product.Queries.Models
{
    public class GetProductImagesQuery : IRequest<Response<List<ProductImage>>>
    {
        public int? ProductId { get; set; }
    }
}