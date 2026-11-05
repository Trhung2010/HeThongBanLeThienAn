namespace MiniSupermarket.WinForms
{
    partial class FormCustomerManagement
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
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();
            grpSearch = new GroupBox();

            tabControlCustomer = new TabControl();
            tabCustomerList = new TabPage();
            dgvCustomers = new DataGridView();
            btnPrev = new Button();
            lblPaging = new Label();
            btnNext = new Button();

            tabOrderHistory = new TabPage();
            lblOrderHistoryInfo = new Label();
            btnBackToList = new Button();
            dgvOrderHistory = new DataGridView();

            grpCustomerInfo = new GroupBox();
            lblCustomerId = new Label();
            txtCustomerId = new TextBox();
            lblCustomerName = new Label();
            txtCustomerName = new TextBox();
            lblPhoneNumber = new Label();
            txtPhoneNumber = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            lblRewardPoints = new Label();
            txtRewardPoints = new TextBox();
            lblMembershipRank = new Label();
            cboMembershipRank = new ComboBox();
            btnViewHistory = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderHistory).BeginInit();
            grpSearch.SuspendLayout();
            tabControlCustomer.SuspendLayout();
            tabCustomerList.SuspendLayout();
            tabOrderHistory.SuspendLayout();
            grpCustomerInfo.SuspendLayout();
            SuspendLayout();

            //
            // grpSearch
            //
            grpSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Location = new Point(20, 25);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(910, 75);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm Khách hàng";

            //
            // txtKeyword
            //
            txtKeyword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.Location = new Point(17, 28);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên, SĐT hoặc Hạng thẻ...";
            txtKeyword.Size = new Size(620, 27);
            txtKeyword.TabIndex = 0;

            //
            // btnSearch
            //
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(660, 26);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 31);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            //
            // btnLoad
            //
            btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoad.Location = new Point(780, 26);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(110, 31);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            //
            // tabControlCustomer
            //
            tabControlCustomer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlCustomer.Controls.Add(tabCustomerList);
            tabControlCustomer.Controls.Add(tabOrderHistory);
            tabControlCustomer.Font = new Font("Segoe UI", 9.5F);
            tabControlCustomer.Location = new Point(20, 110);
            tabControlCustomer.Name = "tabControlCustomer";
            tabControlCustomer.SelectedIndex = 0;
            tabControlCustomer.Size = new Size(605, 510);
            tabControlCustomer.TabIndex = 1;

            //
            // tabCustomerList
            //
            tabCustomerList.Controls.Add(dgvCustomers);
            tabCustomerList.Controls.Add(btnPrev);
            tabCustomerList.Controls.Add(lblPaging);
            tabCustomerList.Controls.Add(btnNext);
            tabCustomerList.Location = new Point(4, 30);
            tabCustomerList.Name = "tabCustomerList";
            tabCustomerList.Padding = new Padding(3);
            tabCustomerList.Size = new Size(597, 476);
            tabCustomerList.TabIndex = 0;
            tabCustomerList.Text = "📋 Danh sách Khách hàng";
            tabCustomerList.UseVisualStyleBackColor = true;

            //
            // dgvCustomers
            //
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.BorderStyle = BorderStyle.Fixed3D;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(6, 10);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 40;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(585, 415);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;
            dgvCustomers.CellDoubleClick += dgvCustomers_CellDoubleClick;

            //
            // btnPrev
            //
            btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPrev.Location = new Point(6, 435);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(110, 32);
            btnPrev.TabIndex = 1;
            btnPrev.Text = "<< Trang trước";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;

            //
            // lblPaging
            //
            lblPaging.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblPaging.Location = new Point(125, 439);
            lblPaging.Name = "lblPaging";
            lblPaging.Size = new Size(345, 24);
            lblPaging.TabIndex = 2;
            lblPaging.Text = "Trang 0 / 0 (Tổng: 0 KH)";
            lblPaging.TextAlign = ContentAlignment.MiddleCenter;

            //
            // btnNext
            //
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.Location = new Point(480, 435);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(110, 32);
            btnNext.TabIndex = 3;
            btnNext.Text = "Trang sau >>";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;

            //
            // tabOrderHistory
            //
            tabOrderHistory.Controls.Add(lblOrderHistoryInfo);
            tabOrderHistory.Controls.Add(btnBackToList);
            tabOrderHistory.Controls.Add(dgvOrderHistory);
            tabOrderHistory.Location = new Point(4, 30);
            tabOrderHistory.Name = "tabOrderHistory";
            tabOrderHistory.Padding = new Padding(3);
            tabOrderHistory.Size = new Size(597, 476);
            tabOrderHistory.TabIndex = 1;
            tabOrderHistory.Text = "📜 Lịch sử Đơn hàng";
            tabOrderHistory.UseVisualStyleBackColor = true;

            //
            // lblOrderHistoryInfo
            //
            lblOrderHistoryInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblOrderHistoryInfo.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblOrderHistoryInfo.ForeColor = Color.DarkBlue;
            lblOrderHistoryInfo.Location = new Point(10, 10);
            lblOrderHistoryInfo.Name = "lblOrderHistoryInfo";
            lblOrderHistoryInfo.Size = new Size(410, 28);
            lblOrderHistoryInfo.TabIndex = 0;
            lblOrderHistoryInfo.Text = "← Chọn một khách hàng để xem lịch sử đơn hàng";
            lblOrderHistoryInfo.TextAlign = ContentAlignment.MiddleLeft;

            //
            // btnBackToList
            //
            btnBackToList.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBackToList.Location = new Point(430, 8);
            btnBackToList.Name = "btnBackToList";
            btnBackToList.Size = new Size(160, 30);
            btnBackToList.TabIndex = 1;
            btnBackToList.Text = "← Xem danh sách KH";
            btnBackToList.UseVisualStyleBackColor = true;
            btnBackToList.Click += btnBackToList_Click;

            //
            // dgvOrderHistory
            //
            dgvOrderHistory.AllowUserToAddRows = false;
            dgvOrderHistory.AllowUserToDeleteRows = false;
            dgvOrderHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvOrderHistory.BackgroundColor = Color.White;
            dgvOrderHistory.BorderStyle = BorderStyle.Fixed3D;
            dgvOrderHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderHistory.Location = new Point(6, 45);
            dgvOrderHistory.Name = "dgvOrderHistory";
            dgvOrderHistory.ReadOnly = true;
            dgvOrderHistory.RowHeadersWidth = 40;
            dgvOrderHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderHistory.Size = new Size(585, 420);
            dgvOrderHistory.TabIndex = 2;
            dgvOrderHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //
            // grpCustomerInfo
            //
            grpCustomerInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpCustomerInfo.Controls.Add(lblCustomerId);
            grpCustomerInfo.Controls.Add(txtCustomerId);
            grpCustomerInfo.Controls.Add(lblCustomerName);
            grpCustomerInfo.Controls.Add(txtCustomerName);
            grpCustomerInfo.Controls.Add(lblPhoneNumber);
            grpCustomerInfo.Controls.Add(txtPhoneNumber);
            grpCustomerInfo.Controls.Add(lblAddress);
            grpCustomerInfo.Controls.Add(txtAddress);
            grpCustomerInfo.Controls.Add(lblRewardPoints);
            grpCustomerInfo.Controls.Add(txtRewardPoints);
            grpCustomerInfo.Controls.Add(lblMembershipRank);
            grpCustomerInfo.Controls.Add(cboMembershipRank);
            grpCustomerInfo.Controls.Add(btnViewHistory);
            grpCustomerInfo.Controls.Add(btnAdd);
            grpCustomerInfo.Controls.Add(btnUpdate);
            grpCustomerInfo.Controls.Add(btnDelete);
            grpCustomerInfo.Location = new Point(635, 110);
            grpCustomerInfo.Name = "grpCustomerInfo";
            grpCustomerInfo.Size = new Size(295, 510);
            grpCustomerInfo.TabIndex = 2;
            grpCustomerInfo.TabStop = false;
            grpCustomerInfo.Text = "Thông tin Khách hàng";

            //
            // lblCustomerId
            //
            lblCustomerId.AutoSize = true;
            lblCustomerId.Location = new Point(15, 23);
            lblCustomerId.Text = "Mã KH:";

            txtCustomerId.Location = new Point(15, 43);
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(260, 27);

            //
            // lblCustomerName
            //
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(15, 75);
            lblCustomerName.Text = "Tên KH (*):";

            txtCustomerName.Location = new Point(15, 95);
            txtCustomerName.Size = new Size(260, 27);

            //
            // lblPhoneNumber
            //
            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Location = new Point(15, 127);
            lblPhoneNumber.Text = "Số điện thoại (*):";

            txtPhoneNumber.Location = new Point(15, 147);
            txtPhoneNumber.Size = new Size(260, 27);

            //
            // lblAddress
            //
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(15, 179);
            lblAddress.Text = "Địa chỉ:";

            txtAddress.Location = new Point(15, 199);
            txtAddress.Size = new Size(260, 27);

            //
            // lblRewardPoints
            //
            lblRewardPoints.AutoSize = true;
            lblRewardPoints.Location = new Point(15, 231);
            lblRewardPoints.Text = "Điểm tích lũy:";

            txtRewardPoints.Location = new Point(15, 251);
            txtRewardPoints.Size = new Size(260, 27);

            //
            // lblMembershipRank
            //
            lblMembershipRank.AutoSize = true;
            lblMembershipRank.Location = new Point(15, 283);
            lblMembershipRank.Text = "Hạng thẻ:";

            cboMembershipRank.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMembershipRank.Items.AddRange(new object[] { "Chuẩn", "Bạc", "Vàng", "Kim Cương" });
            cboMembershipRank.Location = new Point(15, 303);
            cboMembershipRank.SelectedIndex = 0;
            cboMembershipRank.Size = new Size(260, 28);

            //
            // btnViewHistory
            //
            btnViewHistory.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnViewHistory.BackColor = Color.FromArgb(41, 128, 185);
            btnViewHistory.ForeColor = Color.White;
            btnViewHistory.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnViewHistory.FlatStyle = FlatStyle.Flat;
            btnViewHistory.FlatAppearance.BorderSize = 0;
            btnViewHistory.Location = new Point(15, 395);
            btnViewHistory.Name = "btnViewHistory";
            btnViewHistory.Size = new Size(260, 40);
            btnViewHistory.TabIndex = 12;
            btnViewHistory.Text = "📜 Xem lịch sử đơn hàng";
            btnViewHistory.UseVisualStyleBackColor = false;
            btnViewHistory.Click += btnViewHistory_Click;

            //
            // btnAdd
            //
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.Location = new Point(15, 450);
            btnAdd.Size = new Size(80, 40);
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            //
            // btnUpdate
            //
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdate.Location = new Point(105, 450);
            btnUpdate.Size = new Size(80, 40);
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            //
            // btnDelete
            //
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(195, 450);
            btnDelete.Size = new Size(80, 40);
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            //
            // FormCustomerManagement
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(950, 640);
            Controls.Add(grpSearch);
            Controls.Add(tabControlCustomer);
            Controls.Add(grpCustomerInfo);
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Khách hàng - Thiên Ân";
            Load += FormCustomerManagement_Load;

            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderHistory).EndInit();
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            tabControlCustomer.ResumeLayout(false);
            tabCustomerList.ResumeLayout(false);
            tabOrderHistory.ResumeLayout(false);
            grpCustomerInfo.ResumeLayout(false);
            grpCustomerInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;
        private GroupBox grpSearch;

        private TabControl tabControlCustomer;
        private TabPage tabCustomerList;
        private DataGridView dgvCustomers;
        private Button btnPrev;
        private Label lblPaging;
        private Button btnNext;

        private TabPage tabOrderHistory;
        private Label lblOrderHistoryInfo;
        private Button btnBackToList;
        private DataGridView dgvOrderHistory;

        private GroupBox grpCustomerInfo;
        private Label lblCustomerId;
        private TextBox txtCustomerId;
        private Label lblCustomerName;
        private TextBox txtCustomerName;
        private Label lblPhoneNumber;
        private TextBox txtPhoneNumber;
        private Label lblAddress;
        private TextBox txtAddress;
        private Label lblRewardPoints;
        private TextBox txtRewardPoints;
        private Label lblMembershipRank;
        private ComboBox cboMembershipRank;
        private Button btnViewHistory;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}
