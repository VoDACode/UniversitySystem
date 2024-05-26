using Microsoft.EntityFrameworkCore;
using University.Domain.Requests;

namespace University.Domain.Responses
{
    public class PageResponse<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; } = 0;
        public int PageCount { get; set; } = 0;
        public int CurrentPage { get; set; } = 0;

        public PageResponse(IEnumerable<T> items, int totalCount, int pageCount, int currentPage)
        {
            Items = items;
            TotalCount = totalCount;
            PageCount = pageCount;
            CurrentPage = currentPage;
        }

        public PageResponse(IEnumerable<T> items, int totalCount, PageRequest pageRequest)
        {
            Items = items;
            TotalCount = totalCount;
            PageCount = (int)Math.Ceiling(totalCount / 10.0);
            CurrentPage = pageRequest.Page;
        }

        public PageResponse() { }

        public static async Task<PageResponse<T>> Create(IQueryable<T> source, PageRequest pageRequest)
        {
            var totalCount = await source.CountAsync();
            var items = await source.Skip(10 * (pageRequest.Page - 1)).Take(10).ToListAsync();
            return new PageResponse<T>(items, totalCount, pageRequest);
        }
    }
}
