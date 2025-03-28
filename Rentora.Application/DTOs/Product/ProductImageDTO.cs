using Microsoft.AspNetCore.Http;

namespace Rentora.Application.DTOs.Product
{
    public class ProductImageDTO
    {
        public int ProductId { get; set; }
        public IFormFile Image { get; set; }
    }
}
