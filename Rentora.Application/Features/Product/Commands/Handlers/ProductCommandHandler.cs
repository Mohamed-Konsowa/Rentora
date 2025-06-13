using AutoMapper;
using MediatR;
using Rentora.Application.Base;
using Rentora.Application.Features.Product.Commands.Models;
using Rentora.Application.Helpers;
using Rentora.Application.IServices;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.Features.Product.Queries.Handlers
{
    public class ProductCommandHandler : IRequestHandler<DeleteImageCommand, Response<string>>
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
            var result = await _productService.DeleteImageById((int)request.ImageId);
            if(result) return ResponseHandler.Success("Image deleted successfully.");
            return ResponseHandler.BadRequest<string>("Failed to delete the image");
        }

        public async Task<Response<string>> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            var isImage = CommonUtils.IsImage(request.Image);
            if (!isImage.Item1) return ResponseHandler.BadRequest<string>(isImage.Item2);

            var result = await _productService.AddProductImageAsync((int)request.ProductId, request.Image);
            if (result) return ResponseHandler.Success("Image was added successfully");
            return ResponseHandler.BadRequest<string>("Failed to add Image!");
        }

        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteProductAsync((int)request.ProductId);
            return result ? ResponseHandler.Success("The product is deleted.") : ResponseHandler.BadRequest<string>("Failed to delete product!");
        }

        public async Task<Response<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateProductAsync(request);
            if (result is null) return ResponseHandler.NotFound<ProductDTO>("Product not found!");
            return ResponseHandler.Success(result);
        }

        public async Task<Response<ProductDTO>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<AddProductDTO>(request);
            var result = await _productService.AddProductAsync(product);
            return ResponseHandler.Success(result);
        }
    }
}



