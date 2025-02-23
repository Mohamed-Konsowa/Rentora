using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentoraAPI.DTOs;
using RentoraAPI.Models;
using RentoraAPI.Repository;

namespace RentoraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;
        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _productRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _productRepository.GetById(id);
            if(product is not null)
            return Ok(product);
            return BadRequest();
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(ProductDTO productDto)
        {
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
            _productRepository.Add(product);
            return Ok(product);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _productRepository.Delete(id);
            if(result)
                return Ok();
            else
                return BadRequest();
        }
    }
}
