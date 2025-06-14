using Microsoft.EntityFrameworkCore;
using Rentora.Application.Base;

namespace Rentora.Application.Wrappers
{
    public static class QueryableExtensions
    {
        public static async Task<Response<List<T>>> ToPaginatedListAsync<T>
            (this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null) throw new Exception("Empty");

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();

            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            
            var response = ResponseHandler.Success(items);
            response.Meta = new PaginatedMeta
            {
                CurrentPage = pageNumber,
                Succeeded = true,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                TotalCount = count
            };
            return response;
        }
        public static async Task<Response<List<T>>> ToPaginatedListAsync<T>
            (this List<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null) throw new Exception("Empty");

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            int count = source.Count();

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            var response = ResponseHandler.Success(items.ToList());
            response.Meta = new PaginatedMeta
            {
                CurrentPage = pageNumber,
                Succeeded = true,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                TotalCount = count
            };
            return response;
        }
    }
}
