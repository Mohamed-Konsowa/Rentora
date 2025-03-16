using Rentora.Domain.Models;
using Rentora.Application.IRepositories;
using Rentora.Presentation.DTOs.Product;
using Rentora.Application.DTOs.Product;
using Rentora.Domain.Models.Categories;

namespace Rentora.Presentation.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            var temp = await _unitOfWork.products.GetAll();
            var products = temp.Select(p => new ProductDTO(p)).ToList();
            return products;
        }

        public async Task<ProductDTO> GetProductDTOById(int id)
        {
            var temp = await  _unitOfWork.products.GetById(id);
            var product = new ProductDTO(temp);
            switch (product.CategoryId)
            {
                case 1:
                    var SpecificCategory = await _unitOfWork.products.GetProductSpecificCategory<Sport>(1);
                    product.Brand = SpecificCategory.Brand;
                    product.Model = SpecificCategory.Model;
                    product.Condition = SpecificCategory.Condition;
                break;

                case 2:
                    var SpecificCategory2 = await _unitOfWork.products.GetProductSpecificCategory<Transportation>(1);
                    product.Transmission = SpecificCategory2.Transmission;
                    product.Body_Type = SpecificCategory2.Body_Type;
                    product.Fuel_Type = SpecificCategory2.Fuel_Type;
                break;

                case 3:
                    var SpecificCategory3 = await _unitOfWork.products.GetProductSpecificCategory<Electronic>(1);
                    product.Condition = SpecificCategory3.Condition;
                break;

                case 4:
                    var SpecificCategory4 = await _unitOfWork.products.GetProductSpecificCategory<Electronic>(1);
                    product.Brand = SpecificCategory4.Brand;
                    product.Model = SpecificCategory4.Model;
                    product.Condition = SpecificCategory4.Condition;
                break;
            }
            return product;
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = await _unitOfWork.products.GetById(id);            
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

        public async Task<bool> DeleteProduct(int id)
        {
            _unitOfWork.products.DeleteProductCategory(id);
            _unitOfWork.products.DeleteProductImages(id);
            var result = await _unitOfWork.products.Delete(id);
            await _unitOfWork.Save();
            return result;
        }

        public async Task<bool> AddProductImage(ProductImageDTO productImage)
        {
            var id = await GoogleDriveService.UploadFileToDriveAsync(productImage.Image);
            var result = await _unitOfWork.products.AddProductImage(new ProductImage()
            {
                ProductId = productImage.ProductId,
                Image = id
            });
            await _unitOfWork.Save();
            return result;
        }

        public async Task<bool> AddProductCategory<T>(T category) where T : class
        {
            var result = await _unitOfWork.products.AddProductSpecificCategory(category);
            await _unitOfWork.Save();
            return result != null;
        }

        public async Task<bool> UpdateProductCategory<T>(int id, T category) where T : class
        {
            var result = _unitOfWork.products.UpdateProductCategory(id, category);
            await _unitOfWork.Save();
            return result != null;
        }

        public int GetProductCategoryId(int id)
        {
            return _unitOfWork.products.GetProductSpecificCategoryId(id);
        }

        public async Task<List<ProductImage>> GetProductImagesByIdAsync(int productId)
        {
            var images = await _unitOfWork.products.GetProductImages(productId);
            foreach(var image in images) image.Image = 
                    await GoogleDriveService.GetFileAsBase64Async(image.Image);
            return images;
        }

        public async Task<bool> DeleteImageById(int imageId)
        {
            var image = await _unitOfWork.products.GetProductImageById(imageId);
            _unitOfWork.products.DeleteProductImage(image);
            await _unitOfWork.Save();
            if (image is null) return false;
            await GoogleDriveService.DeleteFileAsync(image.Image);
            return true;
        }
    }
}
