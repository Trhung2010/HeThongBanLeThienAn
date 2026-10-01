namespace MiniSupermarket.API.Models
{
    public class Category
    {
        // Mã định danh sản phẩm (Khóa chính)
        public int CategoryId { get; set; }

        // Tên sản phẩm (Bắt buộc, không được để trống)
        public string CategoryName { get; set; } = string.Empty;

        // Mô tả chi tiết về sản phẩm (Có thể để trống)
        public string? Description { get; set; }

        // Số lượng tồn kho hiện tại
        public int StockQuantity { get; set; }

    }
}
