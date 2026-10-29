using System.Text;

namespace MiniSupermarket.WinForms;

internal static class CsvExportService
{
    private static string Cell(object? value)
    {
        string text = value?.ToString() ?? string.Empty;
        return $"\"{text.Replace("\"", "\"\"")}\"";
    }

    public static void WriteReport(string path, DashboardSummaryViewDto report)
    {
        var rows = new List<string>
        {
            "BÁO CÁO HỆ THỐNG BÁN LẺ THIÊN ÂN",
            $"Từ ngày,{Cell(report.FromDate.ToString("dd/MM/yyyy"))},Đến ngày,{Cell(report.ToDate.ToString("dd/MM/yyyy"))}",
            $"Tổng hóa đơn,{report.TotalOrders},Doanh thu,{report.RevenueToday}",
            $"Khách hàng mới,{report.NewCustomers},Hàng sắp hết,{report.LowStockItems}",
            string.Empty,
            "Ngày,Số hóa đơn,Doanh thu"
        };
        rows.AddRange(report.RevenueSeries.Select(x =>
            $"{Cell(x.Date.ToString("dd/MM/yyyy"))},{x.Orders},{x.Revenue}"));
        rows.Add(string.Empty);
        rows.Add("Mã vạch,Sản phẩm,Số lượng bán,Doanh thu");
        rows.AddRange(report.TopSellingProducts.Select(x =>
            $"{Cell(x.Barcode)},{Cell(x.ProductName)},{x.TotalSoldQuantity},{x.TotalSalesAmount}"));
        File.WriteAllLines(path, rows, new UTF8Encoding(true));
    }

    public static void WriteProducts(string path, IEnumerable<ProductItemViewDto> products)
    {
        var rows = new List<string> { "Mã SP,Mã vạch,Tên sản phẩm,Đơn giá,Tồn kho,Nhóm hàng" };
        rows.AddRange(products.Select(x =>
            $"{x.ProductId},{Cell(x.Barcode)},{Cell(x.ProductName)},{x.Price},{x.StockQuantity},{Cell(x.CategoryName)}"));
        File.WriteAllLines(path, rows, new UTF8Encoding(true));
    }
}
