using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.ProductImage;

namespace Rentora.Application.Features.Product.Queries.Models
{
    public class GetProductImagesQuery : IRequest<Response<List<ProductImageDTO>>>
    {
        public int? ProductId { get; set; }
    }
}