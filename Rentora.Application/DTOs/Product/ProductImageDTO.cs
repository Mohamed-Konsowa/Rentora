using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Application.DTOs.Product
{
    public class ProductImageDTO
    {
        public int ProductId { get; set; }
        public IFormFile Image { get; set; }
    }
}
