using MediatR;
using Microsoft.EntityFrameworkCore;
using Rentora.Application.Base;
using Rentora.Application.Features.Product.Queries.Models;
using Rentora.Application.IServices;
using Rentora.Application.Wrappers;
using Rentora.Domain.Models;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.Features.Product.Queries.Handlers
{
    public class ProductQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductDTO>>
                                     , IRequestHandler<GetProductsQuery, Response<List<ProductDTO>>>
                                     , IRequestHandler<GetProductsPaginatedQuery, Response<List<ProductDTO>>>
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
            var result = await _productService.GetProductDTOByIdAsync((int)request.ProductId);
            if (result == null) return ResponseHandler.NotFound<ProductDTO>("Product not found");
            return ResponseHandler.Success(result);
        }

        public async Task<Response<List<ProductDTO>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return ResponseHandler.Success(_productService.GetProductsDTO().ToList());
        }

        public async Task<Response<List<ProductImage>>> Handle(GetProductImagesQuery request, CancellationToken cancellationToken)
        {
            return ResponseHandler.Success(await _productService.GetProductImagesByIdAsync((int)request.ProductId)); 
        }

        public Task<Response<List<ProductDTO>>> Handle(GetProductsPaginatedQuery request, CancellationToken cancellationToken)
        {
            var products = _productService.GetProducts().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var keywords = request.Search
                    .ToLowerInvariant()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var keyword in keywords)
                {
                    var temp = keyword;
                    products = products.Where(p =>
                        p.Title.ToLower().Contains(temp) ||
                        p.Description.ToLower().Contains(temp)
                    );
                }
            }

            if (request.FromPrice is not null)
                products = products.Where(p => p.Price >= request.FromPrice);

            if (request.ToPrice is not null)
                products = products.Where(p => p.Price <= request.ToPrice);

            if (request.CategoryId is not null)
            {
                products = products.Where
                (
                    p => p.CategoryId == request.CategoryId
                );
            }

            return products.Select(p => new ProductDTO(p))
                .ToPaginatedListAsync((int)request.PageNumber, (int)request.PageSize);
        }
    }
}
