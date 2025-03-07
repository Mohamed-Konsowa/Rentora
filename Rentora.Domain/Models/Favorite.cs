namespace Rentora.Domain.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public string ApplicationUserId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public ApplicationUser ApplicationUser { get; set; }
        public Product Product { get; set; }
    }
}
