using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Services;

namespace FloriSys._5_GiaoHang
{
    public partial class ucCapNhatGH : UserControl
    {
        private DataTable dtDonGiao;

        public ucCapNhatGH()
        {
            InitializeComponent();
            this.Load += ucCapNhatGH_Load;
            btnGiaoXong1.Click += btnGiaoXong1_Click;
            btnVang1.Click += btnVang1_Click;
            btnHoan1.Click += btnHoan1_Click;
            button1.Click += btnBatDau2_Click;
        }

        private void ucCapNhatGH_Load(object sender, EventArgs e)
        {
            lblSub.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                dtDonGiao = GiaoHangDAO.LayDonCuaShipper(SessionManager.MaNV);
                if (dtDonGiao == null || dtDonGiao.Rows.Count == 0)
                {
                    pnlDon1.Visible = false;
                    panel2.Visible = false;
                    return;
                }

                // Find the first DangGiao order for card 1
                DataRow dangGiao = null;
                DataRow choPhanCong = null;
                foreach (DataRow dr in dtDonGiao.Rows)
                {
                    string tt = dr["TrangThai"].ToString();
                    if (tt == "DangGiao" && dangGiao == null) dangGiao = dr;
                    if ((tt == "ChoPhanCong" || tt == "GiaoLai") && choPhanCong == null) choPhanCong = dr;
                }

                // Card 1 - Đơn đang giao (urgent)
                if (dangGiao != null)
                {
                    pnlDon1.Visible = true;
                    pnlDon1.Tag = dangGiao["MaGiaoHang"].ToString();
                    lblMaDon1.Text = dangGiao["MaDon"].ToString() + " – " + dangGiao["TenKH"].ToString();
                    lblInfo1.Text = "📍 " + (dangGiao["DiaChi"] != DBNull.Value ? dangGiao["DiaChi"].ToString() : "—") +
                                    "   📞 " + (dangGiao["SoDienThoai"] != DBNull.Value ? dangGiao["SoDienThoai"].ToString() : "—");
                    lblTien1.Text = string.Format("💰 {0:N0}đ – COD", dangGiao["TongTien"]);

                    string ghiChu = dangGiao["GhiChu"] != DBNull.Value ? dangGiao["GhiChu"].ToString() : "";
                    string ghiChuGH = dangGiao["GhiChuGiaoHang"] != DBNull.Value ? dangGiao["GhiChuGiaoHang"].ToString() : "";
                    label1.Text = ghiChu.Length > 0 ? "📝 " + ghiChu : "";
                    label2.Text = ghiChuGH.Length > 0 ? "🚚 " + ghiChuGH : "";
                }
                else
                {
                    pnlDon1.Visible = false;
                }

                // Card 2 - Đơn chờ giao tiếp theo
                if (choPhanCong != null)
                {
                    panel2.Visible = true;
                    panel2.Tag = choPhanCong["MaGiaoHang"].ToString();
                    label7.Text = choPhanCong["MaDon"].ToString() + " – " + choPhanCong["TenKH"].ToString();
                    label6.Text = "📍 " + (choPhanCong["DiaChi"] != DBNull.Value ? choPhanCong["DiaChi"].ToString() : "—");
                    label5.Text = "⏰ Chờ giao";
                    label4.Text = string.Format("💰 {0:N0}đ", choPhanCong["TongTien"]);
                    label3.Text = "📞 " + (choPhanCong["SoDienThoai"] != DBNull.Value ? choPhanCong["SoDienThoai"].ToString() : "—");
                }
                else
                {
                    panel2.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu giao hàng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGiaoXong1_Click(object sender, EventArgs e)
        {
            string maGH = pnlDon1.Tag?.ToString();
            if (string.IsNullOrEmpty(maGH)) return;

            if (MessageBox.Show("Xác nhận đơn này đã giao thành công?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    GiaoHangDAO.CapNhatTrangThai(maGH, "GiaoThanhCong");
                    MessageBox.Show("✅ Đã cập nhật giao thành công!", "Thông báo");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVang1_Click(object sender, EventArgs e)
        {
            string maGH = pnlDon1.Tag?.ToString();
            if (string.IsNullOrEmpty(maGH)) return;

            if (MessageBox.Show("Ghi nhận giao lại (khách vắng mặt)?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    GiaoHangDAO.CapNhatTrangThai(maGH, "GiaoLai", "Khách vắng mặt, hẹn giao lại");
                    MessageBox.Show("🔄 Đã ghi nhận giao lại.", "Thông báo");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHoan1_Click(object sender, EventArgs e)
        {
            string maGH = pnlDon1.Tag?.ToString();
            if (string.IsNullOrEmpty(maGH)) return;

            if (MessageBox.Show("Xác nhận HOÀN HÀNG đơn này?\nHàng sẽ được ghi nhận trả về kho.", "Hoàn hàng",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    GiaoHangDAO.CapNhatTrangThai(maGH, "HoanHang", "Shipper hoàn hàng");
                    MessageBox.Show("↩️ Đã hoàn hàng.", "Thông báo");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBatDau2_Click(object sender, EventArgs e)
        {
            string maGH = panel2.Tag?.ToString();
            if (string.IsNullOrEmpty(maGH)) return;

            if (MessageBox.Show("Bắt đầu giao đơn này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    GiaoHangDAO.CapNhatTrangThai(maGH, "DangGiao");
                    MessageBox.Show("🛵 Đã bắt đầu giao!", "Thông báo");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblTien1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void pnlDon1_Paint(object sender, PaintEventArgs e) { }
    }
}
