namespace MiniSupermarket.WinForms
{
    partial class FormUserManagement
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
            pnlLeft = new Panel();
            grpList = new GroupBox();
            dgvUsers = new DataGridView();
            pnlPaging = new Panel();
            btnPrev = new Button();
            lblPaging = new Label();
            btnNext = new Button();
            pnlRight = new Panel();
            grpAdd = new GroupBox();
            btnReload = new Button();
            btnAddUser = new Button();
            cboRole = new ComboBox();
            lblRole = new Label();
            txtFullName = new TextBox();
            lblFullName = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();

            pnlLeft.SuspendLayout();
            grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            pnlRight.SuspendLayout();
            grpAdd.SuspendLayout();
            SuspendLayout();

            //
            // pnlLeft (Vùng hiển thị danh sách tài khoản)
            //
            pnlLeft.Controls.Add(grpList);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(12);
            pnlLeft.Size = new Size(690, 660);
            pnlLeft.TabIndex = 0;

            //
            // grpList
            //
            grpList.Controls.Add(dgvUsers);
            grpList.Controls.Add(pnlPaging);
            grpList.Dock = DockStyle.Fill;
            grpList.Font = new Font("Segoe UI", 9.5F);
            grpList.Location = new Point(12, 12);
            grpList.Name = "grpList";
            grpList.Padding = new Padding(8);
            grpList.Size = new Size(666, 636);
            grpList.TabIndex = 0;
            grpList.TabStop = false;
            grpList.Text = "Danh sách Nhân viên & Tài khoản";

                        //
            // pnlPaging
            //
            pnlPaging.Controls.Add(btnPrev);
            pnlPaging.Controls.Add(lblPaging);
            pnlPaging.Controls.Add(btnNext);
            pnlPaging.Dock = DockStyle.Bottom;
            pnlPaging.Height = 45;
            pnlPaging.Name = "pnlPaging";
            pnlPaging.TabIndex = 1;
            //
            // btnPrev
            //
            btnPrev.Location = new Point(40, 6);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(110, 32);
            btnPrev.TabIndex = 0;
            btnPrev.Text = "<< Trang trước";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            //
            // lblPaging
            //
            lblPaging.Location = new Point(160, 10);
            lblPaging.Name = "lblPaging";
            lblPaging.Size = new Size(300, 24);
            lblPaging.TabIndex = 1;
            lblPaging.Text = "Trang 0 / 0 (Tổng: 0 user)";
            lblPaging.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnNext
            //
            btnNext.Location = new Point(470, 6);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(110, 32);
            btnNext.TabIndex = 2;
            btnNext.Text = "Trang sau >>";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            //
            // dgvUsers
            //
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.Fixed3D;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.Location = new Point(8, 30);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersWidth = 40;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(650, 598);
            dgvUsers.TabIndex = 0;

            //
            // pnlRight (Vùng tạo tài khoản mới)
            //
            pnlRight.Controls.Add(grpAdd);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(690, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(12);
            pnlRight.Size = new Size(360, 660);
            pnlRight.TabIndex = 1;

            //
            // grpAdd
            //
            grpAdd.Controls.Add(btnReload);
            grpAdd.Controls.Add(btnAddUser);
            grpAdd.Controls.Add(cboRole);
            grpAdd.Controls.Add(lblRole);
            grpAdd.Controls.Add(txtFullName);
            grpAdd.Controls.Add(lblFullName);
            grpAdd.Controls.Add(txtPassword);
            grpAdd.Controls.Add(lblPassword);
            grpAdd.Controls.Add(txtUsername);
            grpAdd.Controls.Add(lblUsername);
            grpAdd.Dock = DockStyle.Fill;
            grpAdd.Font = new Font("Segoe UI", 9.5F);
            grpAdd.Location = new Point(12, 12);
            grpAdd.Name = "grpAdd";
            grpAdd.Padding = new Padding(15);
            grpAdd.Size = new Size(336, 636);
            grpAdd.TabIndex = 0;
            grpAdd.TabStop = false;
            grpAdd.Text = "Thêm tài khoản nhân viên";

            //
            // lblUsername
            //
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(15, 35);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(114, 21);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Tên đăng nhập:";

            //
            // txtUsername
            //
            txtUsername.Location = new Point(15, 60);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Ví dụ: cashier07, ware06...";
            txtUsername.Size = new Size(300, 29);
            txtUsername.TabIndex = 1;

            //
            // lblPassword
            //
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(15, 110);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(78, 21);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Mật khẩu:";

            //
            // txtPassword
            //
            txtPassword.Location = new Point(15, 135);
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Nhập mật khẩu...";
            txtPassword.Size = new Size(300, 29);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;

            //
            // lblFullName
            //
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(15, 185);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(79, 21);
            lblFullName.TabIndex = 4;
            lblFullName.Text = "Họ và tên:";

            //
            // txtFullName
            //
            txtFullName.Location = new Point(15, 210);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Ví dụ: Nguyễn Văn Thu Ngân...";
            txtFullName.Size = new Size(300, 29);
            txtFullName.TabIndex = 5;

            //
            // lblRole
            //
            lblRole.AutoSize = true;
            lblRole.Location = new Point(15, 260);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(130, 21);
            lblRole.TabIndex = 6;
            lblRole.Text = "Vai trò (Phân quyền):";

            //
            // cboRole
            //
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cboRole.FormattingEnabled = true;
            cboRole.Location = new Point(15, 285);
            cboRole.Name = "cboRole";
            cboRole.Size = new Size(300, 29);
            cboRole.TabIndex = 7;

            //
            // btnAddUser
            //
            btnAddUser.BackColor = Color.ForestGreen;
            btnAddUser.FlatAppearance.BorderSize = 0;
            btnAddUser.FlatStyle = FlatStyle.Flat;
            btnAddUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnAddUser.ForeColor = Color.White;
            btnAddUser.Location = new Point(15, 350);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new Size(300, 42);
            btnAddUser.TabIndex = 8;
            btnAddUser.Text = "➕ Thêm Tài Khoản Mới";
            btnAddUser.UseVisualStyleBackColor = false;
            btnAddUser.Click += btnAddUser_Click;

            //
            // btnReload
            //
            btnReload.BackColor = Color.FromArgb(70, 80, 95);
            btnReload.FlatAppearance.BorderSize = 0;
            btnReload.FlatStyle = FlatStyle.Flat;
            btnReload.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnReload.ForeColor = Color.White;
            btnReload.Location = new Point(15, 405);
            btnReload.Name = "btnReload";
            btnReload.Size = new Size(300, 38);
            btnReload.TabIndex = 9;
            btnReload.Text = "🔄 Tải Lại Danh Sách";
            btnReload.UseVisualStyleBackColor = false;
            btnReload.Click += btnReload_Click;

            //
            // FormUserManagement
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 660);
            Controls.Add(pnlLeft);
            Controls.Add(pnlRight);
            Name = "FormUserManagement";
            Text = "Quản trị Tài khoản Người dùng";
            Load += FormUserManagement_Load;

            pnlLeft.ResumeLayout(false);
            grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            pnlRight.ResumeLayout(false);
            grpAdd.ResumeLayout(false);
            grpAdd.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlLeft;
        private GroupBox grpList;
        private DataGridView dgvUsers;
        private Panel pnlPaging;
        private Button btnPrev;
        private Label lblPaging;
        private Button btnNext;
        private Panel pnlRight;
        private GroupBox grpAdd;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblRole;
        private ComboBox cboRole;
        private Button btnAddUser;
        private Button btnReload;
    }
}
