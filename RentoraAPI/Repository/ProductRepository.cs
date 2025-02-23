using RentoraAPI.Models;

namespace RentoraAPI.Repository
{
    public class ProductRepository : Repository<Product>
    {
        public ProductRepository(ApplicationDbContext _context) : base(_context)
        {

        }

        //specific tasks
    }
}
