using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace MiniSupermarket.WinForms
{
    // =============================================================
    // FORM QUẢN LÝ SẢN PHẨM VÀ TỒN KHO
    // =============================================================
    public partial class FormCategoryManagement : Form
    {
        private static readonly HttpClient _client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7163/api/")
        };

        public FormCategoryManagement()
        {
            InitializeComponent();
        }

        // Khi Form mở
        private async void FormCategoryManagement_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        // =========================================================
        // TẠO HTTP CLIENT KÈM JWT TOKEN
        // =========================================================
        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7163/api/")
            };

            // Gắn Bearer Token vào Header
            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(
                        "Bearer",
                        SessionManager.JwtToken);
            }

            return client;
        }

        // =========================================================
        // LOAD DATA
        // =========================================================
        private async Task LoadDataAsync()
        {
            try
            {
                // Tạo HttpClient đã được gắn JWT Token
                using var client = GetAuthenticatedClient();

                var categories =
                    await client.GetFromJsonAsync<List<CategoryDto>>("categories");

                dgvCategories.DataSource = categories;

                FormatDataGridView();
                UpdatePermissionButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private bool IsCashier()
        {
            return string.Equals(SessionManager.CurrentRole, "Cashier", StringComparison.OrdinalIgnoreCase);
        }

        private void UpdatePermissionButtons()
        {
            bool isReadOnlyCashier = IsCashier();
            btnAdd.Enabled = !isReadOnlyCashier;
            btnUpdate.Enabled = !isReadOnlyCashier;
            btnDelete.Enabled = !isReadOnlyCashier;
        }

        // =========================================================
        // FORMAT DATAGRIDVIEW
        // =========================================================
        private void FormatDataGridView()
        {
            if (dgvCategories.Columns.Count == 0)
                return;

            dgvCategories.AutoGenerateColumns = true;
            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (dgvCategories.Columns["CategoryId"] != null)
            {
                dgvCategories.Columns["CategoryId"].HeaderText = "Mã ID";
                dgvCategories.Columns["CategoryId"].Width = 65;
            }

            if (dgvCategories.Columns["CategoryName"] != null)
            {
                dgvCategories.Columns["CategoryName"].HeaderText = "Tên sản phẩm";
                dgvCategories.Columns["CategoryName"].Width = 275;
            }

            if (dgvCategories.Columns["Description"] != null)
            {
                dgvCategories.Columns["Description"].HeaderText = "Mô Tả";
                dgvCategories.Columns["Description"].Width = 260;
            }

            if (dgvCategories.Columns["StockQuantity"] != null)
            {
                dgvCategories.Columns["StockQuantity"].HeaderText = "Tồn kho";
                dgvCategories.Columns["StockQuantity"].Width = 90;
            }

            dgvCategories.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvCategories.MultiSelect = false;

            dgvCategories.AllowUserToAddRows = false;

            dgvCategories.ReadOnly = true;
        }

        // =========================================================
        // TẢI LẠI
        // =========================================================
        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            ClearInputs();

            await LoadDataAsync();
        }

        // =========================================================
        // CLICK DÒNG
        // =========================================================
        private void dgvCategories_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvCategories.Rows[e.RowIndex];

            if (row.Cells["CategoryId"].Value != null)
            {
                txtId.Text =
                    row.Cells["CategoryId"].Value.ToString();
            }

            if (row.Cells["CategoryName"].Value != null)
            {
                txtCategoryName.Text =
                    row.Cells["CategoryName"].Value.ToString();
            }

            if (row.Cells["Description"].Value != null)
            {
                txtDescription.Text =
                    row.Cells["Description"].Value.ToString();
            }
            else
            {
                txtDescription.Clear();
            }

            if (row.Cells["StockQuantity"].Value != null)
            {
                txtStockQuantity.Text = row.Cells["StockQuantity"].Value.ToString();
            }
        }

        // =========================================================
        // THÊM MỚI
        // =========================================================
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsCashier())
            {
                MessageBox.Show(
                    "Tài khoản Cashier chỉ được xem dữ liệu, không được thêm sản phẩm.",
                    "Quyền truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên sản phẩm!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtCategoryName.Focus();
                return;
            }

            try
            {
                var newCat = new
                {
                    CategoryName = txtCategoryName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    StockQuantity = ParseStockQuantity()
                };

                var response =
                    await _client.PostAsJsonAsync("categories", newCat);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Thêm mới thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        "Thêm mới thất bại!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // CẬP NHẬT
        // =========================================================
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsCashier())
            {
                MessageBox.Show(
                    "Tài khoản Cashier chỉ được xem dữ liệu, không được sửa sản phẩm.",
                    "Quyền truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần sửa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show(
                    "Vui lòng nhập tên sản phẩm!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                var updateCat = new
                {
                    CategoryId = id,
                    CategoryName = txtCategoryName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    StockQuantity = ParseStockQuantity()
                };

                var response =
                    await _client.PutAsJsonAsync(
                        $"categories/{id}",
                        updateCat);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Cập nhật thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        "Cập nhật thất bại!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // XÓA
        // =========================================================
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsCashier())
            {
                MessageBox.Show(
                    "Tài khoản Cashier chỉ được xem dữ liệu, không được xóa sản phẩm.",
                    "Quyền truy cập",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtId.Text))
            {
                MessageBox.Show(
                    "Vui lòng chọn sản phẩm cần xóa!",
                    "Cảnh báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show(
                    "Mã ID không hợp lệ!",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa sản phẩm ID = {id}?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                var response =
                    await _client.DeleteAsync($"categories/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        "Xóa thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show(
                        "Xóa thất bại!",
                        "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // TÌM KIẾM
        // =========================================================
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtKeyword.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                await LoadDataAsync();
                return;
            }

            try
            {
                string encodedKeyword =
                    Uri.EscapeDataString(keyword);

                var result =
                    await _client.GetFromJsonAsync<List<CategoryDto>>(
                        $"categories/search?keyword={encodedKeyword}");

                dgvCategories.DataSource = result;

                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tìm thấy kết quả phù hợp!\n" + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        // =========================================================
        // XÓA Ô NHẬP
        // =========================================================
        private void ClearInputs()
        {
            txtId.Clear();
            txtCategoryName.Clear();
            txtDescription.Clear();
            txtStockQuantity.Clear();
        }

        private int ParseStockQuantity()
        {
            return int.TryParse(txtStockQuantity.Text.Trim(), out int quantity) && quantity >= 0
                ? quantity
                : 0;
        }

        private void grpCategoryList_Enter(object sender, EventArgs e)
        {

        }
    }

    // =============================================================
    // DTO (Data Transfer Object) cho Category
    // =============================================================
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int StockQuantity { get; set; }
    }
}