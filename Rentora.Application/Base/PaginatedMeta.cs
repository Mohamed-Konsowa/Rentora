
namespace Rentora.Application.Base
{
    public  class PaginatedMeta
    {
        public int CurrentPage { get; set; }
        public bool Succeeded { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
    }
}
