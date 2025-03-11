using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Domain.Models.Categories
{
    public class Sport
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Condition { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
