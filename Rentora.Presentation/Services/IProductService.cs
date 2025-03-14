using Rentora.Domain.Models;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Presentation.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int id);
        Task<bool> AddProductImage(ProductImage productImage);
        Task<bool> AddProductCategory<T>(T category) where T : class;
        Task<bool> UpdateProductCategory<T>(int id, T category) where T : class;
        int GetProductCategoryId(int id);
    }
}
