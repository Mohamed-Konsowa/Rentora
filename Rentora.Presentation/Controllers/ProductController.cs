using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentora.Presentation.DTOs.Product;
using Rentora.Domain.Models;
using Rentora.Application.IRepositories;
using Rentora.Presentation.Services;
using Rentora.Domain.Models.Categories;
using Microsoft.VisualBasic.FileIO;

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

        [HttpGet("GetProducts")]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var product = await _productService.GetProductById(id);
            if(product is not null)
            return Ok(product);
            return BadRequest("Product not found");
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProductAsync(AddProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (productDto.CategoryId < 1 || productDto.CategoryId > 4)
                return BadRequest("Invalid Category Id");
            var product = new Product()
            {
                ApplicationUserId = productDto.ApplicationUserId,
                CategoryId = productDto.CategoryId,
                Title = productDto.Title,
                Description = productDto.Description,
                Quantity = productDto.Quantity,
                Price = productDto.Price,
                RentalPeriod = productDto.RentalPeriod,
                Location = productDto.Location,
                Latitude = productDto.Latitude,
                Longitude = productDto.Longitude,
                Status = productDto.Status,
                CreatedAt = DateTime.UtcNow
            };
            await _productService.AddProduct(product);
            switch (productDto.CategoryId)
            {
                case 1: await _productService.AddProductCategory(new Sport()
                {
                    ProductId = product.ProductId,
                    Brand = productDto.Brand,
                    Model = productDto.Model,
                    Condition = productDto.Condition
                }); break;
                case 2: await _productService.AddProductCategory(new Transportation()
                {
                    ProductId = product.ProductId,
                    Transmission = productDto.Transmission,
                    Body_Type = productDto.Body_Type,
                    Fuel_Type = productDto.Fuel_Type
                }); break;
                case 3: await _productService.AddProductCategory(new Travel()
                {
                    ProductId = product.ProductId,
                    Condition = productDto.Condition
                }); break;
                case 4: await _productService.AddProductCategory(new Electronic()
                {
                    ProductId = product.ProductId,
                    Brand = productDto.Brand,
                    Model = productDto.Model,
                    Condition = productDto.Condition
                }); break;
            }

            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int ProductId, AddProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _productService.GetProductById(ProductId);
            if(product is null) return BadRequest("Product not found");

            product.ApplicationUserId = productDto.ApplicationUserId;
            product.CategoryId = productDto.CategoryId;
            product.Title = productDto.Title;
            product.Description = productDto.Description;
            product.Quantity = productDto.Quantity;
            product.Price = productDto.Price;
            product.RentalPeriod = productDto.RentalPeriod;
            product.Location = productDto.Location;
            product.Latitude = productDto.Latitude;
            product.Longitude = productDto.Longitude;
            product.Status = productDto.Status;

            await _productService.UpdateProduct(product);
            return Ok(productDto);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if(result)
                return Ok("The product is deleted.");
            else
                return BadRequest("Error");
        }
        [HttpPost("add-image")]
        public async Task<IActionResult> AddProdectImage(ProductImage productImage)
        {
            var result = await _productService.AddProductImage(productImage);
            if (result) return Ok("Image was added successfully");
            return BadRequest("Failed to add Image!");
        }
    }
}
