using Rentora.Domain.Models;
using Rentora.Application.IRepositories;
using Rentora.Persistence.Helpers;

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

        public async Task<bool> AddProductImage(ProductImage productImage)
        {
            //var image = new ProductImage() {ProductId = id ,Image = imageBase64 };
            var result = await _unitOfWork.products.AddProductImage(productImage);
            return result;
        }

        public async Task<bool> AddProductCategory<T>(T category) where T : class
        {
            var result = await _unitOfWork.products.AddProductCategory(category);
            await _unitOfWork.Save();
            return result != null;
        }
    }
}
