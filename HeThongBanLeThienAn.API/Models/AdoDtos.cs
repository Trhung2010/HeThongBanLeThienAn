using System;

namespace MiniSupermarket.API.Models
{
    /// <summary>
    /// DTO tra cứu sản phẩm cho quầy POS qua ADO.NET
    /// </summary>
    public class ProductLookupDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
    }

    /// <summary>
    /// DTO thống kê số lượng sản phẩm theo từng nhóm danh mục (Bài tập 2.2)
    /// </summary>
    public class CategoryProductCountDto
    {
        public int CategoryId { get; set; }
        public int Total { get; set; }
    }

    /// <summary>
    /// DTO hứng kết quả thống kê doanh thu thu ngân từ Stored Procedure (Tiết 3)
    /// </summary>
    public class CashierRevenueDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalPointsAwarded { get; set; }
    }

    /// <summary>
    /// DTO thống kê Top 5 sản phẩm bán chạy nhất từ Stored Procedure (Bài tập 3.2)
    /// </summary>
    public class TopSellingProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int TotalSoldQuantity { get; set; }
        public decimal TotalSalesAmount { get; set; }
    }

    /// <summary>
    /// DTO biểu diễn từng dòng sản phẩm trong giỏ hàng gửi lên quầy POS (Tiết 5)
    /// </summary>
    public class CartItemCheckoutDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    /// <summary>
    /// DTO nhận yêu cầu thanh toán tổng thể từ máy POS (Tiết 5)
    /// </summary>
    public class PosCheckoutRequestDto
    {
        public string CashierUsername { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public List<CartItemCheckoutDto> Items { get; set; } = new();
    }
}
