using Microsoft.EntityFrameworkCore.Storage;
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
        public IFavoriteRepository favorites {  get; }
        public IRentRepository rentals { get; }
        public IReviewRepository reviews { get; }
        public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository, 
            IUserRepository userRepository, IEmailRepository emailRepository, 
            ICartRepository cartRepository, IFavoriteRepository favoriteRepository,
            IRentRepository rentRepository, IReviewRepository reviewRepository)
        {
            _context = context;
            products = productRepository;
            users = userRepository;
            emails = emailRepository;
            carts = cartRepository;
            favorites = favoriteRepository;
            rentals = rentRepository;
            reviews = reviewRepository;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
