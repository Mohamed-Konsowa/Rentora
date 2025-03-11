using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
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

        public async Task<T> AddProductCategory<T>(T category) where T : class 
        {
            var result = await _context.Set<T>().AddAsync(category);
            return category;
        }

        //specific tasks
        public async Task<bool> AddProductImage(ProductImage productImage)
        {
            var result = await _context.ProductImages.AddAsync(productImage);
            return true;
        }  
    }
}
