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
            try
            {
                var users = await ApiClientService.Client.GetFromJsonAsync<List<UserDto>>("users");
                dgvUsers.DataSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lấy danh sách tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
