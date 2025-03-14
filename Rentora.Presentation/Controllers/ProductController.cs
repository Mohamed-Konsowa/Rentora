using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rentora.Presentation.DTOs.Product;
using Rentora.Domain.Models;
using Rentora.Application.IRepositories;
using Rentora.Presentation.Services;
using Rentora.Domain.Models.Categories;
using Microsoft.VisualBasic.FileIO;
using Rentora.Application.DTOs.Product;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet("getProductById")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var product = await _productService.GetProductById(id);
            if(product is not null)
            return Ok(new ProductDTO(product));
            return BadRequest("Product not found");
        }

        [Authorize]
        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProductAsync(ProductDTO productDto)
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
        [Route("updateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = await _productService.GetProductById(productDto.ProductId);
            if(product is null) return BadRequest("Product not found");

            product.Title = productDto.Title;
            product.Description = productDto.Description;
            product.Quantity = productDto.Quantity;
            product.Price = productDto.Price;
            product.RentalPeriod = productDto.RentalPeriod;
            product.Location = productDto.Location;
            product.Latitude = productDto.Latitude;
            product.Longitude = productDto.Longitude;
            product.Status = productDto.Status;

            var categoryId = _productService.GetProductCategoryId(product.ProductId);
            switch (product.CategoryId)
            {
                case 1:
                    await _productService.UpdateProductCategory(1, new Sport()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Brand = productDto.Brand,
                        Model = productDto.Model,
                        Condition = productDto.Condition
                    }); break;
                case 2:
                    await _productService.UpdateProductCategory(2, new Transportation()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Transmission = productDto.Transmission,
                        Body_Type = productDto.Body_Type,
                        Fuel_Type = productDto.Fuel_Type
                    }); break;
                case 3:
                    await _productService.UpdateProductCategory(3, new Travel()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Condition = productDto.Condition
                    }); break;
                case 4:
                    await _productService.UpdateProductCategory(4, new Electronic()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Brand = productDto.Brand,
                        Model = productDto.Model,
                        Condition = productDto.Condition
                    }); break;
            }

            await _productService.UpdateProduct(product);
            return Ok(productDto);
        }

        [Authorize]
        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            var result = await _productService.DeleteProduct(id);
            if(result)
                return Ok("The product is deleted.");
            else
                return BadRequest("Error");
        }

        [HttpPost("add-image")]
        public async Task<IActionResult> AddProdectImage(ProductImageDTO productImageDTO)
        {
            var result = await _productService.AddProductImage(new ProductImage{ 
                ProductId = productImageDTO.ProductId,
                Image = productImageDTO.Image
            });
            if (result) return Ok("Image was added successfully");
            return BadRequest("Failed to add Image!");
        }
    }
}
