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
using Rentora.Application.DTOs.Authentication;

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
            if (product is null) return BadRequest("Product not found");

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
            if (result)
                return Ok("The product is deleted.");
            else
                return BadRequest("Error");
        }

        [HttpPost("add-image")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProdectImage(ProductImageDTO productImageDTO)
        {
            var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            if (!allowedTypes.Contains(productImageDTO.Image.ContentType))
            {
                return BadRequest("Invalid file type. Only JPEG, PNG, and GIF are allowed.");
            }
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
