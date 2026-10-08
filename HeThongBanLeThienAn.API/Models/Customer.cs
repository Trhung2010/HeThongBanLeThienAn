using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc!")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại là bắt buộc!")]
        public string PhoneNumber { get; set; } = string.Empty;

        public string? Address { get; set; }

        public int RewardPoints { get; set; } = 0;

        public string MembershipRank { get; set; } = "Chuẩn";
    }
}
