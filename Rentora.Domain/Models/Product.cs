namespace Rentora.Domain.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ApplicationUserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Images { get; set; } // JSON array of image URLs
        public int Quantity { get; set; } = 1;
        public decimal PricePerDay { get; set; }
        public string Location { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Status { get; set; } = "Available";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public Category Category { get; set; }
        public List<Rental> Rentals { get; set; }
        public List<Favorite> Favorites { get; set; }
        public List<Review> Reviews { get; set; }
        public List<RentalCart> RentalCarts { get; set; }
        public List<Report> Reports { get; set; }

    }
}
