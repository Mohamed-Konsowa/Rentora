using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<T> AddProductCategory<T>(T category) where T : class;
        Task<bool> AddProductImage(ProductImage productImage);
    }
}
