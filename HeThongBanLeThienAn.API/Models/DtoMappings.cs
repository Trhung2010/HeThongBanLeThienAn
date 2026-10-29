using System;
using System.Linq;

namespace MiniSupermarket.API.Models;

public static class DtoMappings
{
    public static CategoryDto ToDto(this Category category) => new()
    {
        CategoryId = category.CategoryId,
        CategoryName = category.CategoryName,
        Description = category.Description
    };

    public static CustomerDto ToDto(this Customer customer) => new()
    {
        CustomerId = customer.CustomerId,
        CustomerName = customer.CustomerName,
        PhoneNumber = customer.PhoneNumber,
        Address = customer.Address,
        RewardPoints = customer.RewardPoints,
        MembershipRank = customer.MembershipRank,
        CreatedDate = customer.CreatedDate
    };

    public static ProductDto ToDto(this Product product, string? categoryName = null) => new()
    {
        ProductId = product.ProductId,
        Barcode = product.Barcode,
        ProductName = product.ProductName,
        Price = product.Price,
        DataSource = product.DataSource,
        StockQuantity = product.StockQuantity,
        CategoryId = product.CategoryId,
        CategoryName = categoryName ?? product.Category?.CategoryName ?? string.Empty,
        IsLowStock = product.IsLowStock
    };

    public static ProductLookupDto ToLookupDto(this Product product) => new()
    {
        ProductId = product.ProductId,
        Barcode = product.Barcode,
        ProductName = product.ProductName,
        Price = product.Price,
        StockQuantity = product.StockQuantity
    };

    public static UserDto ToDto(this User user) => new()
    {
        Id = user.UserId,
        Username = user.Username,
        FullName = user.FullName,
        Role = user.Role,
        IsActive = user.IsActive
    };

    public static UserDto ToDto(this MiniSupermarket.API.Data.UserAccount user) => new()
    {
        Id = user.Id,
        Username = user.Username,
        FullName = user.FullName,
        Role = user.Role,
        IsActive = user.IsActive
    };

    public static OrderSummaryDto ToSummaryDto(this Order order) => new()
    {
        OrderId = order.OrderId,
        OrderCode = order.OrderCode,
        CreatedDate = order.CreatedDate,
        CashierUsername = order.CashierUsername,
        CustomerId = order.CustomerId,
        CustomerName = order.Customer != null ? order.Customer.CustomerName : "Khách vãng lai",
        TotalItems = order.TotalItems,
        FinalAmount = order.FinalAmount,
        RewardPoints = order.RewardPoints,
        RedeemedPoints = order.RedeemedPoints,
        DiscountAmount = order.DiscountAmount
    };

    public static OrderDetailDto ToDto(this OrderDetail detail) => new()
    {
        OrderDetailId = detail.OrderDetailId,
        ProductId = detail.ProductId,
        Barcode = detail.Product == null ? string.Empty : detail.Product.Barcode,
        ProductName = detail.Product == null ? string.Empty : detail.Product.ProductName,
        Quantity = detail.Quantity,
        UnitPrice = detail.UnitPrice,
        LineTotal = detail.LineTotal
    };

    public static OrderDto ToDto(this Order order) => new()
    {
        OrderId = order.OrderId,
        OrderCode = order.OrderCode,
        CreatedDate = order.CreatedDate,
        CustomerId = order.CustomerId,
        CustomerName = order.Customer != null ? order.Customer.CustomerName : "Khách vãng lai",
        CashierUsername = order.CashierUsername,
        TotalItems = order.TotalItems,
        FinalAmount = order.FinalAmount,
        RewardPoints = order.RewardPoints,
        RedeemedPoints = order.RedeemedPoints,
        DiscountAmount = order.DiscountAmount,
        OrderDetails = order.OrderDetails != null
            ? order.OrderDetails.OrderBy(d => d.OrderDetailId).Select(d => d.ToDto()).ToList()
            : Array.Empty<OrderDetailDto>()
    };
}
