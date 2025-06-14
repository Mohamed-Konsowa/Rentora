using Rentora.Application.DTOs.ProductImage;
using Rentora.Domain.Models;
using System.Linq.Expressions;

namespace Rentora.Application.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<T> AddProductSpecificCategoryAsync<T>(T category) where T : class;
        Task<T> GetProductSpecificCategoryAsync<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> UpdateProductCategoryAsync<T>(int categoryId, T category) where T : class;
        Task<bool> DeleteProductCategoryAsync(int productId);
        bool DeleteProductImages(int productId);
        Task<bool> AddProductImageAsync(ProductImage productImage);
        Task<int> GetProductSpecificCategoryIdAsync(int productId);
        Task<List<ProductImageDTO>> GetProductImagesAsync(int productId);
        Task<ProductImage> GetProductImageByIdAsync(int imageId);
        void DeleteProductImage(ProductImage productImage);
    }
}
