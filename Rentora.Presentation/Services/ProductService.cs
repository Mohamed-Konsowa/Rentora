using Rentora.Domain.Models;
using Rentora.Application.IRepositories;

namespace Rentora.Presentation.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _unitOfWork.products.GetAll();
            return products;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await  _unitOfWork.products.GetById(id);
            return product;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _unitOfWork.products.Add(product);
            await _unitOfWork.Save();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            await _unitOfWork.products.Update(product);
            await _unitOfWork.Save();
            return product;
        }

        public Task<bool> DeleteProduct(int id)
        {
            var result = _unitOfWork.products.Delete(id);
            _unitOfWork.Save();
            return result;
        }

    }
}
