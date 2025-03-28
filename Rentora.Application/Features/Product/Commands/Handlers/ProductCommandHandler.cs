using AutoMapper;
using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Product.Commands.Models;
using Rentora.Application.Helpers;
using Rentora.Application.IServices;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.Features.Product.Queries.Handlers
{
    public class ProductCommandHandler : ResponseHandler
                                      , IRequestHandler<DeleteImageCommand, Response<string>>
                                      , IRequestHandler<AddImageCommand, Response<string>>
                                      , IRequestHandler<DeleteProductCommand, Response<string>>
                                      , IRequestHandler<UpdateProductCommand, Response<ProductDTO>>
                                      , IRequestHandler<AddProductCommand, Response<ProductDTO>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductCommandHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteImageById(request.ImageId);
            if(result) return Success("Image deleted successfully.");
            return BadRequest<string>("Failed to delete the image");
        }

        public async Task<Response<string>> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var isImage = CommonUtils.IsImage(request.Image);
            if (!isImage.Item1) return BadRequest<string>(isImage.Item2);

            var result = await _productService.AddProductImage(request.ProductId, request.Image);
            if (result) return Success("Image was added successfully");
            return BadRequest<string>("Failed to add Image!");
        }

        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteProduct(request.ProductId);
            return result ? Success("The product is deleted.") : BadRequest<string>("Failed to delete product!");
        }

        public async Task<Response<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateProduct(request);
            if (result is null) return NotFound<ProductDTO>("Product not found!");
            return Success(result);
        }

        public async Task<Response<ProductDTO>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if (request.CategoryId < 1 || request.CategoryId > 4)
                return BadRequest<ProductDTO>("Invalid Category Id, (1,2,3,4) only");
            var product = new AddProductDTO();
            _mapper.Map(request, product);
            var result = await _productService.AddProduct(product);
            return Success(result);
        }
    }
}



