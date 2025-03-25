

namespace Rentora.Application.Helpers
{
    public class CustomResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
    }
}
