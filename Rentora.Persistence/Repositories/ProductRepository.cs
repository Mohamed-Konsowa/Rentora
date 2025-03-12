using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
using Rentora.Domain.Models.Categories;
using Rentora.Persistence.Data.DbContext;

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
        public async Task<T> AddProductCategory<T>(T category) where T : class 
        {
            var result = await _context.Set<T>().AddAsync(category);
            return category;
        }

        public T UpdateProductCategory<T>(int id, T category) where T : class
        {
            if(id == 1)
            {
                var x = category as Sport;
                var cat = _context.sports.FirstOrDefault(c => c.ProductId == x.Id);
                
                cat.Brand = x.Brand;
                cat.Model = x.Model;
                cat.Condition = x.Condition;
                _context.Set<Sport>().Update(cat);
            }
            else if (id == 2)
            {
                var x = category as Transportation;
                var cat = _context.Transportations.FirstOrDefault(c => c.ProductId == x.Id);

                cat.Transmission = x.Transmission;
                cat.Body_Type = x.Body_Type;
                cat.Fuel_Type = x.Fuel_Type;
                _context.Set<Transportation>().Update(cat);
            }
            else if (id == 3)
            {
                var x = category as Travel;
                var cat = _context.Travels.FirstOrDefault(c => c.ProductId == x.Id);

                cat.Condition = x.Condition;

                _context.Set<Travel>().Update(cat);
            }
            else //4
            {
                var x = category as Electronic;
                var cat = _context.Electronics.FirstOrDefault(c => c.ProductId == x.Id);

                cat.Brand = x.Brand;
                cat.Model = x.Model;
                cat.Condition = x.Condition;
                _context.Set<Electronic>().Update(cat);
            }
            
            return category;
        }
        public bool DeleteProductCategory(int productId)
        {
            var categoryId = _context.Products.Find(productId).CategoryId;
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

        public async Task<bool> AddProductImage(ProductImage productImage)
        {
            var result = await _context.ProductImages.AddAsync(productImage);
            return true;
        }

        public int GetProductCategoryId(int productId)
        {
            var cat = _context.sports.FirstOrDefault(s => s.ProductId == productId);
            if (cat != null) return cat.ProductId;

            var cat2 = _context.Transportations.FirstOrDefault(s => s.ProductId == productId);
            if (cat2 != null) return cat2.ProductId;

            var cat3 = _context.Travels.FirstOrDefault(s => s.ProductId == productId);
            if (cat3 != null) return cat3.ProductId;

            var cat4 = _context.Electronics.FirstOrDefault(s => s.ProductId == productId);
            if (cat4 != null) return cat4.ProductId;

            return -1;
        }
    }
}
