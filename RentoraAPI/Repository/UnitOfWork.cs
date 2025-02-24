using RentoraAPI.Models;

namespace RentoraAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository products { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            products = new ProductRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
