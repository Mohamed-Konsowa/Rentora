using Rentora.Domain.Models;
using System.Linq.Expressions;

namespace Rentora.Application.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<T> AddProductSpecificCategory<T>(T category) where T : class;
        Task<T> GetProductSpecificCategory<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> UpdateProductCategoryAsync<T>(int id, T category) where T : class;
        Task<bool> DeleteProductCategory(int productId);
        bool DeleteProductImages(int productId);
        Task<bool> AddProductImage(ProductImage productImage);
        int GetProductSpecificCategoryId(int productId);
        Task<List<ProductImage>> GetProductImages(int productId);
        Task<ProductImage> GetProductImageById(int imageId);
        void DeleteProductImage(ProductImage productImage);
    }
}
