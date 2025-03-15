using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<T> AddProductSpecificCategory<T>(T category) where T : class;
        Task<T> GetProductSpecificCategory<T>(int specificId) where T : class;
        T UpdateProductCategory<T>(int id, T category) where T : class;
        bool DeleteProductCategory(int productId);
        bool DeleteProductImages(int productId);
        Task<bool> AddProductImage(ProductImage productImage);
        int GetProductSpecificCategoryId(int productId);
        Task<List<ProductImage>> GetProductImages(int productId);
        Task<ProductImage> GetProductImageById(int imageId);
        void DeleteProductImage(ProductImage productImage);
    }
}
