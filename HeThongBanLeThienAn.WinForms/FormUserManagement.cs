using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormUserManagement : Form
    {
        public FormUserManagement()
        {
            InitializeComponent();
            cboRole.Items.AddRange(new string[] { "Admin", "Cashier", "Warehouse" });
            cboRole.SelectedIndex = 1;
        }
        private const int PageSize = 5;
        private int _currentPage = 1;
        private int _totalPages = 1;
        private bool _isLoading = false;

        private async void FormUserManagement_Load(object sender, EventArgs e)
        {
            if (!string.Equals(SessionManager.CurrentRole, "Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Chỉ Quản trị viên (Admin) mới có quyền truy cập màn hình này!", "Từ chối", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                btnAddUser.Enabled = false;
                return;
            }

            SetupGrid();
            await LoadUsersAsync();
        }

        private void SetupGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.Columns.Clear();

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Id",
                HeaderText = "Mã ID",
                Width = 70
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Tên Đăng Nhập",
                Width = 140
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Họ Và Tên",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Vai Trò (Role)",
                Width = 110
            });

            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsActive",
                HeaderText = "Kích Hoạt",
                Width = 90
            });
        }

        private async Task LoadUsersAsync()
        {
            if (_isLoading) return;
            _isLoading = true;
            SetPagingControlsEnabled(false);

            try
            {
                string url = $"users/paged?pageNumber={_currentPage}&pageSize={PageSize}";
                var result = await ApiClientService.Client.GetFromJsonAsync<PagedViewResult<UserDto>>(url);
                if (result != null)
                {
                    _currentPage = Math.Max(result.CurrentPage, 1);
                    _totalPages = result.TotalPages;
                    dgvUsers.DataSource = result.Items;
                    lblPaging.Text = result.TotalPages == 0
                        ? "Trang 0 / 0 (Tổng: 0 tài khoản)"
                        : $"Trang {_currentPage} / {_totalPages} (Tổng: {result.TotalItems} tài khoản)";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btnReload.Enabled = enabled;
        }

        private async void btnPrev_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadUsersAsync();
            }
        }

        private async void btnNext_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadUsersAsync();
            }
        }

        private async void btnAddUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string fullName = txtFullName.Text.Trim();
            string role = cboRole.SelectedItem?.ToString() ?? "Cashier";

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Tên đăng nhập và mật khẩu không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newUser = new
            {
                Username = username,
                Password = password,
                FullName = string.IsNullOrWhiteSpace(fullName) ? username : fullName,
                Role = role
            };

            try
            {
                var res = await ApiClientService.Client.PostAsJsonAsync("users", newUser);

                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("Tạo tài khoản mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersAsync();
                    txtUsername.Clear();
                    txtPassword.Clear();
                    txtFullName.Clear();
                }
                else
                {
                    string err = await res.Content.ReadAsStringAsync();
                    MessageBox.Show("Không thể tạo tài khoản: " + err, "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReload_Click(object sender, EventArgs e)
        {
            _currentPage = 1;
            await LoadUsersAsync();
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
