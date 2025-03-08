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

        //specific tasks
    }
}
