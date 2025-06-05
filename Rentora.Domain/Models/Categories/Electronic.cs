
namespace Rentora.Domain.Models.Categories
{
    public class Electronic
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Condition { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
