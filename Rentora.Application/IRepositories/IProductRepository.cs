using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<T> AddProductCategory<T>(T category) where T : class;
        T UpdateProductCategory<T>(int id, T category) where T : class;
        bool DeleteProductCategory(int productId);
        bool DeleteProductImages(int productId);
        Task<bool> AddProductImage(ProductImage productImage);
        int GetProductCategoryId(int productId);
    }
}
