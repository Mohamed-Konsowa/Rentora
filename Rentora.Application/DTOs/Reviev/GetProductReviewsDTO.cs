
namespace Rentora.Application.DTOs.Reviev
{
    public class GetProductReviewsDTO
    {
        public string ProductReviewId { get; set; }
        public string ReviewerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
