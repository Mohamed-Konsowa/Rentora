using Microsoft.AspNetCore.Http;
using Rentora.Application.Features.Product.Commands.Models;
using Rentora.Domain.Models;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Application.IServices
{
    public interface IProductService
    {
        IQueryable<ProductDTO> GetProductsDTO();
        IQueryable<Product> GetProducts();
        Task<ProductDTO> GetProductDTOByIdAsync(int id);
        Task<Product> GetProductByIdAsync(int id);
        Task<ProductDTO> AddProductAsync(AddProductDTO productDto);
        Task<ProductDTO> UpdateProductAsync(UpdateProductCommand product);
        Task<Product> UpdateAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> AddProductImageAsync(int productId, IFormFile productImage);
        Task<bool> AddProductCategoryAsync<T>(T category) where T : class;
        Task<bool> UpdateProductCategoryAsync<T>(int categoryId, T category) where T : class;
        Task<int> GetProductSpecificCategoryIdAsync(int id);
        Task<List<ProductImage>> GetProductImagesByIdAsync(int productId);
        Task<bool> DeleteImageById(int imageId);
    }
}
