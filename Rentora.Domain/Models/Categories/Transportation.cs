
namespace Rentora.Domain.Models.Categories
{
    public class Transportation
    {
        public int Id { get; set; }
        public string Transmission { get; set; }
        public string Body_Type { get; set; }
        public string Fuel_Type { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
