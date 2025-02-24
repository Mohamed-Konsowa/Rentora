using RentoraAPI.Models;
using RentoraAPI.Repository;

namespace RentoraAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Product> GetProducts()
        {
            var products = _unitOfWork.products.GetAll();
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = _unitOfWork.products.GetById(id);
            return product;
        }

        public Product AddProduct(Product product)
        {
            _unitOfWork.products.Add(product);
            _unitOfWork.Save();
            return product;
        }

        public Product UpdateProduct(Product product)
        {
            _unitOfWork.products.Update(product);
            _unitOfWork.Save();
            return product;
        }

        public bool DeleteProduct(int id)
        {
            var result = _unitOfWork.products.Delete(id);
            _unitOfWork.Save();
            return result;
        }

    }
}
