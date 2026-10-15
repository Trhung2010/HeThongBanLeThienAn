using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        public FormQuickReport()
        {
            InitializeComponent();
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            // Kiểm tra bảo vệ phân quyền cấp Form (Lớp bảo vệ thứ 2)
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối truy cập", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnRunReport.Enabled = false;
                return;
            }

            await LoadReportDataAsync();
        }

        private async void btnRunReport_Click(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private async Task LoadReportDataAsync()
        {
            try
            {
                string dateStr = dtpReportDate.Value.ToString("yyyy-MM-dd");
                var report = await ApiClientService.Client.GetFromJsonAsync<QuickReportDto>($"reports/quick?date={dateStr}");

                if (report != null)
                {
                    lblTotalOrders.Text = report.TotalOrders.ToString();
                    lblTotalRevenue.Text = $"{report.TotalRevenue:N0} đ";
                    lblBestSeller.Text = report.BestSeller;

                    lblReportNote.Text =
                        $"• Báo cáo ngày: {report.ReportDate}\n" +
                        $"• Tổng số giao dịch thành công: {report.TotalOrders} đơn hàng\n" +
                        $"• Tổng doanh số thu vào: {report.TotalRevenue:N0} VNĐ\n" +
                        $"• Sản phẩm có doanh số/sức mua cao nhất: {report.BestSeller}\n" +
                        $"• Dữ liệu được trích xuất từ cơ sở dữ liệu Bách Hóa Tổng Hợp Thiên Ân.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp báo cáo từ Server: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class QuickReportDto
    {
        public string ReportDate { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public string BestSeller { get; set; } = string.Empty;
    }
}
