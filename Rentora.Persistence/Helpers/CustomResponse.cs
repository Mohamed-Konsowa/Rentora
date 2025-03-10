using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentora.Persistence.Helpers
{
    public class CustomResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
