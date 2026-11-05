namespace MiniSupermarket.WinForms
{
    partial class FormCategoryManagement
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

        private void InitializeComponent()
        {
            dgvCategories = new DataGridView();
            btnPrev = new Button();
            lblPaging = new Label();
            btnNext = new Button();
            txtKeyword = new TextBox();
            txtDescription = new TextBox();
            txtId = new TextBox();
            txtCategoryName = new TextBox();
            btnSearch = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            btnLoad = new Button();
            grpSearch = new GroupBox();
            grpCategoryList = new GroupBox();
            grpCategoryInfo = new GroupBox();
            lblId = new Label();
            lblCategoryName = new Label();
            lblDescription = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvCategories).BeginInit();
            grpSearch.SuspendLayout();
            grpCategoryList.SuspendLayout();
            grpCategoryInfo.SuspendLayout();
            SuspendLayout();
            //
            // dgvCategories
            //
            dgvCategories.AllowUserToAddRows = false;
            dgvCategories.AllowUserToDeleteRows = false;
            dgvCategories.AllowUserToResizeRows = false;
            dgvCategories.BackgroundColor = Color.White;
            dgvCategories.BorderStyle = BorderStyle.Fixed3D;
            dgvCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCategories.Location = new Point(12, 28);
            dgvCategories.Margin = new Padding(3, 4, 3, 4);
            dgvCategories.MultiSelect = false;
            dgvCategories.Name = "dgvCategories";
            dgvCategories.ReadOnly = true;
            dgvCategories.RowHeadersWidth = 40;
            dgvCategories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCategories.Size = new Size(549, 407);
            dgvCategories.TabIndex = 0;
            dgvCategories.CellClick += dgvCategories_CellClick;
            //
            // txtKeyword
            //
            txtKeyword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtKeyword.Location = new Point(17, 28);
            txtKeyword.Margin = new Padding(3, 4, 3, 4);
            txtKeyword.Name = "txtKeyword";
            txtKeyword.PlaceholderText = "Nhập từ khóa...";
            txtKeyword.Size = new Size(610, 27);
            txtKeyword.TabIndex = 1;
            //
            // txtDescription
            //
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(17, 227);
            txtDescription.Margin = new Padding(3, 4, 3, 4);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(265, 110);
            txtDescription.TabIndex = 5;
            //
            // txtId
            //
            txtId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtId.Location = new Point(17, 67);
            txtId.Margin = new Padding(3, 4, 3, 4);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(265, 27);
            txtId.TabIndex = 1;
            //
            // txtCategoryName
            //
            txtCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCategoryName.Location = new Point(17, 147);
            txtCategoryName.Margin = new Padding(3, 4, 3, 4);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(265, 27);
            txtCategoryName.TabIndex = 3;
            //
            // btnSearch
            //
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(637, 25);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(110, 31);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            //
            // btnDelete
            //
            btnDelete.Location = new Point(189, 350);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(69, 40);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            //
            // btnUpdate
            //
            btnUpdate.Location = new Point(97, 350);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(86, 40);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            //
            // btnAdd
            //
            btnAdd.Location = new Point(17, 350);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(74, 40);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            //
            // btnLoad
            //
            btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLoad.Location = new Point(753, 25);
            btnLoad.Margin = new Padding(3, 4, 3, 4);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(105, 31);
            btnLoad.TabIndex = 3;
            btnLoad.Text = "Tải lại";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            //
            // grpSearch
            //
            grpSearch.Controls.Add(txtKeyword);
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(btnLoad);
            grpSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpSearch.Location = new Point(20, 15);
            grpSearch.Margin = new Padding(3, 4, 3, 4);
            grpSearch.Name = "grpSearch";
            grpSearch.Padding = new Padding(3, 4, 3, 4);
            grpSearch.Size = new Size(874, 75);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Tìm kiếm";
                        //
            // btnPrev
            //
            btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPrev.Location = new Point(15, 470);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(110, 32);
            btnPrev.TabIndex = 1;
            btnPrev.Text = "<< Trang trước";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            //
            // lblPaging
            //
            lblPaging.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPaging.Location = new Point(135, 475);
            lblPaging.Name = "lblPaging";
            lblPaging.Size = new Size(250, 24);
            lblPaging.TabIndex = 2;
            lblPaging.Text = "Trang 0 / 0 (Tổng: 0 nhóm)";
            lblPaging.TextAlign = ContentAlignment.MiddleCenter;
            //
            // btnNext
            //
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnNext.Location = new Point(395, 470);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(110, 32);
            btnNext.TabIndex = 3;
            btnNext.Text = "Trang sau >>";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            //
            // grpCategoryList
            //
            grpCategoryList.Controls.Add(dgvCategories);
            grpCategoryList.Controls.Add(btnPrev);
            grpCategoryList.Controls.Add(lblPaging);
            grpCategoryList.Controls.Add(btnNext);
            grpCategoryList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpCategoryList.Location = new Point(20, 100);
            grpCategoryList.Margin = new Padding(3, 4, 3, 4);
            grpCategoryList.Name = "grpCategoryList";
            grpCategoryList.Padding = new Padding(3, 4, 3, 4);
            grpCategoryList.Size = new Size(560, 515);
            grpCategoryList.TabIndex = 4;
            grpCategoryList.TabStop = false;
            grpCategoryList.Text = "Danh sách Nhóm hàng";
            grpCategoryList.Enter += grpCategoryList_Enter;
            //
            // grpCategoryInfo
            //
            grpCategoryInfo.Controls.Add(lblId);
            grpCategoryInfo.Controls.Add(txtId);
            grpCategoryInfo.Controls.Add(lblCategoryName);
            grpCategoryInfo.Controls.Add(txtCategoryName);
            grpCategoryInfo.Controls.Add(lblDescription);
            grpCategoryInfo.Controls.Add(txtDescription);
            grpCategoryInfo.Controls.Add(btnAdd);
            grpCategoryInfo.Controls.Add(btnUpdate);
            grpCategoryInfo.Controls.Add(btnDelete);
            grpCategoryInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpCategoryInfo.Location = new Point(594, 100);
            grpCategoryInfo.Margin = new Padding(3, 4, 3, 4);
            grpCategoryInfo.Name = "grpCategoryInfo";
            grpCategoryInfo.Padding = new Padding(3, 4, 3, 4);
            grpCategoryInfo.Size = new Size(300, 515);
            grpCategoryInfo.TabIndex = 5;
            grpCategoryInfo.TabStop = false;
            grpCategoryInfo.Text = "Thông tin Nhóm hàng";
            //
            // lblId
            //
            lblId.AutoSize = true;
            lblId.Location = new Point(17, 40);
            lblId.Name = "lblId";
            lblId.Size = new Size(49, 20);
            lblId.TabIndex = 0;
            lblId.Text = "Mã ID";
            //
            // lblCategoryName
            //
            lblCategoryName.AutoSize = true;
            lblCategoryName.Location = new Point(17, 120);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new Size(230, 20);
            lblCategoryName.TabIndex = 2;
            lblCategoryName.Text = "Tên nhóm hàng";
            //
            // lblDescription
            //
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(17, 200);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(162, 20);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Mô Tả (Mô tả chi tiết...)";
            //
            // FormCategoryManagement
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 630);
            Controls.Add(grpSearch);
            Controls.Add(grpCategoryList);
            Controls.Add(grpCategoryInfo);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormCategoryManagement";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý Danh mục (Categories) - Thiên Ân";
            Load += FormCategoryManagement_Load;
            ((System.ComponentModel.ISupportInitialize)dgvCategories).EndInit();
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpCategoryList.ResumeLayout(false);
            grpCategoryInfo.ResumeLayout(false);
            grpCategoryInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // =========================================================
        // DATA GRID VIEW
        // =========================================================

        private DataGridView dgvCategories;
        private Button btnPrev;
        private Label lblPaging;
        private Button btnNext;

        // =========================================================
        // TEXTBOX
        // =========================================================

        private TextBox txtKeyword;
        private TextBox txtDescription;
        private TextBox txtId;
        private TextBox txtCategoryName;

        // =========================================================
        // BUTTON
        // =========================================================

        private Button btnSearch;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private Button btnLoad;

        // =========================================================
        // GROUPBOX
        // =========================================================

        private GroupBox grpSearch;
        private GroupBox grpCategoryList;
        private GroupBox grpCategoryInfo;

        // =========================================================
        // LABEL
        // =========================================================

        private Label lblId;
        private Label lblCategoryName;
        private Label lblDescription;
    }
}
