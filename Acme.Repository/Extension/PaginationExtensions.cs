using Acme.Models;
using Microsoft.EntityFrameworkCore;

namespace Acme.Repository.Extension
{
    public static class PaginationExtensions
    {
        public static async Task<Pagination<T>> ToPaginationAsync<T>(this IQueryable<T> query, int pageSize, int pageIndex)
        {
            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new Pagination<T>
            {
                Items = items,
                PageSize = pageSize,
                PageIndex = pageIndex,
                TotalCount = totalCount
            };
        }
    }
}
