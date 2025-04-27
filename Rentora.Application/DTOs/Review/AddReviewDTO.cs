
namespace Rentora.Application.DTOs.Review
{
    public class AddReviewDTO
    {
        public int ProductId { get; set; }
        public string ReviewerId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
    }
}
