using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Models;

public sealed class CategoryDto
{
    public int CategoryId { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public string? Description { get; init; }
}

public sealed class CategoryUpsertDto
{
    [Required, StringLength(100)]
    public string CategoryName { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Description { get; set; }
}

public sealed class CustomerDto
{
    public int CustomerId { get; init; }
    public string CustomerName { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string? Address { get; init; }
    public int RewardPoints { get; init; }
    public string MembershipRank { get; init; } = "Chuẩn";
    public DateTime CreatedDate { get; init; }
}

public sealed class CustomerUpsertDto
{
    [Required, StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required, StringLength(15)]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Address { get; set; }

    [Range(0, int.MaxValue)]
    public int RewardPoints { get; set; }

    [StringLength(50)]
    public string MembershipRank { get; set; } = "Chuẩn";
}

public sealed class ProductDto
{
    public int ProductId { get; init; }
    public string Barcode { get; init; } = string.Empty;
    public string ProductName { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string DataSource { get; init; } = string.Empty;
    public int StockQuantity { get; init; }
    public int CategoryId { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public bool IsLowStock { get; init; }
}

public sealed class ProductUpsertDto
{
    [Required, StringLength(50), RegularExpression(@"^[0-9]+$")]
    public string Barcode { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string ProductName { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}

public sealed class OrderSummaryDto
{
    public int OrderId { get; init; }
    public string OrderCode { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
    public string CashierUsername { get; init; } = string.Empty;
    public int? CustomerId { get; init; }
    public string CustomerName { get; init; } = "Khách vãng lai";
    public int TotalItems { get; init; }
    public decimal FinalAmount { get; init; }
    public int RewardPoints { get; init; }
    public int RedeemedPoints { get; init; }
    public decimal DiscountAmount { get; init; }
}

public sealed class OrderDetailDto
{
    public int OrderDetailId { get; init; }
    public int ProductId { get; init; }
    public string Barcode { get; init; } = string.Empty;
    public string ProductName { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal LineTotal { get; init; }
}

public sealed class OrderDto
{
    public int OrderId { get; init; }
    public string OrderCode { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
    public int? CustomerId { get; init; }
    public string CustomerName { get; init; } = "Khách vãng lai";
    public string CashierUsername { get; init; } = string.Empty;
    public int TotalItems { get; init; }
    public decimal FinalAmount { get; init; }
    public int RewardPoints { get; init; }
    public int RedeemedPoints { get; init; }
    public decimal DiscountAmount { get; init; }
    public IReadOnlyList<OrderDetailDto> OrderDetails { get; init; } = Array.Empty<OrderDetailDto>();
}

public sealed class UserDto
{
    public int Id { get; init; }
    public string Username { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}

public sealed class CreateUserDto
{
    [Required, StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Role { get; set; } = "Cashier";
}

public sealed class LoginRequestDto
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public sealed class LoginResponseDto
{
    public bool Success { get; init; }
    public string Token { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string Username { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
}

public sealed class CheckoutResponseDto
{
    public bool Success { get; init; }
    public int OrderId { get; init; }
    public string OrderCode { get; init; } = string.Empty;
    public decimal Subtotal { get; init; }
    public decimal DiscountAmount { get; init; }
    public decimal TotalAmount { get; init; }
    public decimal FinalAmount { get; init; }
    public int TotalItems { get; init; }
    public int RedeemedPoints { get; init; }
    public int RewardPointsAdded { get; init; }
    public string Cashier { get; init; } = string.Empty;
    public string? Customer { get; init; }
    public DateTime Timestamp { get; init; }
}

public sealed class QuickReportDto
{
    public int TotalOrders { get; init; }
    public decimal TotalRevenue { get; init; }
    public string BestSeller { get; init; } = string.Empty;
    public int NewCustomers { get; init; }
    public int LowStockItems { get; init; }
}

public sealed class CashierRevenueReportDto
{
    public bool Success { get; init; } = true;
    public int TotalCashiers { get; init; }
    public IReadOnlyList<CashierRevenueDto> Data { get; init; } = Array.Empty<CashierRevenueDto>();
}

public sealed class DateRangeRevenueReportDto
{
    public bool Success { get; init; } = true;
    public string FromDate { get; init; } = string.Empty;
    public string ToDate { get; init; } = string.Empty;
    public IReadOnlyList<CashierRevenueDto> Data { get; init; } = Array.Empty<CashierRevenueDto>();
}

public sealed class TopSellingReportDto
{
    public bool Success { get; init; } = true;
    public int TotalItems { get; init; }
    public IReadOnlyList<TopSellingProductDto> Data { get; init; } = Array.Empty<TopSellingProductDto>();
}

public sealed class CachedProductsReportDto
{
    public int TotalLoaded { get; init; }
    public string CachedAt { get; init; } = string.Empty;
    public IReadOnlyList<ProductLookupDto> Data { get; init; } = Array.Empty<ProductLookupDto>();
}

public sealed class StockFilterReportDto
{
    public int MinStockThreshold { get; init; }
    public int TotalFound { get; init; }
    public IReadOnlyList<ProductLookupDto> Data { get; init; } = Array.Empty<ProductLookupDto>();
}

public sealed class CategoryProductCountReportDto
{
    public int TotalCategories { get; init; }
    public IReadOnlyList<CategoryProductCountDto> Data { get; init; } = Array.Empty<CategoryProductCountDto>();
}

public sealed class MessageResponseDto
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public string? ErrorDetail { get; init; }
}
