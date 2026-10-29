namespace MiniSupermarket.API.Models
{
    public sealed class PagedResult<T>
    {
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
    }
}
