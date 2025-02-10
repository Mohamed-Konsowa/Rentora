namespace RentoraAPI.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        // Navigation property
        public List<Product> Products { get; set; }
    }
}
