using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._3_BanHang
{
    public partial class ucDashboardBanHang : UserControl
    {
        private string currentUserMaNV = FloriSys.Services.SessionManager.MaNV;

        public ucDashboardBanHang()
        {
            InitializeComponent();
        }

        private void ucDashboardBanHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            LoadStats();
            LoadDonHang();
            LoadLookup("");
        }

        private void LoadStats()
        {
            ThongKeBanHang stats = BaoCaoDAO.ThongKeBanHang(currentUserMaNV);
            if (stats != null)
            {
                lblValDonToi.Text = stats.DonHomNay.ToString();
                
                decimal doanhThu = stats.DoanhThuHomNay;
                if (doanhThu >= 1000000)
                    lblValDoanhThu.Text = (doanhThu / 1000000).ToString("N1") + "M";
                else
                    lblValDoanhThu.Text = doanhThu.ToString("N0") + "đ";

                lblValDangXuLy.Text = stats.DonDangXuLy.ToString();
                lblValHoanThanh.Text = stats.DonHoanThanh.ToString();
            }
        }

        private void LoadDonHang()
        {
            List<DonHangGanDay> dsDH = BaoCaoDAO.DonHangCuaNV(currentUserMaNV);
            dgvDonGanDay.DataSource = dsDH;
            if (dgvDonGanDay.Columns.Count > 0)
            {
                dgvDonGanDay.Columns["MaDon"].HeaderText = "Mã đơn";
                dgvDonGanDay.Columns["TenKH"].HeaderText = "Khách hàng";
                dgvDonGanDay.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvDonGanDay.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvDonGanDay.Columns["NgayTao"].HeaderText = "Ngày tạo";
                dgvDonGanDay.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvDonGanDay.Columns["TrangThai"].HeaderText = "Trạng thái";
            }
        }

        private void LoadLookup(string keyword)
        {
            List<SanPham> dsSP = SanPhamDAO.LaySanPhamDangBan(keyword);
            dgvLookup.DataSource = dsSP;
            if (dgvLookup.Columns.Count > 0)
            {
                dgvLookup.Columns["MaSP"].Visible = false;
                dgvLookup.Columns["LoaiHoa"].Visible = false;
                dgvLookup.Columns["TenSP"].HeaderText = "Sản phẩm";
                dgvLookup.Columns["GiaBan"].HeaderText = "Giá bán";
                dgvLookup.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                dgvLookup.Columns["SoLuongTon"].HeaderText = "Tồn";
            }
        }

        private void btnTimLookup_Click(object sender, EventArgs e)
        {
            LoadLookup(txtLookup.Text);
        }
    }
}
