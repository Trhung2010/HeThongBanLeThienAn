using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    // =============================================================
    // FORM QUẢN LÝ SẢN PHẨM (PRODUCTS)
    // =============================================================
    public partial class FormProductManagement : Form
    {
        private List<CategoryDto> _categoryList = new List<CategoryDto>();

        public FormProductManagement()
        {
            InitializeComponent();
        }

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            // Gắn thanh điều hướng MenuStrip lên đầu Form
            var menu = NavigationManager.CreateAppMenu(this);
            this.Controls.Add(menu);
            this.MainMenuStrip = menu;

            await LoadCategoriesAsync();
            await LoadDataAsync();
            UpdatePermissionButtons();
        }

        private HttpClient GetAuthenticatedClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5167/api/")
            };

            if (!string.IsNullOrEmpty(SessionManager.JwtToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);
            }

            return client;
        }

        private bool IsCashier()
        {
            return string.Equals(SessionManager.CurrentRole, "Cashier", StringComparison.OrdinalIgnoreCase);
        }

        private void UpdatePermissionButtons()
        {
            bool isReadOnly = IsCashier();
            btnAdd.Enabled = !isReadOnly;
            btnUpdate.Enabled = !isReadOnly;
            btnDelete.Enabled = !isReadOnly;
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();
                var categories = await client.GetFromJsonAsync<List<CategoryDto>>("categories");
                if (categories != null && categories.Count > 0)
                {
                    _categoryList = categories;
                    cboCategory.DataSource = new BindingSource(_categoryList, null);
                    cboCategory.DisplayMember = "CategoryName";
                    cboCategory.ValueMember = "CategoryId";
                }
            }
            catch
            {
                // Bỏ qua lỗi load danh mục phụ nếu API chưa sẵn sàng
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                using var client = GetAuthenticatedClient();
                var products = await client.GetFromJsonAsync<List<ProductDto>>("products");

                dgvProducts.DataSource = products;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi kết nối Server khi tải sản phẩm:\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void FormatDataGridView()
        {
            if (dgvProducts.Columns.Count == 0)
                return;

            dgvProducts.AutoGenerateColumns = true;
            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            if (dgvProducts.Columns["ProductId"] != null)
            {
                dgvProducts.Columns["ProductId"].HeaderText = "Mã SP";
                dgvProducts.Columns["ProductId"].Width = 65;
            }

            if (dgvProducts.Columns["Barcode"] != null)
            {
                dgvProducts.Columns["Barcode"].HeaderText = "Mã vạch";
                dgvProducts.Columns["Barcode"].Width = 110;
            }

            if (dgvProducts.Columns["ProductName"] != null)
            {
                dgvProducts.Columns["ProductName"].HeaderText = "Tên sản phẩm";
                dgvProducts.Columns["ProductName"].Width = 175;
            }

            if (dgvProducts.Columns["Price"] != null)
            {
                dgvProducts.Columns["Price"].HeaderText = "Đơn giá (đ)";
                dgvProducts.Columns["Price"].Width = 95;
                dgvProducts.Columns["Price"].DefaultCellStyle.Format = "N0";
            }

            if (dgvProducts.Columns["StockQuantity"] != null)
            {
                dgvProducts.Columns["StockQuantity"].HeaderText = "Tồn kho";
                dgvProducts.Columns["StockQuantity"].Width = 70;
            }

            if (dgvProducts.Columns["CategoryId"] != null)
            {
                dgvProducts.Columns["CategoryId"].HeaderText = "Mã Cate";
                dgvProducts.Columns["CategoryId"].Width = 65;
            }

            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.ReadOnly = true;
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

            txtId.Text = row.Cells["ProductId"]?.Value?.ToString() ?? string.Empty;
            txtBarcode.Text = row.Cells["Barcode"]?.Value?.ToString() ?? string.Empty;
            txtProductName.Text = row.Cells["ProductName"]?.Value?.ToString() ?? string.Empty;
            txtPrice.Text = row.Cells["Price"]?.Value?.ToString() ?? string.Empty;
            txtStockQuantity.Text = row.Cells["StockQuantity"]?.Value?.ToString() ?? string.Empty;

            if (row.Cells["CategoryId"]?.Value != null &&
                int.TryParse(row.Cells["CategoryId"].Value.ToString(), out int catId))
            {
                cboCategory.SelectedValue = catId;
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            ClearInputs();
            await LoadCategoriesAsync();
            await LoadDataAsync();
        }

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
                using var client = GetAuthenticatedClient();
                string encodedKeyword = Uri.EscapeDataString(keyword);
                var result = await client.GetFromJsonAsync<List<ProductDto>>($"products/search?keyword={encodedKeyword}");

                dgvProducts.DataSource = result;
                FormatDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Không tìm thấy sản phẩm phù hợp!\n" + ex.Message,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsCashier())
            {
                MessageBox.Show("Nhân viên thu ngân không có quyền thêm sản phẩm!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string barcode = txtBarcode.Text.Trim();
            string name = txtProductName.Text.Trim();

            if (string.IsNullOrEmpty(barcode) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Mã vạch và tên sản phẩm không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ và không âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStockQuantity.Text.Trim(), out int stock) || stock < 0)
            {
                MessageBox.Show("Số lượng tồn kho phải là số nguyên không âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int catId = cboCategory.SelectedValue is int cId ? cId : 1;

            var newProduct = new ProductDto
            {
                Barcode = barcode,
                ProductName = name,
                Price = price,
                StockQuantity = stock,
                CategoryId = catId
            };

            try
            {
                using var client = GetAuthenticatedClient();
                var response = await client.PostAsJsonAsync("products", newProduct);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    await LoadDataAsync();
                }
                else
                {
                    string err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Thêm thất bại: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsCashier())
            {
                MessageBox.Show("Nhân viên thu ngân không có quyền sửa sản phẩm!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text.Trim(), out int id))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string barcode = txtBarcode.Text.Trim();
            string name = txtProductName.Text.Trim();

            if (string.IsNullOrEmpty(barcode) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Mã vạch và tên sản phẩm không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ và không âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStockQuantity.Text.Trim(), out int stock) || stock < 0)
            {
                MessageBox.Show("Số lượng tồn kho phải là số nguyên không âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int catId = cboCategory.SelectedValue is int cId ? cId : 1;

            var updateData = new ProductDto
            {
                ProductId = id,
                Barcode = barcode,
                ProductName = name,
                Price = price,
                StockQuantity = stock,
                CategoryId = catId
            };

            try
            {
                using var client = GetAuthenticatedClient();
                var response = await client.PutAsJsonAsync($"products/{id}", updateData);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    await LoadDataAsync();
                }
                else
                {
                    string err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Cập nhật thất bại: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsCashier())
            {
                MessageBox.Show("Nhân viên thu ngân không có quyền xóa sản phẩm!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtId.Text.Trim(), out int id))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm [{txtProductName.Text}] (ID: {id}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using var client = GetAuthenticatedClient();
                var response = await client.DeleteAsync($"products/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputs();
                    await LoadDataAsync();
                }
                else
                {
                    string err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Xóa thất bại: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearInputs()
        {
            txtId.Clear();
            txtBarcode.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtStockQuantity.Clear();
            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;
        }
    }

    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
    }
}
