using Rentora.Domain.Models;
using Rentora.Application.IRepositories;
using Rentora.Presentation.DTOs.Product;
using Rentora.Domain.Models.Categories;
using Rentora.Application.IServices;
using Microsoft.AspNetCore.Http;
using Rentora.Application.Features.Product.Commands.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Rentora.Application.Features.Product.Queries.Models;
using Rentora.Application.DTOs.ProductImage;

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

        public async Task<List<ProductDTO>> GetProductsDTOAsync()
        {
            var temp = _unitOfWork.products.GetAll().ToList();
            var products = new List<ProductDTO>();
            foreach (var product in temp)
            {
                products.Add(await GetProductDTOByIdAsync(product.ProductId));
            }
            
            return products;
        }

        public async Task<List<ProductDTO>> GetProductsDTOPaginatedAsync(GetProductsPaginatedQuery request)
        {
            var allProducts = GetProducts().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var keywords = request.Search
                    .ToLowerInvariant()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var keyword in keywords)
                {
                    var temp = keyword;
                    allProducts = allProducts.Where(p =>
                        p.Title.ToLower().Contains(temp) ||
                        p.Description.ToLower().Contains(temp)
                    );
                }
            }

            if (request.FromPrice is not null)
                allProducts = allProducts.Where(p => p.Price >= request.FromPrice);

            if (request.ToPrice is not null)
                allProducts = allProducts.Where(p => p.Price <= request.ToPrice);

            if (request.CategoryId is not null)
            {
                allProducts = allProducts.Where
                (
                    p => p.CategoryId == request.CategoryId
                );
            }


            var products = new List<ProductDTO>();
            foreach (var product in allProducts)
            {
                products.Add(await GetProductDTOByIdAsync(product.ProductId));
            }

            return products;
        }
        public IQueryable<Product> GetProducts()
        {
            var products = _unitOfWork.products.GetAll();
            return products;
        }
        public async Task<ProductDTO> GetProductDTOByIdAsync(int id)
        {
            var temp = await _unitOfWork.products.GetByIdAsync(id);
            if (temp == null) return null;
            var product = new ProductDTO(temp);
            switch (product.CategoryId)
            {
                case 1:
                    var SpecificCategory = await _unitOfWork.products.GetProductSpecificCategoryAsync<Sport>(c => c.ProductId == id);
                    product.Brand = SpecificCategory.Brand;
                    product.Model = SpecificCategory.Model;
                    product.Condition = SpecificCategory.Condition;
                break;

                case 2:
                    var SpecificCategory2 = await _unitOfWork.products.GetProductSpecificCategoryAsync<Transportation>(c => c.ProductId == id);
                    product.Transmission = SpecificCategory2.Transmission;
                    product.Body_Type = SpecificCategory2.Body_Type;
                    product.Fuel_Type = SpecificCategory2.Fuel_Type;
                break;

                case 3:
                    var SpecificCategory3 = await _unitOfWork.products.GetProductSpecificCategoryAsync<Electronic>(c => c.ProductId == id);
                    product.Condition = SpecificCategory3.Condition;
                break;

                case 4:
                    var SpecificCategory4 = await _unitOfWork.products.GetProductSpecificCategoryAsync<Electronic>(c => c.ProductId == id);
                    product.Brand = SpecificCategory4.Brand;
                    product.Model = SpecificCategory4.Model;
                    product.Condition = SpecificCategory4.Condition;
                break;
            }
            return product;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.products.GetByIdAsync(id);            
            return product;
        }
        public async Task<ProductDTO> AddProductAsync(AddProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _unitOfWork.products.AddAsync(product);

            await _unitOfWork.SaveChangesAsync();
            
            switch (productDto.CategoryId)
            {
                case 1:
                    await AddProductCategoryAsync(new Sport()
                    {
                        ProductId = product.ProductId,
                        Brand = productDto.Brand,
                        Model = productDto.Model,
                        Condition = productDto.Condition
                    }); break;
                case 2:
                    await AddProductCategoryAsync(new Transportation()
                    {
                        ProductId = product.ProductId,
                        Transmission = productDto.Transmission,
                        Body_Type = productDto.Body_Type,
                        Fuel_Type = productDto.Fuel_Type
                    }); break;
                case 3:
                    await AddProductCategoryAsync(new Travel()
                    {
                        ProductId = product.ProductId,
                        Condition = productDto.Condition
                    }); break;
                case 4:
                    await AddProductCategoryAsync(new Electronic()
                    {
                        ProductId = product.ProductId,
                        Brand = productDto.Brand,
                        Model = productDto.Model,
                        Condition = productDto.Condition
                    }); break;
            }

            await _unitOfWork.SaveChangesAsync();
            return await GetProductDTOByIdAsync(product.ProductId);
        }

        public async Task<ProductDTO> UpdateProductAsync(UpdateProductCommand request)
        {
            var product = await GetProductByIdAsync((int)request.ProductId);
            if (product is null) return null;

            _mapper.Map(request, product);

            var categoryId = await GetProductSpecificCategoryIdAsync(product.ProductId);
            switch (product.CategoryId)
            {
                case 1:
                    await UpdateProductCategoryAsync(1, new Sport()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Brand = request.Brand,
                        Model = request.Model,
                        Condition = request.Condition
                    }); break;
                case 2:
                    await UpdateProductCategoryAsync(2, new Transportation()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Transmission = request.Transmission,
                        Body_Type = request.Body_Type,
                        Fuel_Type = request.Fuel_Type
                    }); break;
                case 3:
                    await UpdateProductCategoryAsync(3, new Travel()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Condition = request.Condition
                    }); break;
                case 4:
                    await UpdateProductCategoryAsync(4, new Electronic()
                    {
                        Id = categoryId,
                        ProductId = product.ProductId,
                        Brand = request.Brand,
                        Model = request.Model,
                        Condition = request.Condition
                    }); break;
            }

            _unitOfWork.products.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return await GetProductDTOByIdAsync(product.ProductId);
        }
        public async Task<Product> UpdateAsync(Product product)
        {
            var p = _unitOfWork.products.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return p;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                //await _unitOfWork.products.DeleteProductCategoryAsync(id);
                var images = await _unitOfWork.products.GetProductImagesAsync(id);
                _unitOfWork.products.DeleteProductImages(id);
                var result = _unitOfWork.products.Delete(id);
                
                await _unitOfWork.SaveChangesAsync();
                await transaction.CommitAsync();

                if(result) 
                    images.ForEach(async i => await _imageService.DeleteImageAsync(i.ImageUrl));

                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> AddProductImageAsync(int productId, IFormFile file)
        {
            var imageUrl = await _imageService.UploadImageAsync(file);
            var result = await _unitOfWork.products.AddProductImageAsync(new ProductImage()
            {
                ProductId = productId,
                Image = imageUrl
            });
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<bool> AddProductCategoryAsync<T>(T category) where T : class
        {
            var result = await _unitOfWork.products.AddProductSpecificCategoryAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return result != null;
        }

        public async Task<bool> UpdateProductCategoryAsync<T>(int categoryId, T category) where T : class
        {
            var result = _unitOfWork.products.UpdateProductCategoryAsync(categoryId, category);
            await _unitOfWork.SaveChangesAsync();
            return result != null;
        }

        public async Task<int> GetProductSpecificCategoryIdAsync(int id)
        {
            return await _unitOfWork.products.GetProductSpecificCategoryIdAsync(id);
        }

        public async Task<List<ProductImageDTO>> GetProductImagesByIdAsync(int productId)
        {
            var images = await _unitOfWork.products.GetProductImagesAsync(productId);
            return images;
        }

        public async Task<bool> DeleteImageById(int imageId)
        {
            var image = await _unitOfWork.products.GetProductImageByIdAsync(imageId);
            if (image is null) return false;

            await _imageService.DeleteImageAsync(image.Image);
            _unitOfWork.products.DeleteProductImage(image);
            await _unitOfWork.SaveChangesAsync();
                       
            return true;
        }
    }
}
