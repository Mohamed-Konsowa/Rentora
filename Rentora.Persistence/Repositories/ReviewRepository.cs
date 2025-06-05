using Microsoft.EntityFrameworkCore;
using Rentora.Application.IRepositories;
using Rentora.Domain.Models;
using Rentora.Persistence.Data.DbContext;

namespace Rentora.Persistence.Repositories
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Review> GetProductReviews(int productId)
        {
            return _context.Reviews.Where(r => r.ProductId == productId);
        }

        public Task<Review> GetReviewAsync(string userId, int productId)
        {
            return _context.Reviews.Where(r => r.ApplicationUserId == userId && r.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserReviewedBeforeAsync(string userId, int productId)
        {
            return await _context.Reviews
                .AnyAsync(r => r.ApplicationUserId == userId && r.ProductId == productId); ;
        }
    }
}
