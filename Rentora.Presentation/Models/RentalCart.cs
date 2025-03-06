namespace RentoraAPI.Models
{
    public class RentalCart
    {
        public int RentalCartId { get; set; }
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }
    }
}
