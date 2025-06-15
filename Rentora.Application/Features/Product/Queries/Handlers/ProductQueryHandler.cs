using MediatR;
using Rentora.Application.Base;
using Rentora.Application.DTOs.ProductImage;
using Rentora.Application.Features.Product.Queries.Models;
using Rentora.Application.IServices;
using Rentora.Application.Wrappers;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.Features.Product.Queries.Handlers
{
    public class ProductQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>
                                     , IRequestHandler<GetProductsQuery, Response<List<ProductDTO>>>
                                     , IRequestHandler<GetProductsPaginatedQuery, Response<List<ProductDTO>>>
                                     , IRequestHandler<GetProductImagesQuery, Response<List<ProductImageDTO>>>
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
            var result = await _productService.GetProductDTOByIdAsync((int)request.ProductId);
            if (result == null) return ResponseHandler.NotFound<ProductDTO>("Product not found");
            return ResponseHandler.Success(result);
        }

        public async Task<Response<List<ProductDTO>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return ResponseHandler.Success(await _productService.GetProductsDTOAsync());
        }

        public async Task<Response<List<ProductImageDTO>>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken)
        {
            if (await _productService.GetProductByIdAsync((int)request.ProductId) is null)
                return ResponseHandler.NotFound<List<ProductImageDTO>> ("Product not found!");

            return ResponseHandler.Success(await _productService.GetProductImagesByIdAsync((int)request.ProductId)); 
        }

        public async Task<Response<List<ProductDTO>>> Handle(GetProductsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsDTOPaginatedAsync(request);

            return await products
                .ToPaginatedListAsync((int)request.PageNumber, (int)request.PageSize);
        }
    }
}
