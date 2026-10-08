namespace MiniSupermarket.WinForms
{
    partial class FormProductManagement
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
            dgvProducts = new DataGridView();
            txtKeyword = new TextBox();
            btnSearch = new Button();
            btnLoad = new Button();
            grpSearch = new GroupBox();
            grpProductList = new GroupBox();
            grpProductInfo = new GroupBox();
            lblId = new Label();
            txtId = new TextBox();
            lblBarcode = new Label();
            txtBarcode = new TextBox();
            lblProductName = new Label();
            txtProductName = new TextBox();
            lblPrice = new Label();
            txtPrice = new TextBox();
            lblStockQuantity = new Label();
            txtStockQuantity = new TextBox();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();

            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            grpSearch.SuspendLayout();
            grpProductList.SuspendLayout();
            grpProductInfo.SuspendLayout();
            SuspendLayout();

            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.AllowUserToDeleteRows = false;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.BackgroundColor = Color.White;
            dgvProducts.BorderStyle = BorderStyle.Fixed3D;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(12, 28);
            dgvProducts.Margin = new Padding(3, 4, 3, 4);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersWidth = 40;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(576, 460);
            dgvProducts.TabIndex = 0;
            dgvProducts.CellClick += dgvProducts_CellClick;

            // 
            // txtKeyword
            // 
            txtKeyword.Location = new Point(17, 30);
            txtKeyword.Margin = new Padding(3, 4, 3, 4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập tên sản phẩm hoặc mã vạch cần tìm...";
            txtKeyword.Size = new Size(620, 27);
            txtKeyword.TabIndex = 0;

            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(650, 27);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(115, 33);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "🔍 Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;

            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(775, 27);
            btnLoad.Margin = new Padding(3, 4, 3, 4);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(115, 33);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "🔄 Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;

            // 
            // grpSearch
            // 
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Location = new Point(20, 40);
            grpSearch.Margin = new Padding(3, 4, 3, 4);
            grpSearch.Name = "grpSearch";
            grpSearch.Padding = new Padding(3, 4, 3, 4);
            grpSearch.Size = new Size(910, 75);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm sản phẩm";

            // 
            // grpProductList
            // 
            grpProductList.Controls.Add(dgvProducts);
            grpProductList.Location = new Point(20, 125);
            grpProductList.Margin = new Padding(3, 4, 3, 4);
            grpProductList.Name = "grpProductList";
            grpProductList.Padding = new Padding(3, 4, 3, 4);
            grpProductList.Size = new Size(600, 500);
            grpProductList.TabIndex = 1;
            grpProductList.TabStop = false;
            grpProductList.Text = "Danh sách Sản phẩm";

            // 
            // grpProductInfo
            // 
            grpProductInfo.Controls.Add(lblId);
            grpProductInfo.Controls.Add(txtId);
            grpProductInfo.Controls.Add(lblBarcode);
            grpProductInfo.Controls.Add(txtBarcode);
            grpProductInfo.Controls.Add(lblProductName);
            grpProductInfo.Controls.Add(txtProductName);
            grpProductInfo.Controls.Add(lblPrice);
            grpProductInfo.Controls.Add(txtPrice);
            grpProductInfo.Controls.Add(lblStockQuantity);
            grpProductInfo.Controls.Add(txtStockQuantity);
            grpProductInfo.Controls.Add(lblCategory);
            grpProductInfo.Controls.Add(cboCategory);
            grpProductInfo.Controls.Add(btnAdd);
            grpProductInfo.Controls.Add(btnUpdate);
            grpProductInfo.Controls.Add(btnDelete);
            grpProductInfo.Location = new Point(635, 125);
            grpProductInfo.Margin = new Padding(3, 4, 3, 4);
            grpProductInfo.Name = "grpProductInfo";
            grpProductInfo.Padding = new Padding(3, 4, 3, 4);
            grpProductInfo.Size = new Size(295, 500);
            grpProductInfo.TabIndex = 2;
            grpProductInfo.TabStop = false;
            grpProductInfo.Text = "Thông tin Sản phẩm";

            // 
            // lblId
            // 
            lblId.AutoSize = true;
            lblId.Location = new Point(15, 25);
            lblId.Name = "lblId";
            lblId.Size = new Size(54, 20);
            lblId.TabIndex = 0;
            lblId.Text = "Mã SP:";

            // 
            // txtId
            // 
            txtId.Location = new Point(15, 48);
            txtId.Margin = new Padding(3, 4, 3, 4);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(260, 27);
            txtId.TabIndex = 1;

            // 
            // lblBarcode
            // 
            lblBarcode.AutoSize = true;
            lblBarcode.Location = new Point(15, 82);
            lblBarcode.Name = "lblBarcode";
            lblBarcode.Size = new Size(68, 20);
            lblBarcode.TabIndex = 2;
            lblBarcode.Text = "Mã vạch:";

            // 
            // txtBarcode
            // 
            txtBarcode.Location = new Point(15, 105);
            txtBarcode.Margin = new Padding(3, 4, 3, 4);
            txtBarcode.Name = "txtBarcode";
            txtBarcode.PlaceholderText = "Ví dụ: 893000000001";
            txtBarcode.Size = new Size(260, 27);
            txtBarcode.TabIndex = 3;

            // 
            // lblProductName
            // 
            lblProductName.AutoSize = true;
            lblProductName.Location = new Point(15, 139);
            lblProductName.Name = "lblProductName";
            lblProductName.Size = new Size(103, 20);
            lblProductName.TabIndex = 4;
            lblProductName.Text = "Tên sản phẩm:";

            // 
            // txtProductName
            // 
            txtProductName.Location = new Point(15, 162);
            txtProductName.Margin = new Padding(3, 4, 3, 4);
            txtProductName.Name = "txtProductName";
            txtProductName.PlaceholderText = "Nhập tên sản phẩm...";
            txtProductName.Size = new Size(260, 27);
            txtProductName.TabIndex = 5;

            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Location = new Point(15, 196);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(99, 20);
            lblPrice.TabIndex = 6;
            lblPrice.Text = "Đơn giá (VNĐ):";

            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(15, 219);
            txtPrice.Margin = new Padding(3, 4, 3, 4);
            txtPrice.Name = "txtPrice";
            txtPrice.PlaceholderText = "Ví dụ: 25000";
            txtPrice.Size = new Size(260, 27);
            txtPrice.TabIndex = 7;

            // 
            // lblStockQuantity
            // 
            lblStockQuantity.AutoSize = true;
            lblStockQuantity.Location = new Point(15, 253);
            lblStockQuantity.Name = "lblStockQuantity";
            lblStockQuantity.Size = new Size(94, 20);
            lblStockQuantity.TabIndex = 8;
            lblStockQuantity.Text = "Số lượng tồn:";

            // 
            // txtStockQuantity
            // 
            txtStockQuantity.Location = new Point(15, 276);
            txtStockQuantity.Margin = new Padding(3, 4, 3, 4);
            txtStockQuantity.Name = "txtStockQuantity";
            txtStockQuantity.PlaceholderText = "Ví dụ: 50";
            txtStockQuantity.Size = new Size(260, 27);
            txtStockQuantity.TabIndex = 9;

            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Location = new Point(15, 310);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(79, 20);
            lblCategory.TabIndex = 10;
            lblCategory.Text = "Danh mục:";

            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(15, 333);
            cboCategory.Margin = new Padding(3, 4, 3, 4);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(260, 28);
            cboCategory.TabIndex = 11;

            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.ForestGreen;
            btnAdd.ForeColor = Color.White;
            btnAdd.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnAdd.Location = new Point(15, 385);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 36);
            btnAdd.TabIndex = 12;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;

            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.SteelBlue;
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUpdate.Location = new Point(105, 385);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 36);
            btnUpdate.TabIndex = 13;
            btnUpdate.Text = "✏️ Sửa";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;

            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.IndianRed;
            btnDelete.ForeColor = Color.White;
            btnDelete.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDelete.Location = new Point(195, 385);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 36);
            btnDelete.TabIndex = 14;
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;

            // 
            // FormProductManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(950, 640);
            Controls.Add(grpSearch);
            Controls.Add(grpProductList);
            Controls.Add(grpProductInfo);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "FormProductManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống Bán lẻ Thiên Ân - Quản lý Sản phẩm (Product)";
            Load += FormProductManagement_Load;

            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpProductList.ResumeLayout(false);
            grpProductInfo.ResumeLayout(false);
            grpProductInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvProducts;
        private TextBox txtKeyword;
        private Button btnSearch;
        private Button btnLoad;
        private GroupBox grpSearch;
        private GroupBox grpProductList;
        private GroupBox grpProductInfo;
        private Label lblId;
        private TextBox txtId;
        private Label lblBarcode;
        private TextBox txtBarcode;
        private Label lblProductName;
        private TextBox txtProductName;
        private Label lblPrice;
        private TextBox txtPrice;
        private Label lblStockQuantity;
        private TextBox txtStockQuantity;
        private Label lblCategory;
        private ComboBox cboCategory;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
    }
}
