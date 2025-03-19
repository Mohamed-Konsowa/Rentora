using Microsoft.AspNetCore.Mvc;
using Rentora.Presentation.DTOs.Product;
using Rentora.Domain.Models;
using Rentora.Presentation.Services;
using Rentora.Domain.Models.Categories;
using Rentora.Application.DTOs.Product;
using Microsoft.AspNetCore.Authorization;
using Rentora.Persistence.Helpers;

namespace Rentora.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("getProductById")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var product = await _productService.GetProductDTOById(id);
            if (product is not null)
                return Ok(product);
            return BadRequest("Product not found");
        }

        //[Authorize]
        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProductAsync(AddProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (productDto.CategoryId < 1 || productDto.CategoryId > 4)
                return BadRequest("Invalid Category Id, (1,2,3,4) only");
            var product = await _productService.AddProduct(productDto);

            return Ok(product);
        }

        [HttpPut]
        [Route("updateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var product = await _productService.GetProductById(productDto.ProductId);
            if (product is null) return BadRequest("Product not found");

            await _productService.UpdateProduct(productDto);
            return Ok(productDto);
        }

        [Authorize]
        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if (result)
                return Ok("The product is deleted.");
            else
                return BadRequest("Error");
        }

        [HttpPost("addImage")]
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
        [Route("getProductImagesById")]
        public async Task<IActionResult> GetProductImagesById(int productId)
        {
            return Ok(await _productService.GetProductImagesByIdAsync(productId));
        }
        [HttpDelete]
        [Route("DeleteProductImageById")]
        public async Task<IActionResult> DeleteImageById(int imageId)
        {
            var result = await _productService.DeleteImageById(imageId);
            if (result) return Ok("Image deleted successfully.");
            return BadRequest("Failed to delete the image");
        }
    }
}
