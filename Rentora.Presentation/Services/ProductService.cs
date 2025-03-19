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
        private readonly IImageService _imageService;

        public ProductService(IUnitOfWork unitOfWork, IImageService imageService)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
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
        public async Task<ProductDTO> AddProduct(AddProductDTO productDto)
        {

            var product = new Product()
            {
                ApplicationUserId = productDto.ApplicationUserId,
                CategoryId = productDto.CategoryId,
                Title = productDto.Title,
                Description = productDto.Description,
                Quantity = productDto.Quantity,
                Price = productDto.Price,
                RentalPeriod = productDto.RentalPeriod,
                Location = productDto.Location,
                Latitude = productDto.Latitude,
                Longitude = productDto.Longitude,
                Status = productDto.Status,
                CreatedAt = DateTime.UtcNow
            };
            await _unitOfWork.products.Add(product);

            await _unitOfWork.Save();
            
            switch (productDto.CategoryId)
            {
                case 1:
                    await AddProductCategory(new Sport()
                    {
                        ProductId = product.ProductId,
                        Brand = productDto.Brand,
                        Model = productDto.Model,
                        Condition = productDto.Condition
                    }); break;
                case 2:
                    await AddProductCategory(new Transportation()
                    {
                        ProductId = product.ProductId,
                        Transmission = productDto.Transmission,
                        Body_Type = productDto.Body_Type,
                        Fuel_Type = productDto.Fuel_Type
                    }); break;
                case 3:
                    await AddProductCategory(new Travel()
                    {
                        ProductId = product.ProductId,
                        Condition = productDto.Condition
                    }); break;
                case 4:
                    await AddProductCategory(new Electronic()
                    {
                        ProductId = product.ProductId,
                        Brand = productDto.Brand,
                        Model = productDto.Model,
                        Condition = productDto.Condition
                    }); break;
            }

            await _unitOfWork.Save();
            return await GetProductDTOById(product.ProductId);
        }

        public async Task<ProductDTO> UpdateProduct(UpdateProductDTO productDto)
        {
            var product = await GetProductById(productDto.ProductId);
            if (product is null) return null;

            product.Title = productDto.Title;
            product.Description = productDto.Description;
            product.Quantity = productDto.Quantity;
            product.Price = productDto.Price;
            product.RentalPeriod = productDto.RentalPeriod;
            product.Location = productDto.Location;
            product.Latitude = productDto.Latitude;
            product.Longitude = productDto.Longitude;
            product.Status = productDto.Status;

            var categoryId = GetProductSpecificCategoryId(product.ProductId);
            switch (product.CategoryId)
            {
                case 1:
                    await UpdateProductCategory(1, new Sport()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Brand = productDto.Brand,
                        Model = productDto.Model,
                        Condition = productDto.Condition
                    }); break;
                case 2:
                    await UpdateProductCategory(2, new Transportation()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Transmission = productDto.Transmission,
                        Body_Type = productDto.Body_Type,
                        Fuel_Type = productDto.Fuel_Type
                    }); break;
                case 3:
                    await UpdateProductCategory(3, new Travel()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Condition = productDto.Condition
                    }); break;
                case 4:
                    await UpdateProductCategory(4, new Electronic()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Brand = productDto.Brand,
                        Model = productDto.Model,
                        Condition = productDto.Condition
                    }); break;
            }

            await _unitOfWork.products.Update(product);
            await _unitOfWork.Save();
            return await GetProductDTOById(product.ProductId);
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
            var imageUrl = await _imageService.UploadImageAsync(productImage.Image);
            var result = await _unitOfWork.products.AddProductImage(new ProductImage()
            {
                ProductId = productImage.ProductId,
                Image = imageUrl
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

        public int GetProductSpecificCategoryId(int id)
        {
            return _unitOfWork.products.GetProductSpecificCategoryId(id);
        }

        public async Task<List<ProductImage>> GetProductImagesByIdAsync(int productId)
        {
            var images = await _unitOfWork.products.GetProductImages(productId);
            return images;
        }

        public async Task<bool> DeleteImageById(int imageId)
        {
            var image = await _unitOfWork.products.GetProductImageById(imageId);
            if (image is null) return false;

            await _imageService.DeleteImageAsync(image.Image);
            _unitOfWork.products.DeleteProductImage(image);
            await _unitOfWork.Save();
                       
            return true;
        }
    }
}
