using MediatR;
using Rentora.Application.Base;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.Features.Product.Queries.Models
{
    public class GetProductsQuery : IRequest<Response<List<ProductDTO>>>
    {
    }
}
