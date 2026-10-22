using System;
using System.Drawing;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public static class NavigationManager
    {
        public static MenuStrip? CreateAppMenu(Form currentForm)
        {
            if (!currentForm.TopLevel)
                return null; // Không hiển thị menu phụ khi form đang được nhúng trong FormMainShell

            var menu = new MenuStrip
            {
                BackColor = Color.FromArgb(240, 244, 250),
                Font = new Font("Segoe UI", 9.5f),
                Dock = DockStyle.Top
            };

            var mnuProduct = new ToolStripMenuItem("📦 Quản lý Sản phẩm (Product)");
            mnuProduct.Click += (s, e) => SwitchForm(currentForm, typeof(FormProductManagement));

            var mnuCategory = new ToolStripMenuItem("🏷️ Quản lý Danh mục (Cate)");
            mnuCategory.Click += (s, e) => SwitchForm(currentForm, typeof(FormCategoryManagement));

            var mnuCustomer = new ToolStripMenuItem("👥 Quản lý Khách hàng (Customer)");
            mnuCustomer.Click += (s, e) => SwitchForm(currentForm, typeof(FormCustomerManagement));

            string roleText = string.IsNullOrEmpty(SessionManager.CurrentRole) ? "Khách" : SessionManager.CurrentRole;
            var mnuUser = new ToolStripMenuItem($"👤 Vai trò: {roleText}")
            {
                Enabled = false,
                Alignment = ToolStripItemAlignment.Right,
                ForeColor = Color.DarkSlateBlue
            };

            var mnuLogout = new ToolStripMenuItem("🚪 Đăng xuất")
            {
                Alignment = ToolStripItemAlignment.Right
            };
            mnuLogout.Click += (s, e) => Logout(currentForm);

            // Làm nổi bật menu của trang hiện tại
            if (currentForm is FormProductManagement)
            {
                mnuProduct.Font = new Font(mnuProduct.Font, FontStyle.Bold);
                mnuProduct.BackColor = Color.FromArgb(200, 220, 245);
            }
            else if (currentForm is FormCategoryManagement)
            {
                mnuCategory.Font = new Font(mnuCategory.Font, FontStyle.Bold);
                mnuCategory.BackColor = Color.FromArgb(200, 220, 245);
            }
            else if (currentForm is FormCustomerManagement)
            {
                mnuCustomer.Font = new Font(mnuCustomer.Font, FontStyle.Bold);
                mnuCustomer.BackColor = Color.FromArgb(200, 220, 245);
            }

            // ── PHÂN QUYỀN MENU THEO ROLE ──────────────────────────────────────
            // Admin     : Sản phẩm | Danh mục | Khách hàng
            // Cashier   : Khách hàng (chỉ xem + tích điểm, không quản lý kho)
            // Warehouse : Sản phẩm | Danh mục (không tiếp xúc khách hàng)
            string role = SessionManager.CurrentRole?.ToUpper() ?? string.Empty;

            bool canSeeProduct  = role == "ADMIN" || role == "WAREHOUSE";
            bool canSeeCategory = role == "ADMIN" || role == "WAREHOUSE";
            bool canSeeCustomer = role == "ADMIN" || role == "CASHIER";

            if (canSeeProduct)  menu.Items.Add(mnuProduct);
            if (canSeeCategory) menu.Items.Add(mnuCategory);
            if (canSeeCustomer) menu.Items.Add(mnuCustomer);

            menu.Items.Add(mnuLogout);
            menu.Items.Add(mnuUser);

            return menu;
        }

        public static void SwitchForm(Form currentForm, Type targetFormType)
        {
            if (currentForm.GetType() == targetFormType)
                return;

            Form? nextForm = Activator.CreateInstance(targetFormType) as Form;
            if (nextForm != null)
            {
                nextForm.StartPosition = FormStartPosition.CenterScreen;
                Program.AppContext.MainForm = nextForm;
                nextForm.Show();
                currentForm.Close();
            }
        }

        public static void Logout(Form currentForm)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                SessionManager.JwtToken = string.Empty;
                SessionManager.CurrentRole = string.Empty;

                var loginForm = new FormLogin();
                loginForm.StartPosition = FormStartPosition.CenterScreen;
                Program.AppContext.MainForm = loginForm;
                loginForm.Show();
                currentForm.Close();
            }
        }
    }
}
