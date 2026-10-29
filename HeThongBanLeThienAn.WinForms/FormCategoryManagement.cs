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

namespace MiniSupermarket.WinForms
{
    // =============================================================
    // FORM QUẢN LÝ SẢN PHẨM VÀ TỒN KHO
    // =============================================================
    public partial class FormCategoryManagement : Form
    {
        private const int PageSize = 5;
        private int _currentPage = 1;
        private int _totalPages = 1;
        private bool _isLoading = false;

        public FormCategoryManagement()
        {
            InitializeComponent();
        }

        // Khi Form mở
        private async void FormCategoryManagement_Load(object sender, EventArgs e)
        {
            var menu = NavigationManager.CreateAppMenu(this);
            if (menu != null)
            {
                this.Controls.Add(menu);
                this.MainMenuStrip = menu;
            }

            await LoadDataAsync();
        }

        // =========================================================
        // LOAD DATA PHÂN TRANG PHÍA SERVER
        // =========================================================
        private async Task LoadDataAsync()
        {
            if (_isLoading) return;
            _isLoading = true;
            SetPagingControlsEnabled(false);

            try
            {
                var client = ApiClientService.Client;
                string keyword = Uri.EscapeDataString(txtKeyword.Text.Trim());
                string url = $"categories/paged?keyword={keyword}&pageNumber={_currentPage}&pageSize={PageSize}";

                var result = await client.GetFromJsonAsync<PagedViewResult<CategoryDto>>(url);
                if (result != null)
                {
                    _currentPage = Math.Max(result.CurrentPage, 1);
                    _totalPages = result.TotalPages;
                    dgvCategories.DataSource = result.Items;
                    FormatDataGridView();
                    lblPaging.Text = result.TotalPages == 0
                        ? "Trang 0 / 0 (Tổng: 0 nhóm)"
                        : $"Trang {_currentPage} / {_totalPages} (Tổng: {result.TotalItems} nhóm)";
                }
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
            finally
            {
                _isLoading = false;
                SetPagingControlsEnabled(true);
            }
        }

        private void SetPagingControlsEnabled(bool enabled)
        {
            btnPrev.Enabled = enabled && _currentPage > 1;
            btnNext.Enabled = enabled && _currentPage < _totalPages;
            btnSearch.Enabled = enabled;
            btnLoad.Enabled = enabled;
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadDataAsync();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadDataAsync();
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
                var client = ApiClientService.Client;
                var newCat = new
                {
                    CategoryName = txtCategoryName.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response =
                    await client.PostAsJsonAsync("categories", newCat);

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
                var client = ApiClientService.Client;
                var updateCat = new
                {
                    CategoryId = id,
                    CategoryName = txtCategoryName.Text.Trim(),
                    Description = txtDescription.Text.Trim()
                };

                var response =
                    await client.PutAsJsonAsync(
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
                var client = ApiClientService.Client;
                var response =
                    await client.DeleteAsync($"categories/{id}");

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
        // TÌM KIẾM (ĐỒNG BỘ PHÂN TRANG)
        // =========================================================
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadDataAsync();
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
