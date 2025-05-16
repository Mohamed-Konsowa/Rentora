using Rentora.Application.DTOs.Review;

namespace Rentora.Application.IServices
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(AddReviewDTO reviewDTO);
        Task<List<GetProductReviewsDTO>> GetProductReviewsAsync(int productId);
        Task<bool> IsUserReviewedBeforeAsync(string userId, int productId);
    }
}
