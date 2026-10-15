namespace MiniSupermarket.WinForms
{
    partial class FormQuickReport
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            pnlFilter = new Panel();
            lblFilter = new Label();
            dtpReportDate = new DateTimePicker();
            btnRunReport = new Button();
            pnlCards = new Panel();
            pnlCardOrders = new Panel();
            lblTotalOrders = new Label();
            lblOrdersTitle = new Label();
            pnlCardRevenue = new Panel();
            lblTotalRevenue = new Label();
            lblRevenueTitle = new Label();
            pnlCardBestSeller = new Panel();
            lblBestSeller = new Label();
            lblBestSellerTitle = new Label();
            pnlChartOrDetail = new Panel();
            grpDetail = new GroupBox();
            lblReportNote = new Label();

            pnlFilter.SuspendLayout();
            pnlCards.SuspendLayout();
            pnlCardOrders.SuspendLayout();
            pnlCardRevenue.SuspendLayout();
            pnlCardBestSeller.SuspendLayout();
            pnlChartOrDetail.SuspendLayout();
            grpDetail.SuspendLayout();
            SuspendLayout();

            // 
            // pnlFilter (Thanh bộ lọc ngày xem báo cáo)
            // 
            pnlFilter.BackColor = Color.White;
            pnlFilter.BorderStyle = BorderStyle.FixedSingle;
            pnlFilter.Controls.Add(btnRunReport);
            pnlFilter.Controls.Add(dtpReportDate);
            pnlFilter.Controls.Add(lblFilter);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(15, 15);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Size = new Size(1020, 65);
            pnlFilter.TabIndex = 0;

            // 
            // lblFilter
            // 
            lblFilter.AutoSize = true;
            lblFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFilter.Location = new Point(18, 20);
            lblFilter.Name = "lblFilter";
            lblFilter.Size = new Size(130, 23);
            lblFilter.TabIndex = 0;
            lblFilter.Text = "📅 Chọn ngày:";

            // 
            // dtpReportDate
            // 
            dtpReportDate.Font = new Font("Segoe UI", 10F);
            dtpReportDate.Format = DateTimePickerFormat.Short;
            dtpReportDate.Location = new Point(155, 17);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new Size(180, 30);
            dtpReportDate.TabIndex = 1;

            // 
            // btnRunReport
            // 
            btnRunReport.BackColor = Color.FromArgb(41, 100, 180);
            btnRunReport.FlatAppearance.BorderSize = 0;
            btnRunReport.FlatStyle = FlatStyle.Flat;
            btnRunReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRunReport.ForeColor = Color.White;
            btnRunReport.Location = new Point(355, 14);
            btnRunReport.Name = "btnRunReport";
            btnRunReport.Size = new Size(160, 35);
            btnRunReport.TabIndex = 2;
            btnRunReport.Text = "📊 Xem Báo Cáo";
            btnRunReport.UseVisualStyleBackColor = false;
            btnRunReport.Click += btnRunReport_Click;

            // 
            // pnlCards (Chứa 3 Card thống kê nhanh KPI)
            // 
            pnlCards.Controls.Add(pnlCardBestSeller);
            pnlCards.Controls.Add(pnlCardRevenue);
            pnlCards.Controls.Add(pnlCardOrders);
            pnlCards.Dock = DockStyle.Top;
            pnlCards.Location = new Point(15, 80);
            pnlCards.Name = "pnlCards";
            pnlCards.Padding = new Padding(0, 15, 0, 15);
            pnlCards.Size = new Size(1020, 175);
            pnlCards.TabIndex = 1;

            // 
            // pnlCardOrders
            // 
            pnlCardOrders.BackColor = Color.FromArgb(41, 128, 185);
            pnlCardOrders.Controls.Add(lblTotalOrders);
            pnlCardOrders.Controls.Add(lblOrdersTitle);
            pnlCardOrders.Location = new Point(0, 15);
            pnlCardOrders.Name = "pnlCardOrders";
            pnlCardOrders.Size = new Size(320, 140);
            pnlCardOrders.TabIndex = 0;

            // 
            // lblOrdersTitle
            // 
            lblOrdersTitle.AutoSize = true;
            lblOrdersTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblOrdersTitle.ForeColor = Color.White;
            lblOrdersTitle.Location = new Point(18, 18);
            lblOrdersTitle.Name = "lblOrdersTitle";
            lblOrdersTitle.Size = new Size(207, 25);
            lblOrdersTitle.TabIndex = 0;
            lblOrdersTitle.Text = "🧾 TỔNG SỐ HÓA ĐƠN";

            // 
            // lblTotalOrders
            // 
            lblTotalOrders.AutoSize = true;
            lblTotalOrders.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblTotalOrders.ForeColor = Color.White;
            lblTotalOrders.Location = new Point(18, 55);
            lblTotalOrders.Name = "lblTotalOrders";
            lblTotalOrders.Size = new Size(76, 60);
            lblTotalOrders.TabIndex = 1;
            lblTotalOrders.Text = "38";

            // 
            // pnlCardRevenue
            // 
            pnlCardRevenue.BackColor = Color.FromArgb(39, 174, 96);
            pnlCardRevenue.Controls.Add(lblTotalRevenue);
            pnlCardRevenue.Controls.Add(lblRevenueTitle);
            pnlCardRevenue.Location = new Point(345, 15);
            pnlCardRevenue.Name = "pnlCardRevenue";
            pnlCardRevenue.Size = new Size(330, 140);
            pnlCardRevenue.TabIndex = 1;

            // 
            // lblRevenueTitle
            // 
            lblRevenueTitle.AutoSize = true;
            lblRevenueTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblRevenueTitle.ForeColor = Color.White;
            lblRevenueTitle.Location = new Point(18, 18);
            lblRevenueTitle.Name = "lblRevenueTitle";
            lblRevenueTitle.Size = new Size(187, 25);
            lblRevenueTitle.TabIndex = 0;
            lblRevenueTitle.Text = "💰 TỔNG DOANH THU";

            // 
            // lblTotalRevenue
            // 
            lblTotalRevenue.AutoSize = true;
            lblTotalRevenue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalRevenue.ForeColor = Color.White;
            lblTotalRevenue.Location = new Point(18, 60);
            lblTotalRevenue.Name = "lblTotalRevenue";
            lblTotalRevenue.Size = new Size(245, 50);
            lblTotalRevenue.TabIndex = 1;
            lblTotalRevenue.Text = "4.850.000 đ";

            // 
            // pnlCardBestSeller
            // 
            pnlCardBestSeller.BackColor = Color.FromArgb(211, 84, 0);
            pnlCardBestSeller.Controls.Add(lblBestSeller);
            pnlCardBestSeller.Controls.Add(lblBestSellerTitle);
            pnlCardBestSeller.Location = new Point(700, 15);
            pnlCardBestSeller.Name = "pnlCardBestSeller";
            pnlCardBestSeller.Size = new Size(320, 140);
            pnlCardBestSeller.TabIndex = 2;

            // 
            // lblBestSellerTitle
            // 
            lblBestSellerTitle.AutoSize = true;
            lblBestSellerTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBestSellerTitle.ForeColor = Color.White;
            lblBestSellerTitle.Location = new Point(18, 18);
            lblBestSellerTitle.Name = "lblBestSellerTitle";
            lblBestSellerTitle.Size = new Size(256, 25);
            lblBestSellerTitle.TabIndex = 0;
            lblBestSellerTitle.Text = "🔥 MẶT HÀNG BÁN CHẠY";

            // 
            // lblBestSeller
            // 
            lblBestSeller.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblBestSeller.ForeColor = Color.White;
            lblBestSeller.Location = new Point(18, 55);
            lblBestSeller.Name = "lblBestSeller";
            lblBestSeller.Size = new Size(285, 70);
            lblBestSeller.TabIndex = 1;
            lblBestSeller.Text = "Nước khoáng Lavie 500ml";

            // 
            // pnlChartOrDetail
            // 
            pnlChartOrDetail.Controls.Add(grpDetail);
            pnlChartOrDetail.Dock = DockStyle.Fill;
            pnlChartOrDetail.Location = new Point(15, 255);
            pnlChartOrDetail.Name = "pnlChartOrDetail";
            pnlChartOrDetail.Size = new Size(1020, 390);
            pnlChartOrDetail.TabIndex = 2;

            // 
            // grpDetail
            // 
            grpDetail.BackColor = Color.White;
            grpDetail.Controls.Add(lblReportNote);
            grpDetail.Dock = DockStyle.Fill;
            grpDetail.Font = new Font("Segoe UI", 10F);
            grpDetail.Location = new Point(0, 0);
            grpDetail.Name = "grpDetail";
            grpDetail.Padding = new Padding(20);
            grpDetail.Size = new Size(1020, 390);
            grpDetail.TabIndex = 0;
            grpDetail.TabStop = false;
            grpDetail.Text = "Ghi chú & Tóm tắt hiệu suất ca";

            // 
            // lblReportNote
            // 
            lblReportNote.Dock = DockStyle.Fill;
            lblReportNote.Font = new Font("Segoe UI", 10.5F);
            lblReportNote.ForeColor = Color.FromArgb(60, 70, 85);
            lblReportNote.Location = new Point(20, 43);
            lblReportNote.Name = "lblReportNote";
            lblReportNote.Size = new Size(980, 327);
            lblReportNote.TabIndex = 0;
            lblReportNote.Text = "• Dữ liệu báo cáo được tổng hợp trực tiếp từ hệ thống giao dịch quầy POS Bách Hóa Thiên Ân.\r\n• Chức năng này được bảo vệ phân quyền 2 lớp (chỉ Quản trị viên mới được phép truy xuất).\r\n• Doanh thu ca phản ánh tổng giá trị thanh toán thực tế đã trừ khuyến mãi và tích điểm.";

            // 
            // FormQuickReport
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 660);
            Controls.Add(pnlChartOrDetail);
            Controls.Add(pnlCards);
            Controls.Add(pnlFilter);
            Name = "FormQuickReport";
            Padding = new Padding(15);
            Text = "Báo cáo Doanh thu Nhanh";
            Load += FormQuickReport_Load;

            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            pnlCards.ResumeLayout(false);
            pnlCardOrders.ResumeLayout(false);
            pnlCardOrders.PerformLayout();
            pnlCardRevenue.ResumeLayout(false);
            pnlCardRevenue.PerformLayout();
            pnlCardBestSeller.ResumeLayout(false);
            pnlCardBestSeller.PerformLayout();
            pnlChartOrDetail.ResumeLayout(false);
            grpDetail.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFilter;
        private Label lblFilter;
        private DateTimePicker dtpReportDate;
        private Button btnRunReport;
        private Panel pnlCards;
        private Panel pnlCardOrders;
        private Label lblTotalOrders;
        private Label lblOrdersTitle;
        private Panel pnlCardRevenue;
        private Label lblTotalRevenue;
        private Label lblRevenueTitle;
        private Panel pnlCardBestSeller;
        private Label lblBestSeller;
        private Label lblBestSellerTitle;
        private Panel pnlChartOrDetail;
        private GroupBox grpDetail;
        private Label lblReportNote;
    }
}
