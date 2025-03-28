using Microsoft.AspNetCore.Mvc;
using Rentora.Presentation.DTOs.Product;
using Rentora.Application.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
using Rentora.Application.Helpers;
using Rentora.Application.IServices;
using Rentora.Presentation.Base;
using Rentora.Domain.AppMetaData;
using Rentora.Application.Features.Product.Queries.Models;

namespace Rentora.Presentation.Controllers
{
    public class ProductController : AppControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(Router.Product.GetAll)]
        public async Task<IActionResult> GetProducts()
        {
            return NewResult(await _mediator.Send(new GetProductsQuery()));
        }

        [HttpGet(Router.Product.GetPById)]
        public async Task<IActionResult> GetProductAsync([FromRoute]int productId)
        {
            return NewResult(await _mediator.Send(new GetProductByIdQuery { ProductId = productId }));
        }

        //[Authorize]
        [HttpPost(Router.Product.Add)]
        public async Task<IActionResult> AddProductAsync(AddProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (productDto.CategoryId < 1 || productDto.CategoryId > 4)
                return BadRequest("Invalid Category Id, (1,2,3,4) only");
            var product = await _productService.AddProduct(productDto);

            return Ok(product);
        }

        [HttpPut]
        [Route(Router.Product.Update)]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = await _productService.GetProductById(productDto.ProductId);
            if (product is null) return BadRequest("Product not found");

            await _productService.UpdateProduct(productDto);
            return Ok(productDto);
        }

        [Authorize]
        [HttpDelete(Router.Product.Delete)]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result)
                return Ok("The product is deleted.");
            else
                return BadRequest("Error");
        }

        [HttpPost(Router.Product.AddImage)]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProdectImage(ProductImageDTO productImageDTO)
        {
            var isImage = CommonUtils.IsImage(productImageDTO.Image);
            if(!isImage.Item1) return BadRequest(isImage.Item2);
            
            var result = await _productService.AddProductImage(productImageDTO);
            if (result) return Ok("Image was added successfully");
            return BadRequest("Failed to add Image!");
        }
        [HttpGet]
        [Route(Router.Product.GetImages)]
        public async Task<IActionResult> GetProductImagesById([FromRoute]int productId)
        {
            return NewResult(await _mediator.Send(new GetProductImagesQuery() { ProductId = productId}));
        }

        [HttpDelete]
        [Route(Router.Product.DeleteImage)]
        public async Task<IActionResult> DeleteImageById(int imageId)
        {
            var result = await _productService.DeleteImageById(imageId);
            if (result) return Ok("Image deleted successfully.");
            return BadRequest("Failed to delete the image");
        }
    }
}
