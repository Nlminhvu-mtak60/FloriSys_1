using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._5_GiaoHang
{
    public partial class ucDashboardShipper : UserControl
    {
        private string currentUserMaNV = FloriSys.Services.SessionManager.MaNV;
        private string currentMaGH = "";

        public ucDashboardShipper()
        {
            InitializeComponent();
        }

        private void ucDashboardShipper_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            LoadStats();
            LoadList();
            LoadCurrentDelivery();
        }

        private void LoadStats()
        {
            DataTable dt = GiaoHangDAO.ThongKeShipper(currentUserMaNV);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                lblValTongDon.Text = dr["TongDonHnay"].ToString();
                lblValDaGiao.Text = dr["DaGiaoHnay"].ToString();
                lblValDangGiao.Text = dr["DangDiGiao"].ToString();
                lblValChuaGiao.Text = dr["ChuaGiao"].ToString();
            }
        }

        private void LoadList()
        {
            dgvAllDon.DataSource = GiaoHangDAO.LayDonCuaShipper(currentUserMaNV);
            if (dgvAllDon.Columns.Count > 0)
            {
                dgvAllDon.Columns["MaGiaoHang"].Visible = false;
                dgvAllDon.Columns["MaDon"].HeaderText = "Mã đơn";
                dgvAllDon.Columns["TenKH"].HeaderText = "Khách hàng";
                dgvAllDon.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvAllDon.Columns["SoDienThoai"].HeaderText = "SĐT";
                dgvAllDon.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvAllDon.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvAllDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            }
        }

        private void LoadCurrentDelivery()
        {
            DataTable dt = dgvAllDon.DataSource as DataTable;
            DataRow current = null;
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["TrangThai"].ToString() == "DangGiao")
                    {
                        current = dr;
                        break;
                    }
                }
            }

            if (current != null)
            {
                pnlCurrent.Visible = true;
                currentMaGH = current["MaGiaoHang"].ToString();
                lblCurTitle.Text = "🔴 Đơn đang giao – " + current["MaDon"].ToString();
                lblCurCustomer.Text = "Khách hàng: " + current["TenKH"].ToString();
                lblCurPhone.Text = "📞 SĐT: " + current["SoDienThoai"].ToString();
                lblCurAddress.Text = "📍 Địa chỉ: " + current["DiaChi"].ToString();
            }
            else
            {
                pnlCurrent.Visible = false;
            }
        }

        private void btnThanhCong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            GiaoHangDAO.CapNhatTrangThai(currentMaGH, "DaGiao");
            MessageBox.Show("Đã cập nhật giao hàng thành công!");
            LoadData();
        }

        private void btnKhachVang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            GiaoHangDAO.CapNhatTrangThai(currentMaGH, "KhachVangMat");
            MessageBox.Show("Ghi nhận khách vắng mặt.");
            LoadData();
        }

        private void btnHoanHang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            GiaoHangDAO.CapNhatTrangThai(currentMaGH, "HoanHang");
            MessageBox.Show("Đã ghi nhận hoàn hàng.");
            LoadData();
        }

        private void dgvAllDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string status = dgvAllDon.Rows[e.RowIndex].Cells["TrangThai"].Value.ToString();
                if (status == "DangChuanBi" || status == "ChoGiao")
                {
                    if (MessageBox.Show("Bạn muốn bắt đầu giao đơn này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        string maGH = dgvAllDon.Rows[e.RowIndex].Cells["MaGiaoHang"].Value.ToString();
                        GiaoHangDAO.CapNhatTrangThai(maGH, "DangGiao");
                        LoadData();
                    }
                }
            }
        }
    }
}
