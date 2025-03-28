using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Product.Queries.Models;
using Rentora.Application.IServices;
using Rentora.Domain.Models;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.Features.Product.Queries.Handlers
{
    public class ProductQueryHandler : ResponseHandler
                                      , IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>
                                      , IRequestHandler<GetProductsQuery, Response<List<ProductDTO>>>
                                      , IRequestHandler<GetProductImagesQuery, Response<List<ProductImage>>>
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;

        public ProductQueryHandler(IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }
        public async Task<Response<ProductDTO>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _productService.GetProductDTOById(request.ProductId);
            if (result == null) return NotFound<ProductDTO>("Product not found");
            return Success(result);
        }

        public async Task<Response<List<ProductDTO>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return Success(await _productService.GetProducts());
        }

        public async Task<Response<List<ProductImage>>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken)
        {
            return Success(await _productService.GetProductImagesByIdAsync(request.ProductId)); 
        }
    }
}
