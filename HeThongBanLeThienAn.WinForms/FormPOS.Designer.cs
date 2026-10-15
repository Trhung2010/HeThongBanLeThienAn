namespace MiniSupermarket.WinForms
{
    partial class FormPOS
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
            grpCart = new GroupBox();
            dgvCart = new DataGridView();
            pnlBarcode = new Panel();
            txtBarcode = new TextBox();
            lblBarcode = new Label();
            btnClearCart = new Button();
            pnlRight = new Panel();
            grpPayment = new GroupBox();
            btnCheckout = new Button();
            lblChange = new Label();
            lblChangeTitle = new Label();
            txtCashReceived = new TextBox();
            lblCashReceived = new Label();
            lblTotalAmount = new Label();
            lblTotalTitle = new Label();
            grpCustomer = new GroupBox();
            btnLookupCustomer = new Button();
            lblCustomerName = new Label();
            txtCustomerPhone = new TextBox();
            lblCustomerPhone = new Label();

            pnlLeft.SuspendLayout();
            grpCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            pnlBarcode.SuspendLayout();
            pnlRight.SuspendLayout();
            grpPayment.SuspendLayout();
            grpCustomer.SuspendLayout();
            SuspendLayout();

            // 
            // pnlLeft (Vùng bên trái chứa Giỏ hàng và Quét Barcode)
            // 
            pnlLeft.Controls.Add(grpCart);
            pnlLeft.Controls.Add(pnlBarcode);
            pnlLeft.Dock = DockStyle.Fill;
            pnlLeft.Location = new Point(0, 0);
            pnlLeft.Name = "pnlLeft";
            pnlLeft.Padding = new Padding(12);
            pnlLeft.Size = new Size(680, 660);
            pnlLeft.TabIndex = 0;

            // 
            // pnlBarcode
            // 
            pnlBarcode.Controls.Add(btnClearCart);
            pnlBarcode.Controls.Add(txtBarcode);
            pnlBarcode.Controls.Add(lblBarcode);
            pnlBarcode.Dock = DockStyle.Top;
            pnlBarcode.Location = new Point(12, 12);
            pnlBarcode.Name = "pnlBarcode";
            pnlBarcode.Size = new Size(656, 70);
            pnlBarcode.TabIndex = 0;

            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBarcode.Location = new Point(3, 8);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(211, 23);
            lblBarcode.TabIndex = 0;
            lblBarcode.Text = "📷 Quét mã vạch Barcode:";

            // 
            // txtBarcode
            // 
            txtBarcode.Font = new Font("Segoe UI", 12F);
            txtBarcode.Location = new Point(3, 34);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Quét hoặc nhập mã vạch (ví dụ: 893000000002) rồi nhấn Enter...";
            txtBarcode.Size = new Size(520, 34);
            txtBarcode.TabIndex = 1;
            txtBarcode.KeyDown += txtBarcode_KeyDown;

            // 
            // btnClearCart
            // 
            btnClearCart.BackColor = Color.FromArgb(220, 60, 60);
            btnClearCart.FlatStyle = FlatStyle.Flat;
            btnClearCart.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnClearCart.ForeColor = Color.White;
            btnClearCart.Location = new Point(535, 34);
            btnClearCart.Name = "btnClearCart";
            btnClearCart.Size = new Size(118, 34);
            btnClearCart.TabIndex = 2;
            btnClearCart.Text = "🗑️ Hủy giỏ";
            btnClearCart.UseVisualStyleBackColor = false;
            btnClearCart.Click += btnClearCart_Click;

            // 
            // grpCart
            // 
            grpCart.Controls.Add(dgvCart);
            grpCart.Dock = DockStyle.Fill;
            grpCart.Font = new Font("Segoe UI", 9.5F);
            grpCart.Location = new Point(12, 82);
            grpCart.Name = "grpCart";
            grpCart.Padding = new Padding(8);
            grpCart.Size = new Size(656, 566);
            grpCart.TabIndex = 1;
            grpCart.TabStop = false;
            grpCart.Text = "Giỏ hàng thanh toán";

            // 
            // dgvCart
            // 
            dgvCart.AllowUserToAddRows = false;
            dgvCart.AllowUserToDeleteRows = false;
            dgvCart.BackgroundColor = Color.White;
            dgvCart.BorderStyle = BorderStyle.Fixed3D;
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Dock = DockStyle.Fill;
            dgvCart.Location = new Point(8, 30);
            dgvCart.MultiSelect = false;
            dgvCart.Name = "dgvCart";
            dgvCart.ReadOnly = true;
            dgvCart.RowHeadersWidth = 40;
            dgvCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCart.Size = new Size(640, 528);
            dgvCart.TabIndex = 0;

            // 
            // pnlRight (Vùng bên phải chứa Thông tin khách hàng & Thanh toán)
            // 
            pnlRight.Controls.Add(grpPayment);
            pnlRight.Controls.Add(grpCustomer);
            pnlRight.Dock = DockStyle.Right;
            pnlRight.Location = new Point(680, 0);
            pnlRight.Name = "pnlRight";
            pnlRight.Padding = new Padding(12);
            pnlRight.Size = new Size(370, 660);
            pnlRight.TabIndex = 1;

            // 
            // grpCustomer
            // 
            grpCustomer.Controls.Add(btnLookupCustomer);
            grpCustomer.Controls.Add(lblCustomerName);
            grpCustomer.Controls.Add(txtCustomerPhone);
            grpCustomer.Controls.Add(lblCustomerPhone);
            grpCustomer.Dock = DockStyle.Top;
            grpCustomer.Font = new Font("Segoe UI", 9.5F);
            grpCustomer.Location = new Point(12, 12);
            grpCustomer.Name = "grpCustomer";
            grpCustomer.Padding = new Padding(12);
            grpCustomer.Size = new Size(346, 140);
            grpCustomer.TabIndex = 0;
            grpCustomer.TabStop = false;
            grpCustomer.Text = "Khách hàng thân thiết";

            // 
            // lblCustomerPhone
            // 
            lblCustomerPhone.AutoSize = true;
            lblCustomerPhone.Location = new Point(12, 28);
            lblCustomerPhone.Name = "lblCustomerPhone";
            lblCustomerPhone.Size = new Size(104, 21);
            lblCustomerPhone.TabIndex = 0;
            lblCustomerPhone.Text = "Số điện thoại:";

            // 
            // txtCustomerPhone
            // 
            txtCustomerPhone.Location = new Point(12, 54);
            txtCustomerPhone.Name = "txtCustomerPhone";
            txtCustomerPhone.PlaceholderText = "Nhập SĐT (VD: 0901234567)...";
            txtCustomerPhone.Size = new Size(240, 29);
            txtCustomerPhone.TabIndex = 1;
            txtCustomerPhone.KeyDown += txtCustomerPhone_KeyDown;

            // 
            // btnLookupCustomer
            // 
            btnLookupCustomer.Location = new Point(258, 53);
            btnLookupCustomer.Name = "btnLookupCustomer";
            btnLookupCustomer.Size = new Size(76, 31);
            btnLookupCustomer.TabIndex = 2;
            btnLookupCustomer.Text = "Tìm";
            btnLookupCustomer.UseVisualStyleBackColor = true;
            btnLookupCustomer.Click += btnLookupCustomer_Click;

            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            lblCustomerName.ForeColor = Color.DarkSlateBlue;
            lblCustomerName.Location = new Point(12, 98);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(183, 21);
            lblCustomerName.TabIndex = 3;
            lblCustomerName.Text = "Khách hàng: Vãng lai";

            // 
            // grpPayment
            // 
            grpPayment.Controls.Add(btnCheckout);
            grpPayment.Controls.Add(lblChange);
            grpPayment.Controls.Add(lblChangeTitle);
            grpPayment.Controls.Add(txtCashReceived);
            grpPayment.Controls.Add(lblCashReceived);
            grpPayment.Controls.Add(lblTotalAmount);
            grpPayment.Controls.Add(lblTotalTitle);
            grpPayment.Dock = DockStyle.Fill;
            grpPayment.Font = new Font("Segoe UI", 9.5F);
            grpPayment.Location = new Point(12, 152);
            grpPayment.Name = "grpPayment";
            grpPayment.Padding = new Padding(12);
            grpPayment.Size = new Size(346, 496);
            grpPayment.TabIndex = 1;
            grpPayment.TabStop = false;
            grpPayment.Text = "Thanh toán đơn hàng";

            // 
            // lblTotalTitle
            // 
            lblTotalTitle.AutoSize = true;
            lblTotalTitle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTotalTitle.Location = new Point(12, 35);
            lblTotalTitle.Name = "lblTotalTitle";
            lblTotalTitle.Size = new Size(207, 25);
            lblTotalTitle.TabIndex = 0;
            lblTotalTitle.Text = "TỔNG TIỀN PHẢI TRẢ:";

            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTotalAmount.ForeColor = Color.Crimson;
            lblTotalAmount.Location = new Point(12, 65);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(86, 50);
            lblTotalAmount.TabIndex = 1;
            lblTotalAmount.Text = "0 đ";

            // 
            // lblCashReceived
            // 
            lblCashReceived.AutoSize = true;
            lblCashReceived.Font = new Font("Segoe UI", 10F);
            lblCashReceived.Location = new Point(12, 140);
            lblCashReceived.Name = "lblCashReceived";
            lblCashReceived.Size = new Size(164, 23);
            lblCashReceived.TabIndex = 2;
            lblCashReceived.Text = "Tiền khách đưa (VNĐ):";

            // 
            // txtCashReceived
            // 
            txtCashReceived.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            txtCashReceived.ForeColor = Color.DarkBlue;
            txtCashReceived.Location = new Point(12, 170);
            txtCashReceived.Name = "txtCashReceived";
            txtCashReceived.PlaceholderText = "Nhập tiền...";
            txtCashReceived.Size = new Size(320, 39);
            txtCashReceived.TabIndex = 3;
            txtCashReceived.TextChanged += txtCashReceived_TextChanged;

            // 
            // lblChangeTitle
            // 
            lblChangeTitle.AutoSize = true;
            lblChangeTitle.Font = new Font("Segoe UI", 10F);
            lblChangeTitle.Location = new Point(12, 230);
            lblChangeTitle.Name = "lblChangeTitle";
            lblChangeTitle.Size = new Size(155, 23);
            lblChangeTitle.TabIndex = 4;
            lblChangeTitle.Text = "Tiền thừa trả khách:";

            // 
            // lblChange
            // 
            lblChange.AutoSize = true;
            lblChange.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblChange.ForeColor = Color.ForestGreen;
            lblChange.Location = new Point(12, 260);
            lblChange.Name = "lblChange";
            lblChange.Size = new Size(67, 41);
            lblChange.TabIndex = 5;
            lblChange.Text = "0 đ";

            // 
            // btnCheckout
            // 
            btnCheckout.BackColor = Color.ForestGreen;
            btnCheckout.Dock = DockStyle.Bottom;
            btnCheckout.FlatAppearance.BorderSize = 0;
            btnCheckout.FlatStyle = FlatStyle.Flat;
            btnCheckout.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCheckout.ForeColor = Color.White;
            btnCheckout.Location = new Point(12, 424);
            btnCheckout.Name = "btnCheckout";
            btnCheckout.Size = new Size(322, 60);
            btnCheckout.TabIndex = 6;
            btnCheckout.Text = "💳 THANH TOÁN (F9)";
            btnCheckout.UseVisualStyleBackColor = false;
            btnCheckout.Click += btnCheckout_Click;

            // 
            // FormPOS
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 245, 247);
            ClientSize = new Size(1050, 660);
            Controls.Add(pnlLeft);
            Controls.Add(pnlRight);
            KeyPreview = true;
            Name = "FormPOS";
            Text = "Hệ thống Quầy Bán hàng POS";
            KeyDown += FormPOS_KeyDown;

            pnlLeft.ResumeLayout(false);
            grpCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            pnlBarcode.ResumeLayout(false);
            pnlBarcode.PerformLayout();
            pnlRight.ResumeLayout(false);
            grpPayment.ResumeLayout(false);
            grpPayment.PerformLayout();
            grpCustomer.ResumeLayout(false);
            grpCustomer.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlLeft;
        private Panel pnlBarcode;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Button btnClearCart;
        private GroupBox grpCart;
        private DataGridView dgvCart;
        private Panel pnlRight;
        private GroupBox grpCustomer;
        private Label lblCustomerPhone;
        private TextBox txtCustomerPhone;
        private Button btnLookupCustomer;
        private Label lblCustomerName;
        private GroupBox grpPayment;
        private Label lblTotalTitle;
        private Label lblTotalAmount;
        private Label lblCashReceived;
        private TextBox txtCashReceived;
        private Label lblChangeTitle;
        private Label lblChange;
        private Button btnCheckout;
    }
}
