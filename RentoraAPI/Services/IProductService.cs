using RentoraAPI.Models;

namespace RentoraAPI.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        Product AddProduct(Product product);
        Product UpdateProduct(Product product);
        bool DeleteProduct(int id);
    }
}
