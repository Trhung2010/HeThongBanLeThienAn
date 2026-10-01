namespace MiniSupermarket.WinForms
{
    partial class FormLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxLogin = new GroupBox();
            lblUserIcon = new Label();
            lblPassIcon = new Label();
            label1 = new Label();
            label2 = new Label();
            txtUser = new TextBox();
            txtPass = new TextBox();
            btnLogin = new Button();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();

            groupBoxLogin.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();

            // 
            // groupBoxLogin
            // 
            groupBoxLogin.Controls.Add(lblUserIcon);
            groupBoxLogin.Controls.Add(lblPassIcon);
            groupBoxLogin.Controls.Add(label1);
            groupBoxLogin.Controls.Add(label2);
            groupBoxLogin.Controls.Add(txtUser);
            groupBoxLogin.Controls.Add(txtPass);
            groupBoxLogin.Controls.Add(btnLogin);

            groupBoxLogin.Location = new Point(95, 55);
            groupBoxLogin.Name = "groupBoxLogin";
            groupBoxLogin.Size = new Size(430, 270);
            groupBoxLogin.TabIndex = 0;
            groupBoxLogin.TabStop = false;
            groupBoxLogin.Text = "Đăng nhập";

            // 
            // lblUserIcon
            // 
            lblUserIcon.AutoSize = true;
            lblUserIcon.Font = new Font("Segoe MDL2 Assets", 20F);
            lblUserIcon.Location = new Point(25, 48);
            lblUserIcon.Name = "lblUserIcon";
            lblUserIcon.Size = new Size(35, 27);
            lblUserIcon.TabIndex = 0;

            // Biểu tượng người dùng
            lblUserIcon.Text = "\uE77B";

            // 
            // lblPassIcon
            // 
            lblPassIcon.AutoSize = true;
            lblPassIcon.Font = new Font("Segoe MDL2 Assets", 20F);
            lblPassIcon.Location = new Point(25, 100);
            lblPassIcon.Name = "lblPassIcon";
            lblPassIcon.Size = new Size(35, 27);
            lblPassIcon.TabIndex = 1;

            // Biểu tượng ổ khóa
            lblPassIcon.Text = "\uE72E";

            // 
            // label1 - Tài khoản
            // 
            label1.AutoSize = true;
            label1.Location = new Point(70, 58);
            label1.Name = "label1";
            label1.Size = new Size(71, 20);
            label1.TabIndex = 2;
            label1.Text = "Tài khoản";

            // 
            // label2 - Mật khẩu
            // 
            label2.AutoSize = true;
            label2.Location = new Point(70, 110);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 3;
            label2.Text = "Mật khẩu";

            // 
            // txtUser
            // 
            txtUser.Location = new Point(170, 54);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "Ví dụ: admin";
            txtUser.Size = new Size(220, 27);
            txtUser.TabIndex = 4;

            // 
            // txtPass
            // 
            txtPass.Location = new Point(170, 106);
            txtPass.Name = "txtPass";
            txtPass.PlaceholderText = "Nhập mật khẩu";
            txtPass.Size = new Size(220, 27);
            txtPass.TabIndex = 5;
            txtPass.UseSystemPasswordChar = true;

            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(105, 170);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(200, 40);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Đăng nhập hệ thống";
            btnLogin.UseVisualStyleBackColor = true;

            // Gắn sự kiện Click cho nút đăng nhập
            btnLogin.Click += btnLogin_Click;

            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 378);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(620, 22);
            statusStrip1.TabIndex = 7;
            statusStrip1.Text = "statusStrip1";

            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(165, 17);
            lblStatus.Text = "Kết nối: Chưa xác thực";

            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(620, 400);

            Controls.Add(statusStrip1);
            Controls.Add(groupBoxLogin);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = true;

            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập hệ thống";

            groupBoxLogin.ResumeLayout(false);
            groupBoxLogin.PerformLayout();

            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBoxLogin;
        private Label lblUserIcon;
        private Label lblPassIcon;
        private Label label1;
        private Label label2;
        private TextBox txtUser;
        private TextBox txtPass;
        private Button btnLogin;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
    }
}