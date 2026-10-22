using System.Net.Http.Json;

namespace MiniSupermarket.WinForms
{
    public partial class FormProductManagement : Form
    {
        private const int PageSize = 5;
        private readonly List<CategoryDto> _categoryList = new();
        private int _currentPage = 1;
        private int _totalPages;
        private bool _isLoading;
        private bool _isInitializing = true;

        public FormProductManagement() => InitializeComponent();

        private async void FormProductManagement_Load(object sender, EventArgs e)
        {
            var menu = NavigationManager.CreateAppMenu(this);
            if (menu != null)
            {
                Controls.Add(menu);
                MainMenuStrip = menu;
            }

            await LoadCategoriesAsync();
            _isInitializing = false;
            await LoadProductsPagedAsync();
            UpdatePermissionButtons();
        }

        private bool IsCashier() =>
            string.Equals(SessionManager.CurrentRole, "Cashier", StringComparison.OrdinalIgnoreCase);

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
                var categories = await ApiClientService.Client.GetFromJsonAsync<List<CategoryDto>>("categories");
                _categoryList.Clear();
                if (categories != null)
                {
                    _categoryList.AddRange(categories);
                }

                cboCategory.DataSource = new BindingSource(_categoryList, null);
                cboCategory.DisplayMember = nameof(CategoryDto.CategoryName);
                cboCategory.ValueMember = nameof(CategoryDto.CategoryId);

                var filterCategories = new List<CategoryDto>
                {
                    new() { CategoryId = 0, CategoryName = "Tất cả nhóm hàng" }
                };
                filterCategories.AddRange(_categoryList);
                cboCategoryFilter.DataSource = filterCategories;
                cboCategoryFilter.DisplayMember = nameof(CategoryDto.CategoryName);
                cboCategoryFilter.ValueMember = nameof(CategoryDto.CategoryId);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không tải được danh mục:\n" + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadProductsPagedAsync()
        {
            if (_isLoading)
            {
                return;
            }

            _isLoading = true;
            SetPagingControlsEnabled(false);

            try
            {
                string keyword = Uri.EscapeDataString(txtKeyword.Text.Trim());
                int categoryId = cboCategoryFilter.SelectedValue is int selected ? selected : 0;
                string url = $"products/paged?keyword={keyword}&categoryId={categoryId}" +
                    $"&pageNumber={_currentPage}&pageSize={PageSize}";

                var result = await ApiClientService.Client.GetFromJsonAsync<PagedViewResult<ProductItemViewDto>>(url);
                if (result == null)
                {
                    throw new InvalidOperationException("Máy chủ trả về dữ liệu phân trang rỗng.");
                }

                if (result.Items.Count == 0 && result.TotalItems > 0 && _currentPage > 1)
                {
                    _currentPage = Math.Max(result.TotalPages, 1);
                    _isLoading = false;
                    await LoadProductsPagedAsync();
                    return;
                }

                _currentPage = result.CurrentPage;
                _totalPages = result.TotalPages;
                dgvProducts.DataSource = result.Items;
                FormatDataGridView();
                lblPaging.Text = result.TotalPages == 0
                    ? "Trang 0 / 0 (Tổng: 0 SP)"
                    : $"Trang {_currentPage} / {_totalPages} (Tổng: {result.TotalItems} SP)";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối Server khi tải sản phẩm:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            cboCategoryFilter.Enabled = enabled;
        }

        private void FormatDataGridView()
        {
            if (dgvProducts.Columns.Count == 0)
            {
                return;
            }

            dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            SetColumn("ProductId", "Mã SP", 65);
            SetColumn("Barcode", "Mã vạch", 110);
            SetColumn("ProductName", "Tên sản phẩm", 190);
            SetColumn("Price", "Đơn giá (đ)", 95, "N0");
            SetColumn("StockQuantity", "Tồn kho", 70);
            SetColumn("CategoryId", "Mã nhóm", 70);
            SetColumn("CategoryName", "Nhóm hàng", 170);
        }

        private void SetColumn(string name, string header, int width, string? format = null)
        {
            if (dgvProducts.Columns[name] is not DataGridViewColumn column)
            {
                return;
            }

            column.HeaderText = header;
            column.Width = width;
            if (format != null)
            {
                column.DefaultCellStyle.Format = format;
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvProducts.Rows[e.RowIndex];
            txtId.Text = row.Cells["ProductId"]?.Value?.ToString() ?? string.Empty;
            txtBarcode.Text = row.Cells["Barcode"]?.Value?.ToString() ?? string.Empty;
            txtProductName.Text = row.Cells["ProductName"]?.Value?.ToString() ?? string.Empty;
            txtPrice.Text = row.Cells["Price"]?.Value?.ToString() ?? string.Empty;
            txtStockQuantity.Text = row.Cells["StockQuantity"]?.Value?.ToString() ?? string.Empty;

            if (int.TryParse(row.Cells["CategoryId"]?.Value?.ToString(), out int categoryId))
            {
                cboCategory.SelectedValue = categoryId;
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            cboCategoryFilter.SelectedValue = 0;
            _currentPage = 1;
            ClearInputs();
            await LoadProductsPagedAsync();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        private async void cboCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing)
            {
                return;
            }

            _currentPage = 1;
            await LoadProductsPagedAsync();
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadProductsPagedAsync();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadProductsPagedAsync();
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsCashier())
            {
                MessageBox.Show("Nhân viên thu ngân không có quyền thêm sản phẩm!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TryReadProduct(out ProductDto product))
            {
                return;
            }

            await SaveProductAsync(() => ApiClientService.Client.PostAsJsonAsync("products", product), "Thêm sản phẩm thành công!");
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

            if (!TryReadProduct(out ProductDto product))
            {
                return;
            }

            product.ProductId = id;
            await SaveProductAsync(() => ApiClientService.Client.PutAsJsonAsync($"products/{id}", product), "Cập nhật sản phẩm thành công!");
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

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm [{txtProductName.Text}] (ID: {id}) không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            await SaveProductAsync(() => ApiClientService.Client.DeleteAsync($"products/{id}"), "Đã xóa sản phẩm thành công!");
        }

        private bool TryReadProduct(out ProductDto product)
        {
            product = new ProductDto();
            string barcode = txtBarcode.Text.Trim();
            string name = txtProductName.Text.Trim();

            if (string.IsNullOrEmpty(barcode) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Mã vạch và tên sản phẩm không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out decimal price) || price < 0)
            {
                MessageBox.Show("Đơn giá phải là số hợp lệ và không âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtStockQuantity.Text.Trim(), out int stock) || stock < 0)
            {
                MessageBox.Show("Số lượng tồn kho phải là số nguyên không âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboCategory.SelectedValue is not int categoryId || categoryId <= 0)
            {
                MessageBox.Show("Vui lòng chọn nhóm hàng hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            product = new ProductDto
            {
                Barcode = barcode,
                ProductName = name,
                Price = price,
                StockQuantity = stock,
                CategoryId = categoryId
            };
            return true;
        }

        private async Task SaveProductAsync(Func<Task<HttpResponseMessage>> request, string successMessage)
        {
            try
            {
                using HttpResponseMessage response = await request();
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Thao tác thất bại ({(int)response.StatusCode}): {error}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(successMessage, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                await LoadProductsPagedAsync();
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
            {
                cboCategory.SelectedIndex = 0;
            }
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

    public sealed class ProductItemViewDto : ProductDto
    {
        public string CategoryName { get; set; } = string.Empty;
    }

    public sealed class PagedViewResult<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new();
    }
}
