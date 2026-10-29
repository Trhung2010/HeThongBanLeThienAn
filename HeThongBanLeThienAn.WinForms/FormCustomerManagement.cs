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
        }

        private void FormatDataGridView()
        {
            if (dgvCustomers.Columns.Count == 0)
                return;

            dgvCustomers.AutoGenerateColumns = true;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

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

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dgvCustomers.Rows[e.RowIndex];

            if (row.Cells["CustomerId"].Value != null)
                txtCustomerId.Text = row.Cells["CustomerId"].Value.ToString();

            if (row.Cells["CustomerName"].Value != null)
                txtCustomerName.Text = row.Cells["CustomerName"].Value.ToString();

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
        }

        private int ParseRewardPoints()
        {
            return int.TryParse(txtRewardPoints.Text.Trim(), out int pts) && pts >= 0 ? pts : 0;
        }
    }

}
