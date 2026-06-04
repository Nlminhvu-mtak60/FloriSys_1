using System;
using System.Drawing;
using System.Windows.Forms;

namespace FloriSys._5_GiaoHang
{
    // NOTE: Form này hiện chưa được triển khai đầy đủ.
    // Chức năng cập nhật giao hàng inline đang được xây dựng.
    // Shipper cập nhật trạng thái thông qua ucDashboardShipper.
    public partial class ucCapNhatGH : UserControl
    {
        public ucCapNhatGH()
        {
            InitializeComponent();
            this.Load += ucCapNhatGH_Load;
        }

        private void ucCapNhatGH_Load(object sender, EventArgs e)
        {
            // FIX: Thay vì màn trắng hoàn toàn, hiển thị thông báo rõ ràng cho user
            ShowPlaceholder();
        }

        private void ShowPlaceholder()
        {
            Label lbl = new Label
            {
                Text = "ℹ️  Chức năng này đang được phát triển.\n\nVui lòng sử dụng màn hình Danh Sách Giao Hàng\nhoặc Dashboard Shipper để cập nhật trạng thái.",
                Font = new Font("Segoe UI", 12F),
                ForeColor = Color.FromArgb(75, 85, 99),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(lbl);
        }

        private void lblTien1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void pnlDon1_Paint(object sender, PaintEventArgs e) { }
    }
}