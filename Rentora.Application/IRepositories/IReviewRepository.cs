
using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        public IQueryable<Review> GetProductReviews(int productId);
        bool IsUserReviewedBefore(string userId, int productId);
    }
}
