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
            dgvCustomers = new DataGridView();
            txtKeyword = new TextBox();
            txtCustomerId = new TextBox();
            txtCustomerName = new TextBox();
            txtPhoneNumber = new TextBox();
            txtAddress = new TextBox();
            txtRewardPoints = new TextBox();
            cboMembershipRank = new ComboBox();
            btnSearch = new Button();
            btnLoad = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            grpSearch = new GroupBox();
            grpCustomerList = new GroupBox();
            grpCustomerInfo = new GroupBox();
            lblCustomerId = new Label();
            lblCustomerName = new Label();
            lblPhoneNumber = new Label();
            lblAddress = new Label();
            lblRewardPoints = new Label();
            lblMembershipRank = new Label();

            ((System.ComponentModel.ISupportInitialize)dgvCustomers).BeginInit();
            grpSearch.SuspendLayout();
            grpCustomerList.SuspendLayout();
            grpCustomerInfo.SuspendLayout();
            SuspendLayout();

            //
            // dgvCustomers
            //
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.AllowUserToDeleteRows = false;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.BorderStyle = BorderStyle.Fixed3D;
            dgvCustomers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCustomers.Location = new Point(11, 30);
            dgvCustomers.Margin = new Padding(3, 4, 3, 4);
            dgvCustomers.Name = "dgvCustomers";
            dgvCustomers.ReadOnly = true;
            dgvCustomers.RowHeadersWidth = 40;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.Size = new Size(580, 410);
            dgvCustomers.TabIndex = 0;
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            //
            // txtKeyword
            //
            txtKeyword.Location = new Point(17, 28);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên, SĐỐT hoặc Hạng thẻ...";
            txtKeyword.Size = new Size(520, 27);
            txtKeyword.TabIndex = 0;

            //
            // btnSearch
            //
            btnSearch.Location = new Point(550, 26);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 31);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            //
            // btnLoad
            //
            btnLoad.Location = new Point(670, 26);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(110, 31);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            //
            // grpSearch
            //
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Location = new Point(20, 38);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(910, 75);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm Khách hàng";

            //
            // grpCustomerList
            //
            grpCustomerList.Controls.Add(dgvCustomers);
            grpCustomerList.Location = new Point(20, 125);
            grpCustomerList.Name = "grpCustomerList";
            grpCustomerList.Size = new Size(605, 485);
            grpCustomerList.TabIndex = 1;
            grpCustomerList.TabStop = false;
            grpCustomerList.Text = "Danh sách Khách hàng";

            //
            // grpCustomerInfo
            //
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
            grpCustomerInfo.Controls.Add(btnAdd);
            grpCustomerInfo.Controls.Add(btnUpdate);
            grpCustomerInfo.Controls.Add(btnDelete);
            grpCustomerInfo.Location = new Point(635, 125);
            grpCustomerInfo.Name = "grpCustomerInfo";
            grpCustomerInfo.Size = new Size(295, 455);
            grpCustomerInfo.TabIndex = 2;
            grpCustomerInfo.TabStop = false;
            grpCustomerInfo.Text = "Thông tin Khách hàng";

            // Labels and TextBoxes setup
            lblCustomerId.AutoSize = true;
            lblCustomerId.Location = new Point(15, 25);
            lblCustomerId.Text = "Mã KH:";

            txtCustomerId.Location = new Point(15, 45);
            txtCustomerId.ReadOnly = true;
            txtCustomerId.Size = new Size(260, 27);

            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(15, 80);
            lblCustomerName.Text = "Tên KH (*):";

            txtCustomerName.Location = new Point(15, 100);
            txtCustomerName.Size = new Size(260, 27);

            lblPhoneNumber.AutoSize = true;
            lblPhoneNumber.Location = new Point(15, 135);
            lblPhoneNumber.Text = "Số điện thoại (*):";

            txtPhoneNumber.Location = new Point(15, 155);
            txtPhoneNumber.Size = new Size(260, 27);

            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(15, 190);
            lblAddress.Text = "Địa chỉ:";

            txtAddress.Location = new Point(15, 210);
            txtAddress.Size = new Size(260, 27);

            lblRewardPoints.AutoSize = true;
            lblRewardPoints.Location = new Point(15, 245);
            lblRewardPoints.Text = "Điểm tích lũy:";

            txtRewardPoints.Location = new Point(15, 265);
            txtRewardPoints.Size = new Size(260, 27);

            lblMembershipRank.AutoSize = true;
            lblMembershipRank.Location = new Point(15, 300);
            lblMembershipRank.Text = "Hạng thẻ:";

            cboMembershipRank.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMembershipRank.Items.AddRange(new object[] { "Chuẩn", "Bạc", "Vàng", "Kim Cương" });
            cboMembershipRank.Location = new Point(15, 320);
            cboMembershipRank.SelectedIndex = 0;
            cboMembershipRank.Size = new Size(260, 28);

            // Buttons
            btnAdd.Location = new Point(15, 385);
            btnAdd.Size = new Size(80, 40);
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Location = new Point(105, 385);
            btnUpdate.Size = new Size(80, 40);
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Location = new Point(195, 385);
            btnDelete.Size = new Size(80, 40);
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // Form
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 640);
            Controls.Add(grpSearch);
            Controls.Add(grpCustomerList);
            Controls.Add(grpCustomerInfo);
            Name = "FormCustomerManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Khách hàng - Thiên Ân";
            Load += FormCustomerManagement_Load;

            ((System.ComponentModel.ISupportInitialize)dgvCustomers).EndInit();
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpCustomerList.ResumeLayout(false);
            grpCustomerInfo.ResumeLayout(false);
            grpCustomerInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvCustomers;
        private TextBox txtKeyword;
        private TextBox txtCustomerId;
        private TextBox txtCustomerName;
        private TextBox txtPhoneNumber;
        private TextBox txtAddress;
        private TextBox txtRewardPoints;
        private ComboBox cboMembershipRank;
        private Button btnSearch;
        private Button btnLoad;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private GroupBox grpSearch;
        private GroupBox grpCustomerList;
        private GroupBox grpCustomerInfo;
        private Label lblCustomerId;
        private Label lblCustomerName;
        private Label lblPhoneNumber;
        private Label lblAddress;
        private Label lblRewardPoints;
        private Label lblMembershipRank;
    }
}
