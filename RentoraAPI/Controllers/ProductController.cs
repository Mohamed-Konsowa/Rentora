using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentoraAPI.DTOs;
using RentoraAPI.Models;
using RentoraAPI.Repository;
using RentoraAPI.Services;

namespace RentoraAPI.Controllers
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

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if(product is not null)
            return Ok(product);
            return BadRequest("Product not found");
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = new Product()
            {
                ApplicationUserId = productDto.ApplicationUserId,
                CategoryId = productDto.CategoryId,
                Title = productDto.Title,
                Description = productDto.Description,
                Images = productDto.Images,
                Quantity = productDto.Quantity,
                PricePerDay = productDto.PricePerDay,
                Location = productDto.Location,
                Latitude = productDto.Latitude,
                Longitude = productDto.Longitude,
                Status = productDto.Status,
                CreatedAt = DateTime.UtcNow
            };
            _productService.AddProduct(product);
            return Ok(product);
        }

        [HttpPut]
        public IActionResult UpdateProduct(int ProductId, ProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = _productService.GetProductById(ProductId);
            if(product is null) return BadRequest("Product not found");

            product.ApplicationUserId = productDto.ApplicationUserId;
            product.CategoryId = productDto.CategoryId;
            product.Title = productDto.Title;
            product.Description = productDto.Description;
            product.Images = productDto.Images;
            product.Quantity = productDto.Quantity;
            product.PricePerDay = productDto.PricePerDay;
            product.Location = productDto.Location;
            product.Latitude = productDto.Latitude;
            product.Longitude = productDto.Longitude;
            product.Status = productDto.Status;

            _productService.UpdateProduct(product);
            return Ok(productDto);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productService.DeleteProduct(id);
            if(result)
                return Ok("The product is deleted.");
            else
                return BadRequest("Error");
        }
    }
}
