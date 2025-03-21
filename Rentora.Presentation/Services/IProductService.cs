﻿using Rentora.Application.DTOs.Product;
using Rentora.Domain.Models;
using Rentora.Presentation.DTOs.Product;

namespace Rentora.Presentation.Services
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductDTOById(int id);
        Task<Product> GetProductById(int id);
        Task<ProductDTO> AddProduct(AddProductDTO productDto);
        Task<ProductDTO> UpdateProduct(UpdateProductDTO product);
        Task<bool> DeleteProduct(int id);
        Task<bool> AddProductImage(ProductImageDTO productImage);
        Task<bool> AddProductCategory<T>(T category) where T : class;
        Task<bool> UpdateProductCategory<T>(int id, T category) where T : class;
        int GetProductSpecificCategoryId(int id);
        Task<List<ProductImage>> GetProductImagesByIdAsync(int productId);
        Task<bool> DeleteImageById(int imageId);
    }
}
