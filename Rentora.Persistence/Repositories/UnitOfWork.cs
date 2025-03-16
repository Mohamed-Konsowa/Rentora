using Rentora.Persistence.Data.DbContext;

namespace Rentora.Application.IRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductRepository products { get; }
        public IUserRepository users { get; }
        public IEmailRepository emails {  get; }
        public ICartRepository carts { get; }

        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository, 
            IUserRepository userRepository, IEmailRepository emailRepository, ICartRepository cartRepository)
        {
            _context = context;
            products = productRepository;
            users = userRepository;
            emails = emailRepository;
            carts = cartRepository;
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
