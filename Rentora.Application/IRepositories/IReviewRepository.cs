using Rentora.Domain.Models;

namespace Rentora.Application.IRepositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        public IQueryable<Review> GetProductReviews(int productId);
        Task<bool> IsUserReviewedBeforeAsync(string userId, int productId);
        Task<Review> GetReviewAsync(string userId, int productId);
    }
}
