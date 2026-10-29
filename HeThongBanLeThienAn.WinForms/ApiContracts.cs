namespace MiniSupermarket.WinForms;

public sealed class LoginRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public sealed class LoginResponseDto
{
    public bool Success { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}

public sealed class CategoryDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public sealed class CategoryUpsertRequestDto
{
    public string CategoryName { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class ProductDto
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
}

public sealed class ProductItemViewDto : ProductDto
{
    public string CategoryName { get; set; } = string.Empty;
}

public sealed class ProductLookupDto
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}

public sealed class ProductUpsertRequestDto
{
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
}

public sealed class CustomerDto
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public int RewardPoints { get; set; }
    public string MembershipRank { get; set; } = "Chuẩn";
}

public sealed class CustomerUpsertRequestDto
{
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Address { get; set; }
    public int RewardPoints { get; set; }
    public string MembershipRank { get; set; } = "Chuẩn";
}

public sealed class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public sealed class CreateUserRequestDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string Role { get; set; } = "Cashier";
}

public sealed class PagedViewResult<T>
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public List<T> Items { get; set; } = new();
}

public sealed class CheckoutRequestDto
{
    public string CashierUsername { get; set; } = string.Empty;
    public string? CustomerPhone { get; set; }
    public int RedeemPoints { get; set; }
    public List<CheckoutItemRequestDto> Items { get; set; } = new();
}

public sealed class CheckoutItemRequestDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

public sealed class CheckoutResponseDto
{
    public bool Success { get; set; }
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal FinalAmount { get; set; }
    public int TotalItems { get; set; }
    public int RedeemedPoints { get; set; }
    public int RewardPointsAdded { get; set; }
}

public sealed class OrderSummaryDto
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public string CashierUsername { get; set; } = string.Empty;
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; } = "Khách vãng lai";
    public int TotalItems { get; set; }
    public decimal FinalAmount { get; set; }
    public int RewardPoints { get; set; }
    public int RedeemedPoints { get; set; }
    public decimal DiscountAmount { get; set; }
}

public sealed class OrderDetailDto
{
    public int OrderDetailId { get; set; }
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}

public sealed class OrderDto
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public int? CustomerId { get; set; }
    public string CustomerName { get; set; } = "Khách vãng lai";
    public string CashierUsername { get; set; } = string.Empty;
    public int TotalItems { get; set; }
    public decimal FinalAmount { get; set; }
    public int RewardPoints { get; set; }
    public int RedeemedPoints { get; set; }
    public decimal DiscountAmount { get; set; }
    public List<OrderDetailDto> OrderDetails { get; set; } = new();
}

public sealed class DashboardSummaryViewDto
{
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public decimal RevenueToday { get; set; }
    public int TotalOrders { get; set; }
    public int NewCustomers { get; set; }
    public int LowStockItems { get; set; }
    public List<RevenuePointViewDto> RevenueSeries { get; set; } = new();
    public List<TopSellingProductViewDto> TopSellingProducts { get; set; } = new();
}

public sealed class QuickReportViewDto
{
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public string BestSeller { get; set; } = string.Empty;
    public int NewCustomers { get; set; }
    public int LowStockItems { get; set; }
}

public sealed class RevenuePointViewDto
{
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
    public int Orders { get; set; }
}

public sealed class TopSellingProductViewDto
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int TotalSoldQuantity { get; set; }
    public decimal TotalSalesAmount { get; set; }
}

public sealed class CashierRevenueViewDto
{
    public string CashierUsername { get; set; } = string.Empty;
    public int TotalOrders { get; set; }
    public decimal TotalRevenue { get; set; }
    public int TotalPointsAwarded { get; set; }
}

public sealed class CategoryProductCountViewDto
{
    public int CategoryId { get; set; }
    public int Total { get; set; }
}
