using System.Windows.Forms.DataVisualization.Charting;
using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormQuickReport : Form
    {
        private readonly DateTimePicker _fromDate = new();
        private readonly Button _btnExportCsv = new();
        private readonly Label _lblLowStock = new();
        private readonly RevenueBarChart _revenueChart = new();
        private readonly DataGridView _topSellingGrid = new();
        private DashboardSummaryViewDto? _currentReport;

        public FormQuickReport()
        {
            InitializeComponent();
            ConfigureAdvancedReportUi();
        }

        private void ConfigureAdvancedReportUi()
        {
            lblFilter.Text = "Từ ngày:";
            lblRevenueTitle.Text = "DOANH THU HÔM NAY";
            _fromDate.Format = DateTimePickerFormat.Short;
            _fromDate.Value = DateTime.Today.AddDays(-6);
            _fromDate.Font = new Font("Segoe UI", 10F);
            _fromDate.SetBounds(95, 17, 130, 30);
            pnlFilter.Controls.Add(_fromDate);

            dtpReportDate.SetBounds(310, 17, 130, 30);
            pnlFilter.Controls.Add(new Label
            {
                Text = "Đến ngày:", AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(235, 20)
            });
            btnRunReport.SetBounds(455, 14, 145, 35);

            _btnExportCsv.Text = "Xuất CSV";
            _btnExportCsv.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            _btnExportCsv.SetBounds(615, 14, 125, 35);
            _btnExportCsv.Click += btnExportCsv_Click;
            pnlFilter.Controls.Add(_btnExportCsv);

            pnlCardOrders.SetBounds(0, 15, 240, 140);
            pnlCardRevenue.SetBounds(255, 15, 240, 140);
            pnlCardBestSeller.SetBounds(510, 15, 240, 140);
            lblBestSellerTitle.Text = "KHÁCH HÀNG MỚI";
            lblBestSeller.Text = "0";
            lblBestSeller.Font = new Font("Segoe UI", 26F, FontStyle.Bold);

            var lowStockPanel = new Panel
            {
                BackColor = Color.FromArgb(192, 57, 43),
                Location = new Point(765, 15), Size = new Size(255, 140)
            };
            lowStockPanel.Controls.Add(new Label
            {
                Text = "HÀNG SẮP HẾT", AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White, Location = new Point(18, 18)
            });
            _lblLowStock.Text = "0";
            _lblLowStock.AutoSize = true;
            _lblLowStock.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            _lblLowStock.ForeColor = Color.White;
            _lblLowStock.Location = new Point(18, 55);
            lowStockPanel.Controls.Add(_lblLowStock);
            pnlCards.Controls.Add(lowStockPanel);

            pnlChartOrDetail.Controls.Clear();
            var split = new SplitContainer
            {
                Dock = DockStyle.Fill, Orientation = Orientation.Horizontal,
                SplitterDistance = 205, IsSplitterFixed = true
            };
            _revenueChart.Dock = DockStyle.Fill;
            _revenueChart.BackColor = Color.White;
            split.Panel1.Controls.Add(_revenueChart);

            _topSellingGrid.Dock = DockStyle.Fill;
            _topSellingGrid.ReadOnly = true;
            _topSellingGrid.AllowUserToAddRows = false;
            _topSellingGrid.AllowUserToDeleteRows = false;
            _topSellingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _topSellingGrid.BackgroundColor = Color.White;
            split.Panel2.Controls.Add(_topSellingGrid);
            pnlChartOrDetail.Controls.Add(split);
        }

        private async void FormQuickReport_Load(object sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Bạn không có quyền xem dữ liệu tài chính của siêu thị!", "Từ chối truy cập",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnRunReport.Enabled = false;
                _btnExportCsv.Enabled = false;
                return;
            }
            await LoadReportDataAsync();
        }

        private async void btnRunReport_Click(object sender, EventArgs e) => await LoadReportDataAsync();

        private async Task LoadReportDataAsync()
        {
            if (_fromDate.Value.Date > dtpReportDate.Value.Date)
            {
                MessageBox.Show("Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.", "Khoảng ngày không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string from = _fromDate.Value.ToString("yyyy-MM-dd");
                string to = dtpReportDate.Value.ToString("yyyy-MM-dd");
                _currentReport = await ApiClientService.Client.GetFromJsonAsync<DashboardSummaryViewDto>(
                    $"reports/dashboard-summary?fromDate={from}&toDate={to}");
                if (_currentReport == null) throw new InvalidOperationException("Máy chủ không trả dữ liệu báo cáo.");

                lblTotalOrders.Text = _currentReport.TotalOrders.ToString("N0");
                lblTotalRevenue.Text = $"{_currentReport.RevenueToday:N0} đ";
                lblBestSeller.Text = _currentReport.NewCustomers.ToString("N0");
                _lblLowStock.Text = _currentReport.LowStockItems.ToString("N0");
                _revenueChart.SetData(_currentReport.RevenueSeries);
                _topSellingGrid.DataSource = _currentReport.TopSellingProducts;
                FormatTopSellingGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể nạp báo cáo. Kiểm tra máy chủ và thử lại.\n" + ex.Message,
                    "Lỗi báo cáo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatTopSellingGrid()
        {
            if (_topSellingGrid.Columns[nameof(TopSellingProductViewDto.ProductId)] != null)
                _topSellingGrid.Columns[nameof(TopSellingProductViewDto.ProductId)].Visible = false;
            SetHeader(nameof(TopSellingProductViewDto.Barcode), "Mã vạch");
            SetHeader(nameof(TopSellingProductViewDto.ProductName), "Sản phẩm");
            SetHeader(nameof(TopSellingProductViewDto.Price), "Đơn giá", "N0");
            SetHeader(nameof(TopSellingProductViewDto.TotalSoldQuantity), "Đã bán");
            SetHeader(nameof(TopSellingProductViewDto.TotalSalesAmount), "Doanh thu", "N0");
        }

        private void SetHeader(string name, string header, string? format = null)
        {
            if (_topSellingGrid.Columns[name] is not DataGridViewColumn column) return;
            column.HeaderText = header;
            if (format != null) column.DefaultCellStyle.Format = format;
        }

        private void btnExportCsv_Click(object? sender, EventArgs e)
        {
            if (_currentReport == null)
            {
                MessageBox.Show("Hãy chạy báo cáo trước khi xuất file.");
                return;
            }
            using var dialog = new SaveFileDialog
            {
                Filter = "CSV UTF-8 (*.csv)|*.csv",
                FileName = $"BaoCaoThienAn_{_currentReport.FromDate:yyyyMMdd}_{_currentReport.ToDate:yyyyMMdd}.csv"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                CsvExportService.WriteReport(dialog.FileName, _currentReport);
                MessageBox.Show("Đã xuất báo cáo CSV thành công.", "Hoàn tất", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                MessageBox.Show("Không thể ghi tệp. Hãy đóng tệp đang mở trong Excel, kiểm tra dung lượng hoặc chọn thư mục khác.",
                    "Xuất CSV thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    internal sealed class RevenueBarChart : Chart
    {
        public RevenueBarChart()
        {
            var area = new ChartArea("Revenue");
            area.AxisX.LabelStyle.Format = "dd/MM";
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisY.Title = "VNĐ";
            area.AxisY.Minimum = 0;
            ChartAreas.Add(area);
            Titles.Add("Doanh thu theo ngày");
            Series.Add(new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.DateTime,
                YValueType = ChartValueType.Double,
                Color = Color.SteelBlue,
                ToolTip = "#VALX{dd/MM/yyyy}: #VALY{N0} đ"
            });
        }

        public void SetData(IReadOnlyList<RevenuePointViewDto> data)
        {
            var series = Series[0];
            series.Points.Clear();
            foreach (var point in data)
                series.Points.AddXY(point.Date, (double)point.Revenue);
            Titles[0].Text = data.Count == 0 || data.All(p => p.Revenue == 0)
                ? "Chưa có giao dịch trong khoảng đã chọn" : "Doanh thu theo ngày";
            ChartAreas[0].RecalculateAxesScale();
        }
    }
}
