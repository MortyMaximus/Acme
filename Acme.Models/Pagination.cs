namespace Acme.Models
{
    public class Pagination<T>
    {
        public IReadOnlyList<T> Items { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalCount { get; set; }
        public int PageCount => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasNextPage => PageIndex < PageCount;
        public bool HasPreviousPage => PageIndex > 1;
    }
}
