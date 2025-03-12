using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Rentora.Presentation.DTOs.Product
{
    public class UpdateProductDTO
    {
        // general
        [Required] public int ProductId { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }
        [Required] public int Quantity { get; set; } = 1;
        [Required] public decimal Price { get; set; }
        [Required] public string RentalPeriod { get; set; }
        [Required] public string Location { get; set; }
        [Required] public decimal Latitude { get; set; }
        [Required] public decimal Longitude { get; set; }
        [Required] public string Status { get; set; } = "Available";

        //Electronic & Sport
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Condition { get; set; } // Travel


        //Transportation
        public string Transmission { get; set; }
        public string Body_Type { get; set; }
        public string Fuel_Type { get; set; }
    }
}
