using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._3_BanHang
{
    public partial class ucDashboardBanHang : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();
        private string currentUserMaNV = FloriSys.Services.SessionManager.MaNV;

        public ucDashboardBanHang()
        {
            InitializeComponent();
        }

        private void ucDashboardBanHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void LoadData()
        {
            LoadStats();
            LoadDonHang();
            LoadLookup("");
        }

        private void LoadStats()
        {
            // 1. Thống kê chung hôm nay
            ThongKeBanHang stats = _bcRepo.ThongKeBanHang(currentUserMaNV);
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

            // 2. Hiệu suất tháng hiện tại
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            lblHieuSuatTitle.Text = $"📈 Hiệu suất tháng {currentMonth}/{currentYear}";

            List<HieuSuatNhanVien> dsHieuSuat = _bcRepo.HieuSuatNhanVien(currentMonth, currentYear);
            if (dsHieuSuat != null)
            {
                // Tìm nhân viên hiện tại trong danh sách để biết Xếp hạng
                var myStats = dsHieuSuat.Find(h => h.MaNV == currentUserMaNV);
                int myRank = dsHieuSuat.FindIndex(h => h.MaNV == currentUserMaNV) + 1;

                if (myStats != null)
                {
                    // Cập nhật Số đơn hàng (Target: 200 đơn)
                    int targetDon = 200;
                    lblTargetDon.Text = $"Số đơn hàng ({myStats.SoDonTao} / {targetDon})";
                    pbDonHang.Maximum = targetDon;
                    pbDonHang.Value = Math.Min(myStats.SoDonTao, targetDon);

                    // Cập nhật Doanh thu (Target: 50M)
                    decimal targetDT = 50000000;
                    string myDTStr = (myStats.TongDoanhThu / 1000000).ToString("N1") + "M";
                    string targetDTStr = (targetDT / 1000000).ToString("N0") + "M";
                    lblTargetDoanhThu.Text = $"Doanh thu ({myDTStr} / {targetDTStr})";
                    
                    pbDoanhThu.Maximum = 100; // Dùng phần trăm cho an toàn
                    int pct = (int)((myStats.TongDoanhThu / targetDT) * 100);
                    pbDoanhThu.Value = Math.Min(pct, 100);

                    // Cập nhật Xếp hạng
                    lblRank.Text = $"🥇 Xếp hạng: #{myRank} / {dsHieuSuat.Count} nv";
                }
            }
        }

        private void LoadDonHang()
        {
            List<DonHangGanDay> dsDH = _bcRepo.DonHangCuaNV(currentUserMaNV);
            dgvDonGanDay.DataSource = null;
            dgvDonGanDay.DataSource = dsDH;
            
            if (dgvDonGanDay.Columns.Count > 0)
            {
                var visibleCols = new List<string> { "MaDon", "TenKH", "TongTien", "NgayTao", "TrangThai" };
                foreach (DataGridViewColumn col in dgvDonGanDay.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

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
            List<SanPham> dsSP = _spRepo.LaySanPhamDangBan(keyword);
            dgvLookup.DataSource = null;
            dgvLookup.DataSource = dsSP;
            
            if (dgvLookup.Columns.Count > 0)
            {
                var visibleCols = new List<string> { "TenSP", "GiaBan", "SoLuongTon" };
                foreach (DataGridViewColumn col in dgvLookup.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

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
