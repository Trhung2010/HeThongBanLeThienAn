namespace MiniSupermarket.WinForms
{
    partial class FormMainShell
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
            panelSidebar = new Panel();
            pnlLogo = new Panel();
            lblLogo = new Label();
            btnPOS = new Button();
            btnCategory = new Button();
            btnProduct = new Button();
            btnCustomer = new Button();
            btnReports = new Button();
            btnUserManage = new Button();
            panelUserFooter = new Panel();
            btnLogout = new Button();
            panelTopHeader = new Panel();
            lblTitle = new Label();
            lblUserInfo = new Label();
            panelMainContent = new Panel();

            panelSidebar.SuspendLayout();
            pnlLogo.SuspendLayout();
            panelUserFooter.SuspendLayout();
            panelTopHeader.SuspendLayout();
            SuspendLayout();

            // 
            // panelSidebar (Thanh điều hướng bên trái, Width = 230, BackColor = RGB(24, 30, 48))
            // 
            panelSidebar.BackColor = Color.FromArgb(24, 30, 48);
            panelSidebar.Controls.Add(btnUserManage);
            panelSidebar.Controls.Add(btnReports);
            panelSidebar.Controls.Add(btnCustomer);
            panelSidebar.Controls.Add(btnProduct);
            panelSidebar.Controls.Add(btnCategory);
            panelSidebar.Controls.Add(btnPOS);
            panelSidebar.Controls.Add(pnlLogo);
            panelSidebar.Controls.Add(panelUserFooter);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(230, 720);
            panelSidebar.TabIndex = 0;

            // 
            // pnlLogo
            // 
            pnlLogo.Controls.Add(lblLogo);
            pnlLogo.Dock = DockStyle.Top;
            pnlLogo.Location = new Point(0, 0);
            pnlLogo.Name = "pnlLogo";
            pnlLogo.Size = new Size(230, 80);
            pnlLogo.TabIndex = 0;

            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(22, 24);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(183, 32);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "🛒 MiniMart POS";

            // 
            // btnPOS (🛒 Bán hàng - POS)
            // 
            btnPOS.Dock = DockStyle.Top;
            btnPOS.FlatAppearance.BorderSize = 0;
            btnPOS.FlatStyle = FlatStyle.Flat;
            btnPOS.Font = new Font("Segoe UI", 10.5F);
            btnPOS.ForeColor = Color.White;
            btnPOS.Location = new Point(0, 80);
            btnPOS.Name = "btnPOS";
            btnPOS.Padding = new Padding(20, 0, 0, 0);
            btnPOS.Size = new Size(230, 50);
            btnPOS.TabIndex = 1;
            btnPOS.Text = "🛒 Bán hàng (POS)";
            btnPOS.TextAlign = ContentAlignment.MiddleLeft;
            btnPOS.UseVisualStyleBackColor = true;
            btnPOS.Click += btnPOS_Click;

            // 
            // btnCategory (📁 Quản lý Danh mục)
            // 
            btnCategory.Dock = DockStyle.Top;
            btnCategory.FlatAppearance.BorderSize = 0;
            btnCategory.FlatStyle = FlatStyle.Flat;
            btnCategory.Font = new Font("Segoe UI", 10.5F);
            btnCategory.ForeColor = Color.White;
            btnCategory.Location = new Point(0, 130);
            btnCategory.Name = "btnCategory";
            btnCategory.Padding = new Padding(20, 0, 0, 0);
            btnCategory.Size = new Size(230, 50);
            btnCategory.TabIndex = 2;
            btnCategory.Text = "📁 Quản lý Danh mục";
            btnCategory.TextAlign = ContentAlignment.MiddleLeft;
            btnCategory.UseVisualStyleBackColor = true;
            btnCategory.Click += btnCategory_Click;

            // 
            // btnProduct (📦 Quản lý Sản phẩm)
            // 
            btnProduct.Dock = DockStyle.Top;
            btnProduct.FlatAppearance.BorderSize = 0;
            btnProduct.FlatStyle = FlatStyle.Flat;
            btnProduct.Font = new Font("Segoe UI", 10.5F);
            btnProduct.ForeColor = Color.White;
            btnProduct.Location = new Point(0, 180);
            btnProduct.Name = "btnProduct";
            btnProduct.Padding = new Padding(20, 0, 0, 0);
            btnProduct.Size = new Size(230, 50);
            btnProduct.TabIndex = 3;
            btnProduct.Text = "📦 Quản lý Sản phẩm";
            btnProduct.TextAlign = ContentAlignment.MiddleLeft;
            btnProduct.UseVisualStyleBackColor = true;
            btnProduct.Click += btnProduct_Click;

            // 
            // btnCustomer (👥 Quản lý Khách hàng)
            // 
            btnCustomer.Dock = DockStyle.Top;
            btnCustomer.FlatAppearance.BorderSize = 0;
            btnCustomer.FlatStyle = FlatStyle.Flat;
            btnCustomer.Font = new Font("Segoe UI", 10.5F);
            btnCustomer.ForeColor = Color.White;
            btnCustomer.Location = new Point(0, 230);
            btnCustomer.Name = "btnCustomer";
            btnCustomer.Padding = new Padding(20, 0, 0, 0);
            btnCustomer.Size = new Size(230, 50);
            btnCustomer.TabIndex = 4;
            btnCustomer.Text = "👥 Quản lý Khách hàng";
            btnCustomer.TextAlign = ContentAlignment.MiddleLeft;
            btnCustomer.UseVisualStyleBackColor = true;
            btnCustomer.Click += btnCustomer_Click;

            // 
            // btnReports (📊 Báo cáo Doanh thu)
            // 
            btnReports.Dock = DockStyle.Top;
            btnReports.FlatAppearance.BorderSize = 0;
            btnReports.FlatStyle = FlatStyle.Flat;
            btnReports.Font = new Font("Segoe UI", 10.5F);
            btnReports.ForeColor = Color.White;
            btnReports.Location = new Point(0, 280);
            btnReports.Name = "btnReports";
            btnReports.Padding = new Padding(20, 0, 0, 0);
            btnReports.Size = new Size(230, 50);
            btnReports.TabIndex = 5;
            btnReports.Text = "📊 Báo cáo Doanh thu";
            btnReports.TextAlign = ContentAlignment.MiddleLeft;
            btnReports.UseVisualStyleBackColor = true;
            btnReports.Click += btnReports_Click;

            // 
            // btnUserManage (🛡 Quản trị Tài khoản)
            // 
            btnUserManage.Dock = DockStyle.Top;
            btnUserManage.FlatAppearance.BorderSize = 0;
            btnUserManage.FlatStyle = FlatStyle.Flat;
            btnUserManage.Font = new Font("Segoe UI", 10.5F);
            btnUserManage.ForeColor = Color.White;
            btnUserManage.Location = new Point(0, 330);
            btnUserManage.Name = "btnUserManage";
            btnUserManage.Padding = new Padding(20, 0, 0, 0);
            btnUserManage.Size = new Size(230, 50);
            btnUserManage.TabIndex = 6;
            btnUserManage.Text = "🛡️ Quản trị Tài khoản";
            btnUserManage.TextAlign = ContentAlignment.MiddleLeft;
            btnUserManage.UseVisualStyleBackColor = true;
            btnUserManage.Click += btnUserManage_Click;

            // 
            // panelUserFooter (Panel phụ ở đáy sidebar chứa nút Đăng xuất)
            // 
            panelUserFooter.Controls.Add(btnLogout);
            panelUserFooter.Dock = DockStyle.Bottom;
            panelUserFooter.Location = new Point(0, 655);
            panelUserFooter.Name = "panelUserFooter";
            panelUserFooter.Size = new Size(230, 65);
            panelUserFooter.TabIndex = 7;

            // 
            // btnLogout (🚪 Đăng xuất)
            // 
            btnLogout.BackColor = Color.FromArgb(190, 45, 45);
            btnLogout.Dock = DockStyle.Fill;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(0, 0);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(20, 0, 0, 0);
            btnLogout.Size = new Size(230, 65);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "🚪 Đăng xuất";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;

            // 
            // panelTopHeader (Thanh tiêu đề trên, Height = 60, BackColor = White)
            // 
            panelTopHeader.BackColor = Color.White;
            panelTopHeader.BorderStyle = BorderStyle.FixedSingle;
            panelTopHeader.Controls.Add(lblUserInfo);
            panelTopHeader.Controls.Add(lblTitle);
            panelTopHeader.Dock = DockStyle.Top;
            panelTopHeader.Location = new Point(230, 0);
            panelTopHeader.Name = "panelTopHeader";
            panelTopHeader.Size = new Size(1050, 60);
            panelTopHeader.TabIndex = 1;

            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.5F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(24, 30, 48);
            lblTitle.Location = new Point(18, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(292, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BÀN LÀM VIỆC HỆ THỐNG";

            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.Font = new Font("Segoe UI", 10F);
            lblUserInfo.ForeColor = Color.FromArgb(50, 60, 80);
            lblUserInfo.Location = new Point(560, 16);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(475, 28);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Nhân viên: ... | Vai trò: [...]";
            lblUserInfo.TextAlign = ContentAlignment.MiddleRight;

            // 
            // panelMainContent (Vùng chứa trung tâm nhúng động các Form con)
            // 
            panelMainContent.BackColor = Color.FromArgb(244, 245, 247);
            panelMainContent.Dock = DockStyle.Fill;
            panelMainContent.Location = new Point(230, 60);
            panelMainContent.Name = "panelMainContent";
            panelMainContent.Size = new Size(1050, 660);
            panelMainContent.TabIndex = 2;

            // 
            // FormMainShell
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(panelMainContent);
            Controls.Add(panelTopHeader);
            Controls.Add(panelSidebar);
            MinimumSize = new Size(1100, 650);
            Name = "FormMainShell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Quản lý Bán lẻ & Tồn kho Siêu thị Mini - Bách Hóa Thiên Ân";
            Load += FormMainShell_Load;

            panelSidebar.ResumeLayout(false);
            pnlLogo.ResumeLayout(false);
            pnlLogo.PerformLayout();
            panelUserFooter.ResumeLayout(false);
            panelTopHeader.ResumeLayout(false);
            panelTopHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar;
        private Panel pnlLogo;
        private Label lblLogo;
        private Button btnPOS;
        private Button btnCategory;
        private Button btnProduct;
        private Button btnCustomer;
        private Button btnReports;
        private Button btnUserManage;
        private Panel panelUserFooter;
        private Button btnLogout;
        private Panel panelTopHeader;
        private Label lblTitle;
        private Label lblUserInfo;
        private Panel panelMainContent;
    }
}
