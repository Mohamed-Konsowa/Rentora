namespace Rentora.Presentation.DTOs.Product
{
    public class ProductDTO
    {
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
    }
}
