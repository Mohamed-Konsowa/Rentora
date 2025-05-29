using Rentora.Application.DTOs.Review;
using Rentora.Domain.Models;

namespace Rentora.Application.IServices
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(AddReviewDTO reviewDTO);
        Task<List<GetProductReviewsDTO>> GetProductReviewsAsync(int productId);
        Task<(int Count, float Rate)> GetProductRateAsync(int productId);
        Task<bool> IsUserReviewedBeforeAsync(string userId, int productId);
        Task<Review> GetReviewAsync(string userId, int productId);
        Task<bool> DeleteReviewAsync(int reviewId);
        Task<bool> UpdateReviewAsync(AddReviewDTO reviewDTO);
    }
}
