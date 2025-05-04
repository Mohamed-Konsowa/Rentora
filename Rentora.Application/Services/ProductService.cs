using Rentora.Domain.Models;
using Rentora.Application.IRepositories;
using Rentora.Presentation.DTOs.Product;
using Rentora.Domain.Models.Categories;
using Rentora.Application.IServices;
using Microsoft.AspNetCore.Http;
using Rentora.Application.Features.Product.Commands.Models;
using AutoMapper;

namespace Rentora.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IImageService imageService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetProducts()
        {
            var temp = _unitOfWork.products.GetAll();
            var products = temp.Select(p => new ProductDTO(p)).ToList();
            return products;
        }

        public async Task<ProductDTO> GetProductDTOById(int id)
        {
            var temp = _unitOfWork.products.GetById(id);
            if (temp == null) return null;
            var product = new ProductDTO(temp);
            switch (product.CategoryId)
            {
                case 1:
                    var SpecificCategory = await _unitOfWork.products.GetProductSpecificCategory<Sport>(c => c.ProductId == id);
                    product.Brand = SpecificCategory.Brand;
                    product.Model = SpecificCategory.Model;
                    product.Condition = SpecificCategory.Condition;
                break;

                case 2:
                    var SpecificCategory2 = await _unitOfWork.products.GetProductSpecificCategory<Transportation>(c => c.ProductId == id);
                    product.Transmission = SpecificCategory2.Transmission;
                    product.Body_Type = SpecificCategory2.Body_Type;
                    product.Fuel_Type = SpecificCategory2.Fuel_Type;
                break;

                case 3:
                    var SpecificCategory3 = await _unitOfWork.products.GetProductSpecificCategory<Electronic>(c => c.ProductId == id);
                    product.Condition = SpecificCategory3.Condition;
                break;

                case 4:
                    var SpecificCategory4 = await _unitOfWork.products.GetProductSpecificCategory<Electronic>(c => c.ProductId == id);
                    product.Brand = SpecificCategory4.Brand;
                    product.Model = SpecificCategory4.Model;
                    product.Condition = SpecificCategory4.Condition;
                break;
            }
            return product;
        }
        public async Task<Product> GetProductById(int id)
        {
            var product = _unitOfWork.products.GetById(id);            
            return product;
        }
        public async Task<ProductDTO> AddProduct(AddProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.products.AddAsync(product);

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

        public async Task<ProductDTO> UpdateProduct(UpdateProductCommand request)
        {
            var product = await GetProductById(request.ProductId);
            if (product is null) return null;

            _mapper.Map(request, product);

            var categoryId = GetProductSpecificCategoryId(product.ProductId);
            switch (product.CategoryId)
            {
                case 1:
                    await UpdateProductCategory(1, new Sport()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Brand = request.Brand,
                        Model = request.Model,
                        Condition = request.Condition
                    }); break;
                case 2:
                    await UpdateProductCategory(2, new Transportation()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Transmission = request.Transmission,
                        Body_Type = request.Body_Type,
                        Fuel_Type = request.Fuel_Type
                    }); break;
                case 3:
                    await UpdateProductCategory(3, new Travel()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Condition = request.Condition
                    }); break;
                case 4:
                    await UpdateProductCategory(4, new Electronic()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Brand = request.Brand,
                        Model = request.Model,
                        Condition = request.Condition
                    }); break;
            }

            _unitOfWork.products.Update(product);
            await _unitOfWork.Save();
            return await GetProductDTOById(product.ProductId);
        }
        public async Task<Product> UpdateAsync(Product product)
        {
            var p = _unitOfWork.products.Update(product);
            await _unitOfWork.Save();
            return p;
        }
        public async Task<bool> DeleteProduct(int id)
        {
            _unitOfWork.products.DeleteProductCategory(id);
            _unitOfWork.products.DeleteProductImages(id);
            var result = _unitOfWork.products.Delete(id);
            await _unitOfWork.Save();
            return result;
        }

        public async Task<bool> AddProductImage(int productId, IFormFile file)
        {
            var imageUrl = await _imageService.UploadImageAsync(file);
            var result = await _unitOfWork.products.AddProductImage(new ProductImage()
            {
                ProductId = productId,
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
