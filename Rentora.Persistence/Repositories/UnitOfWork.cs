using Rentora.Persistence.Data.DbContext;

namespace Rentora.Application.IRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository products { get; }
        public IUserRepository users { get; }

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository, IUserRepository userRepository)
        {
            _context = context;
            products = productRepository;
            users = userRepository;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
             _context.Dispose();
        }
    }
}
