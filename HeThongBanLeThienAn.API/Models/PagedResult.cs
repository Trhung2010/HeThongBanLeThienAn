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

    public sealed class ProductItemDto
    {
        public int ProductId { get; init; }
        public string Barcode { get; init; } = string.Empty;
        public string ProductName { get; init; } = string.Empty;
        public decimal Price { get; init; }
        public int StockQuantity { get; init; }
        public int CategoryId { get; init; }
        public string CategoryName { get; init; } = string.Empty;
    }
}
