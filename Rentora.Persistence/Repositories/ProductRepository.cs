using Microsoft.EntityFrameworkCore;
using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
using Rentora.Domain.Models.Categories;
using Rentora.Persistence.Data.DbContext;
using System.Linq.Expressions;

namespace Rentora.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        //specific tasks
        public async Task<T> AddProductSpecificCategoryAsync<T>(T category) where T : class 
        {
            var result = await _context.Set<T>().AddAsync(category);
            return category;
        }

        public async Task<T> UpdateProductCategoryAsync<T>(int categoryId, T category) where T : class
        {
            if (categoryId == 1)
            {
                var x = category as Sport;
                var cat = await _context.sports.FirstOrDefaultAsync(c => c.ProductId == x.Id);

                cat.Brand = x.Brand;
                cat.Model = x.Model;
                cat.Condition = x.Condition;
                _context.Set<Sport>().Update(cat);
            }
            else if (categoryId == 2)
            {
                var x = category as Transportation;
                var cat = await _context.Transportations.FirstOrDefaultAsync(c => c.ProductId == x.Id);

                cat.Transmission = x.Transmission;
                cat.Body_Type = x.Body_Type;
                cat.Fuel_Type = x.Fuel_Type;
                _context.Set<Transportation>().Update(cat);
            }
            else if (categoryId == 3)
            {
                var x = category as Travel;
                var cat = await _context.Travels.FirstOrDefaultAsync(c => c.ProductId == x.Id);

                cat.Condition = x.Condition;

                _context.Set<Travel>().Update(cat);
            }
            else if (categoryId == 4)
            {
                var x = category as Electronic;
                var cat = await _context.Electronics.FirstOrDefaultAsync(c => c.ProductId == x.Id);

                cat.Brand = x.Brand;
                cat.Model = x.Model;
                cat.Condition = x.Condition;
                _context.Set<Electronic>().Update(cat);
            }
            else return null;
            return category;
        }
        public async Task<bool> DeleteProductCategoryAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return false;

            var categoryId = product.CategoryId;

            bool result = true;
            switch (categoryId)
            {
                case 1: 
                    var x = _context.Set<Sport>().Where(c => c.ProductId == productId).FirstOrDefault();
                    _context.Set<Sport>().Remove(x);
                    break;
                case 2:
                    var y = _context.Set<Transportation>().Where(c => c.ProductId == productId).FirstOrDefault();
                    _context.Set<Transportation>().Remove(y);
                    break;
                case 3:
                    var z = _context.Set<Travel>().Where(c => c.ProductId == productId).FirstOrDefault();
                    _context.Set<Travel>().Remove(z);
                    break;
                case 4:
                    var z4 = _context.Set<Electronic>().Where(c => c.ProductId == productId).FirstOrDefault();
                    _context.Set<Electronic>().Remove(z4);
                    break;
            }
            return result;
        }
        public bool DeleteProductImages(int productId)
        {
            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            _context.ProductImages.RemoveRange(images);
            return true;
        }

        public async Task<bool> AddProductImageAsync(ProductImage productImage)
        {
            var result = await _context.ProductImages.AddAsync(productImage);
            return true;
        }

        public async Task<int> GetProductSpecificCategoryIdAsync(int productId)
        {
            var cat = await _context.sports.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (cat != null) return cat.ProductId;

            var cat2 = await _context.Transportations.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (cat2 != null) return cat2.ProductId;

            var cat3 = await _context.Travels.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (cat3 != null) return cat3.ProductId;

            var cat4 = await _context.Electronics.FirstOrDefaultAsync(s => s.ProductId == productId);
            if (cat4 != null) return cat4.ProductId;

            return -1;
        }

        public async Task<List<ProductImage>> GetProductImagesAsync(int productId)
        {
            return await _context.ProductImages.Where(i => i.ProductId == productId).ToListAsync();
        }

        public async Task<ProductImage> GetProductImageByIdAsync(int imageId)
        {
            return await _context.ProductImages.FindAsync(imageId);
        }

        public void DeleteProductImage(ProductImage productImage)
        {
            _context.ProductImages.Remove(productImage);
        }

        public async Task<T> GetProductSpecificCategoryAsync<T>(Expression<Func<T,bool>> expression) where T : class
        {
            return await _context.Set<T>().SingleOrDefaultAsync(expression);
        }
    }
}
