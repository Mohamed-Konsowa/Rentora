
using Rentora.Domain.Enums.Product;

namespace Rentora.Presentation.DTOs.Product
{
    public class ProductDTO
    {
        public ProductDTO()
        {
            
        }
        public ProductDTO(Rentora.Domain.Models.Product product)
        {
            ProductId = product.ProductId;
            ApplicationUserId = product.ApplicationUserId;
            CategoryId = product.CategoryId;
            Title = product.Title;
            Description = product.Description;
            Price = product.Price;
            Location = product.Location;
            Latitude = product.Latitude;
            Longitude = product.Longitude;
            Status = product.ProductStatus;
        }
        // general
        public int ProductId {  get; set; }
        public string ApplicationUserId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public ProductStatus Status { get; set; } 

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
