
namespace Rentora.Application.DTOs.Review
{
    public class GetProductReviewsDTO
    {
        public int ProductReviewId { get; set; }
        public string ReviewerId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
    }
}
