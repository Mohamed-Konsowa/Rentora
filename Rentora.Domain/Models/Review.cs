namespace Rentora.Domain.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }
        public float Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }
    }
}
