using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    // =============================================================
    // FORM QUẢN LÝ KHÁCH HÀNG (CUSTOMERS)
    // =============================================================
    public partial class FormCustomerManagement : Form
    {
        public FormCustomerManagement()
        {
            InitializeComponent();
        }
        private const int PageSize = 5;
        private int _currentPage = 1;
        private int _totalPages = 1;
        private bool _isLoading = false;

        private async void FormCustomerManagement_Load(object sender, EventArgs e)
        {
            var menu = NavigationManager.CreateAppMenu(this);
            if (menu != null)
            {
                this.Controls.Add(menu);
                this.MainMenuStrip = menu;
            }

            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            if (_isLoading) return;
            _isLoading = true;
            SetPagingControlsEnabled(false);

            try
            {
                var client = ApiClientService.Client;
                string keyword = Uri.EscapeDataString(txtKeyword.Text.Trim());
                string url = $"customers/paged?keyword={keyword}&pageNumber={_currentPage}&pageSize={PageSize}";

                var result = await client.GetFromJsonAsync<PagedViewResult<CustomerDto>>(url);
                if (result != null)
                {
                    _currentPage = Math.Max(result.CurrentPage, 1);
                    _totalPages = result.TotalPages;
                    dgvCustomers.DataSource = result.Items;
                    FormatDataGridView();
                    lblPaging.Text = result.TotalPages == 0
                        ? "Trang 0 / 0 (Tổng: 0 KH)"
                        : $"Trang {_currentPage} / {_totalPages} (Tổng: {result.TotalItems} KH)";
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
            // Cashier và Admin đều được xem/thêm/sửa khách hàng. Chỉ Admin được xóa.
            btnDelete.Enabled = !IsCashier();
            txtRewardPoints.ReadOnly = IsCashier();
            cboMembershipRank.Enabled = !IsCashier();
        }

        private void FormatDataGridView()
        {
            if (dgvCustomers.Columns.Count == 0)
                return;

            dgvCustomers.AutoGenerateColumns = true;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvCustomers.Columns["CustomerId"] != null)
            {
                dgvCustomers.Columns["CustomerId"].HeaderText = "Mã KH";
                dgvCustomers.Columns["CustomerId"].Width = 65;
            }

            if (dgvCustomers.Columns["CustomerName"] != null)
            {
                dgvCustomers.Columns["CustomerName"].HeaderText = "Tên khách hàng";
                dgvCustomers.Columns["CustomerName"].Width = 180;
            }

            if (dgvCustomers.Columns["PhoneNumber"] != null)
            {
                dgvCustomers.Columns["PhoneNumber"].HeaderText = "Số điện thoại";
                dgvCustomers.Columns["PhoneNumber"].Width = 120;
            }

            if (dgvCustomers.Columns["Address"] != null)
            {
                dgvCustomers.Columns["Address"].HeaderText = "Địa chỉ";
                dgvCustomers.Columns["Address"].Width = 200;
            }

            if (dgvCustomers.Columns["RewardPoints"] != null)
            {
                dgvCustomers.Columns["RewardPoints"].HeaderText = "Điểm tích lũy";
                dgvCustomers.Columns["RewardPoints"].Width = 100;
            }

            if (dgvCustomers.Columns["MembershipRank"] != null)
            {
                dgvCustomers.Columns["MembershipRank"].HeaderText = "Hạng thẻ";
                dgvCustomers.Columns["MembershipRank"].Width = 90;
            }

            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.ReadOnly = true;
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            ClearInputs();
            _currentPage = 1;
            await LoadDataAsync();
        }

        private async void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];

            int customerId = 0;
            string customerName = string.Empty;

            if (row.Cells["CustomerId"].Value != null)
            {
                txtCustomerId.Text = row.Cells["CustomerId"].Value.ToString();
                int.TryParse(txtCustomerId.Text, out customerId);
            }

            if (row.Cells["CustomerName"].Value != null)
            {
                txtCustomerName.Text = row.Cells["CustomerName"].Value.ToString();
                customerName = txtCustomerName.Text;
            }

            if (row.Cells["PhoneNumber"].Value != null)
                txtPhoneNumber.Text = row.Cells["PhoneNumber"].Value.ToString();

            if (row.Cells["Address"].Value != null)
                txtAddress.Text = row.Cells["Address"].Value.ToString();
            else
                txtAddress.Clear();

            if (row.Cells["RewardPoints"].Value != null)
                txtRewardPoints.Text = row.Cells["RewardPoints"].Value.ToString();

            if (row.Cells["MembershipRank"].Value != null)
                cboMembershipRank.SelectedItem = row.Cells["MembershipRank"].Value.ToString();

            if (customerId > 0)
            {
                await LoadCustomerOrdersAsync(customerId, customerName);
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCustomerName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhoneNumber.Focus();
                return;
            }

            try
            {
                var client = ApiClientService.Client;
                var newCustomer = new CustomerUpsertRequestDto
                {
                    CustomerName = txtCustomerName.Text.Trim(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    RewardPoints = ParseRewardPoints(),
                    MembershipRank = cboMembershipRank.SelectedItem?.ToString() ?? "Chuẩn"
                };

                var response = await client.PostAsJsonAsync("customers", newCustomer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Thêm mới khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Thêm mới thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerId.Text) || !int.TryParse(txtCustomerId.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCustomerName.Text) || string.IsNullOrWhiteSpace(txtPhoneNumber.Text))
            {
                MessageBox.Show("Tên và số điện thoại không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var client = ApiClientService.Client;
                var updateCustomer = new CustomerUpsertRequestDto
                {
                    CustomerName = txtCustomerName.Text.Trim(),
                    PhoneNumber = txtPhoneNumber.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    RewardPoints = ParseRewardPoints(),
                    MembershipRank = cboMembershipRank.SelectedItem?.ToString() ?? "Chuẩn"
                };

                var response = await client.PutAsJsonAsync($"customers/{id}", updateCustomer);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsCashier())
            {
                MessageBox.Show("Tài khoản Cashier không có quyền xóa khách hàng.", "Quyền truy cập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCustomerId.Text) || !int.TryParse(txtCustomerId.Text, out int id))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng ID = {id}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                var client = ApiClientService.Client;
                var response = await client.DeleteAsync($"customers/{id}");

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Xóa khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadDataAsync();
        }

        private void ClearInputs()
        {
            txtCustomerId.Clear();
            txtCustomerName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtRewardPoints.Clear();
            if (cboMembershipRank.Items.Count > 0)
                cboMembershipRank.SelectedIndex = 0;

            dgvOrderHistory.DataSource = null;
            dgvOrderHistory.Visible = false;
            lblOrderHistoryInfo.Text = "← Chọn một khách hàng để xem lịch sử đơn hàng";
            lblOrderHistoryInfo.ForeColor = Color.Gray;
        }

        private async Task LoadCustomerOrdersAsync(int customerId, string customerName)
        {
            lblOrderHistoryInfo.Text = $"Đang tải lịch sử đơn hàng của: {customerName}...";
            lblOrderHistoryInfo.ForeColor = Color.DarkBlue;

            try
            {
                var client = ApiClientService.Client;
                string url = $"orders/paged?customerId={customerId}&pageNumber=1&pageSize=50";
                var result = await client.GetFromJsonAsync<PagedViewResult<OrderSummaryDto>>(url);

                if (result != null && result.Items.Count > 0)
                {
                    dgvOrderHistory.DataSource = result.Items;
                    FormatOrderHistoryGrid();
                    dgvOrderHistory.Visible = true;
                    lblOrderHistoryInfo.Text = $"Lịch sử {result.TotalItems} đơn hàng của khách hàng: {customerName}";
                    lblOrderHistoryInfo.ForeColor = Color.DarkGreen;
                }
                else
                {
                    dgvOrderHistory.DataSource = null;
                    dgvOrderHistory.Visible = false;
                    lblOrderHistoryInfo.Text = $"Khách hàng \"{customerName}\" chưa có đơn hàng nào.";
                    lblOrderHistoryInfo.ForeColor = Color.DimGray;
                }
            }
            catch (Exception ex)
            {
                lblOrderHistoryInfo.Text = $"Lỗi khi tải lịch sử đơn hàng: {ex.Message}";
                lblOrderHistoryInfo.ForeColor = Color.Red;
            }
        }

        private void FormatOrderHistoryGrid()
        {
            if (dgvOrderHistory.Columns.Count == 0) return;

            dgvOrderHistory.AutoGenerateColumns = true;
            dgvOrderHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dgvOrderHistory.Columns["OrderId"] != null)
                dgvOrderHistory.Columns["OrderId"].Visible = false;

            if (dgvOrderHistory.Columns["CustomerId"] != null)
                dgvOrderHistory.Columns["CustomerId"].Visible = false;

            if (dgvOrderHistory.Columns["CustomerName"] != null)
                dgvOrderHistory.Columns["CustomerName"].Visible = false;

            if (dgvOrderHistory.Columns["OrderCode"] != null)
            {
                dgvOrderHistory.Columns["OrderCode"].HeaderText = "Mã HĐ";
                dgvOrderHistory.Columns["OrderCode"].Width = 130;
            }

            if (dgvOrderHistory.Columns["CreatedDate"] != null)
            {
                dgvOrderHistory.Columns["CreatedDate"].HeaderText = "Thời gian lập";
                dgvOrderHistory.Columns["CreatedDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvOrderHistory.Columns["CreatedDate"].Width = 140;
            }

            if (dgvOrderHistory.Columns["CashierUsername"] != null)
            {
                dgvOrderHistory.Columns["CashierUsername"].HeaderText = "Thu ngân";
                dgvOrderHistory.Columns["CashierUsername"].Width = 100;
            }

            if (dgvOrderHistory.Columns["TotalItems"] != null)
            {
                dgvOrderHistory.Columns["TotalItems"].HeaderText = "Tổng SL";
                dgvOrderHistory.Columns["TotalItems"].Width = 80;
                dgvOrderHistory.Columns["TotalItems"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgvOrderHistory.Columns["DiscountAmount"] != null)
            {
                dgvOrderHistory.Columns["DiscountAmount"].HeaderText = "Giảm giá (đ)";
                dgvOrderHistory.Columns["DiscountAmount"].DefaultCellStyle.Format = "N0";
                dgvOrderHistory.Columns["DiscountAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOrderHistory.Columns["DiscountAmount"].Width = 110;
            }

            if (dgvOrderHistory.Columns["FinalAmount"] != null)
            {
                dgvOrderHistory.Columns["FinalAmount"].HeaderText = "Tổng tiền (đ)";
                dgvOrderHistory.Columns["FinalAmount"].DefaultCellStyle.Format = "N0";
                dgvOrderHistory.Columns["FinalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOrderHistory.Columns["FinalAmount"].DefaultCellStyle.Font = new Font(dgvOrderHistory.Font, FontStyle.Bold);
                dgvOrderHistory.Columns["FinalAmount"].Width = 120;
            }

            if (dgvOrderHistory.Columns["RewardPoints"] != null)
            {
                dgvOrderHistory.Columns["RewardPoints"].HeaderText = "+ Điểm";
                dgvOrderHistory.Columns["RewardPoints"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOrderHistory.Columns["RewardPoints"].Width = 80;
            }

            if (dgvOrderHistory.Columns["RedeemedPoints"] != null)
            {
                dgvOrderHistory.Columns["RedeemedPoints"].HeaderText = "- Điểm dùng";
                dgvOrderHistory.Columns["RedeemedPoints"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvOrderHistory.Columns["RedeemedPoints"].Width = 90;
            }
        }

        private int ParseRewardPoints()
        {
            return int.TryParse(txtRewardPoints.Text.Trim(), out int pts) && pts >= 0 ? pts : 0;
        }
    }

}
