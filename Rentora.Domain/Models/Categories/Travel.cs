
namespace Rentora.Domain.Models.Categories
{
    public class Travel
    {
        public int Id { get; set; }
        public string Condition { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
