using MediatR;
using Rentora.Application.Base;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.Features.Product.Queries.Models
{
    public class GetProductsPaginatedQuery : IRequest<Response<List<ProductDTO>>>
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
        public string? Search { get; set; }
        public int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public int? CategoryId { get; set; }
    }
}
