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

        public bool IsUserReviewedBefore(string userId, int productId)
        {
            var r = _context.Reviews.Where(r => r.ApplicationUserId == userId && r.ProductId == productId);
            return (r.Count() > 0) ;
        }
    }
}
