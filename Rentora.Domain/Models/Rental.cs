namespace Rentora.Domain.Models
{
    public class Rental
    {
        public int RentalId { get; set; }
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending";
        public decimal PenaltyFee { get; set; } = 0.00m;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Product Product { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
