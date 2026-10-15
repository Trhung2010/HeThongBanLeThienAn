using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MiniSupermarket.WinForms
{
    public partial class FormPOS : Form
    {
        private readonly List<CartItemDto> _cart = new List<CartItemDto>();

        public FormPOS()
        {
            InitializeComponent();
            SetupCartGrid();
        }

        private void SetupCartGrid()
        {
            dgvCart.AutoGenerateColumns = false;
            dgvCart.Columns.Clear();

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductId",
                HeaderText = "Mã SP",
                Width = 70
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Barcode",
                HeaderText = "Mã Vạch",
                Width = 110
            });

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên Sản Phẩm",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            var colUnitPrice = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Đơn Giá",
                Width = 100
            };
            colUnitPrice.DefaultCellStyle.Format = "N0";
            dgvCart.Columns.Add(colUnitPrice);

            dgvCart.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "SL",
                Width = 60
            });

            var colTotal = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPrice",
                HeaderText = "Thành Tiền",
                Width = 120
            };
            colTotal.DefaultCellStyle.Format = "N0";
            dgvCart.Columns.Add(colTotal);
        }

        // Bắt sự kiện quét mã Barcode (Nhấn Enter)
        private async void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txtBarcode.Text))
            {
                string barcode = txtBarcode.Text.Trim();
                txtBarcode.Clear();
                await AddProductToCartByBarcodeAsync(barcode);
            }
        }

        private async Task AddProductToCartByBarcodeAsync(string barcode)
        {
            try
            {
                // Gọi API tra cứu sản phẩm theo Barcode
                var product = await ApiClientService.Client.GetFromJsonAsync<ProductDto>($"products/barcode/{barcode}");

                if (product == null)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm có mã vạch này!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var existingItem = _cart.FirstOrDefault(c => c.ProductId == product.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    _cart.Add(new CartItemDto
                    {
                        ProductId = product.ProductId,
                        Barcode = product.Barcode,
                        ProductName = product.ProductName,
                        UnitPrice = product.Price,
                        Quantity = 1
                    });
                }

                UpdateCartDisplay();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tra cứu sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCartDisplay()
        {
            dgvCart.DataSource = null;
            dgvCart.DataSource = _cart.ToList();

            decimal total = _cart.Sum(x => x.TotalPrice);
            lblTotalAmount.Text = $"{total:N0} đ";

            CalculateChange();
        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void CalculateChange()
        {
            decimal total = _cart.Sum(x => x.TotalPrice);

            if (decimal.TryParse(txtCashReceived.Text.Replace(",", "").Replace(".", "").Trim(), out decimal cashReceived))
            {
                decimal change = cashReceived - total;
                if (change >= 0)
                {
                    lblChange.Text = $"{change:N0} đ";
                    lblChange.ForeColor = Color.ForestGreen;
                }
                else
                {
                    lblChange.Text = "Chưa đủ tiền!";
                    lblChange.ForeColor = Color.Red;
                }
            }
            else
            {
                lblChange.Text = "0 đ";
                lblChange.ForeColor = Color.ForestGreen;
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0) return;

            var confirm = MessageBox.Show("Bạn có chắc muốn hủy toàn bộ giỏ hàng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                _cart.Clear();
                UpdateCartDisplay();
                txtCashReceived.Clear();
            }
        }

        private async void btnLookupCustomer_Click(object sender, EventArgs e)
        {
            await LookupCustomerAsync();
        }

        private async void txtCustomerPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                await LookupCustomerAsync();
            }
        }

        private async Task LookupCustomerAsync()
        {
            string phone = txtCustomerPhone.Text.Trim();
            if (string.IsNullOrEmpty(phone))
            {
                lblCustomerName.Text = "Khách hàng: Vãng lai";
                return;
            }

            try
            {
                var customers = await ApiClientService.Client.GetFromJsonAsync<List<CustomerDto>>($"customers/search?keyword={Uri.EscapeDataString(phone)}");
                if (customers != null && customers.Any())
                {
                    var cust = customers.First();
                    lblCustomerName.Text = $"Khách: {cust.CustomerName} ({cust.MembershipRank}) - {cust.RewardPoints}đ";
                }
                else
                {
                    lblCustomerName.Text = "SĐT chưa đăng ký (Khách mới)";
                }
            }
            catch
            {
                lblCustomerName.Text = "Không tra cứu được khách hàng";
            }
        }

        private async void btnCheckout_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Giỏ hàng đang trống! Vui lòng quét sản phẩm trước khi thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = _cart.Sum(x => x.TotalPrice);
            if (decimal.TryParse(txtCashReceived.Text.Replace(",", "").Replace(".", "").Trim(), out decimal cashReceived))
            {
                if (cashReceived < total)
                {
                    MessageBox.Show("Tiền khách đưa chưa đủ để thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var orderRequest = new
            {
                CashierUsername = SessionManager.CurrentUsername,
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                Items = _cart.Select(i => new
                {
                    i.ProductId,
                    i.Quantity,
                    i.UnitPrice
                }).ToList()
            };

            try
            {
                var response = await ApiClientService.Client.PostAsJsonAsync("orders/checkout", orderRequest);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show(
                        $"Thanh toán thành công!\nTổng tiền: {total:N0} đ\nĐã xuất hóa đơn và cập nhật kho hàng.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    _cart.Clear();
                    UpdateCartDisplay();
                    txtCashReceived.Clear();
                    txtCustomerPhone.Clear();
                    lblCustomerName.Text = "Khách hàng: Vãng lai";
                    txtBarcode.Focus();
                }
                else
                {
                    string err = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Lỗi khi thanh toán từ Server: " + err, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối máy chủ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                btnCheckout_Click(sender, e);
            }
        }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
    }
}
