using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int? pageNumber, int? pageSize)
        {
            CurrentPage = pageNumber.HasValue ? pageNumber.Value : 1;
            TotalPages = pageSize.HasValue ? (int)Math.Ceiling(count / (double)pageSize) : 1;
            PageSize = pageSize.HasValue ? pageSize.Value : 1;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int? pageNumber, int? pageSize)
        {
            var count = await source.CountAsync();
            if (pageNumber is null && pageSize is null)
            {
                var items = await source.ToListAsync();
                return new PagedList<T>(items,count,1,count);
            }
            else
            {
                var items = await source.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value).ToListAsync();
                return new PagedList<T>(items, count, pageNumber.Value, pageSize.Value);
            }
        }
    }
}