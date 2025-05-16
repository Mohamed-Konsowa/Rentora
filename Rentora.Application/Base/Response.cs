using System.Net;

namespace Rentora.Application.Base
{
    public class Response<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Dictionary<string, List<string>> Errors { get; set; }
        public T Data { get; set; }
    }
}
