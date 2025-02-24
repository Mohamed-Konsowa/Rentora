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
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _unitOfWork.products.GetAll();
            return Ok(products);
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _unitOfWork.products.GetById(id);
            if(product is not null)
            return Ok(product);
            return BadRequest("Product not found");
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
            _unitOfWork.products.Add(product);
            _unitOfWork.Save();
            return Ok(product);
        }

        [HttpPut]
        public IActionResult UpdateProduct(int ProductId, ProductDTO productDto)
        {
            var product = _unitOfWork.products.GetById(ProductId);
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
            
            _unitOfWork.products.Update(product);
            _unitOfWork.Save();
            return Ok(productDto);
        }

        [HttpDelete("DeleteProduct")]
        public IActionResult DeleteProduct(int id)
        {
            var result = _unitOfWork.products.Delete(id);
            _unitOfWork.Save();
            if(result)
                return Ok("The product is deleted.");
            else
                return BadRequest("Error");
        }
    }
}
