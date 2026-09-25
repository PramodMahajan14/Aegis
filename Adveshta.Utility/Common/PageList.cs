using Microsoft.EntityFrameworkCore;

namespace Adveshta.Utility.Common
{
    public class PageList<T>
    {
        public List<T> Items { get; }

        public int Page { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public bool HasNextPage => Page * PageSize < TotalCount;

        public bool hasPreviousPage => PageSize > 1;


        private PageList(List<T> items, int page, int pagesize, int totalCount)
        {
            Items = items;
            Page = page;
            PageSize = pagesize;
            TotalCount = totalCount;
        }


        public static async Task<PageList<T>> CreateAsync(IQueryable<T> query, int page, int pagesize)
        {
            var totalCount = await query.CountAsync();

            var listItem = await query.Skip((page - 1) * pagesize).Take(pagesize).ToListAsync();

            return new PageList<T>(listItem, page, pagesize, totalCount);

        }



    }
}