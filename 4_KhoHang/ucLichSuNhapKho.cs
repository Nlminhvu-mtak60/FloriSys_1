using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._4_KhoHang
{
    public partial class ucLichSuNhapKho : BaseUserControl
    {
        private readonly NhanVienRepository _nvRepo = new NhanVienRepository();
        private readonly PhieuNhapKhoRepository _pnkRepo = new PhieuNhapKhoRepository();

        public ucLichSuNhapKho()
        {
            InitializeComponent();
        }

        private void ucLichSuNhapKho_Load(object sender, EventArgs e)
        {
            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = DateTime.Now;
            LoadNhanVien();
            LoadData();
        }

        private void LoadNhanVien()
        {
            List<NhanVien> dsNV = _nvRepo.LayDanhSach();
            dsNV.Insert(0, new NhanVien { MaNV = "", HoTen = "Tất cả nhân viên" });
            cboNhanVien.DataSource = dsNV;
            cboNhanVien.DisplayMember = "HoTen";
            cboNhanVien.ValueMember = "MaNV";
        }

        public override void LoadData()
        {
            string keyword = txtTimKiem.Text;
            string maNV = cboNhanVien.SelectedValue?.ToString();
            DateTime fromDate = dtpTuNgay.Value;
            DateTime toDate = dtpDenNgay.Value;

            List<PhieuNhapKho> dsPNK = _pnkRepo.LayDanhSach(keyword, maNV, fromDate, toDate);
            dgvPhieuNhap.DataSource = dsPNK;
            
            if (dgvPhieuNhap.Columns.Count > 0)
            {
                var visibleCols = new List<string> { "MaPhieu", "NgayNhap", "TenNV", "SoLoaiSP", "TongSL", "TongTien", "GhiChu" };
                foreach (DataGridViewColumn col in dgvPhieuNhap.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                dgvPhieuNhap.Columns["MaPhieu"].HeaderText = "Mã phiếu";
                dgvPhieuNhap.Columns["NgayNhap"].HeaderText = "Ngày nhập";
                dgvPhieuNhap.Columns["NgayNhap"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvPhieuNhap.Columns["TenNV"].HeaderText = "Nhân viên";
                dgvPhieuNhap.Columns["SoLoaiSP"].HeaderText = "Số loại SP";
                dgvPhieuNhap.Columns["TongSL"].HeaderText = "Tổng SL";
                dgvPhieuNhap.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvPhieuNhap.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvPhieuNhap.Columns["GhiChu"].HeaderText = "Ghi chú";
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PhieuNhapKho pnk = dgvPhieuNhap.Rows[e.RowIndex].DataBoundItem as PhieuNhapKho;
                if (pnk != null)
                    LoadChiTiet(pnk.MaPhieu);
            }
        }

        private void LoadChiTiet(string maPhieu)
        {
            lblDetailTitle.Text = "Chi tiết phiếu nhập: " + maPhieu;
            List<ChiTietNhapKho> dsCT = _pnkRepo.LayChiTiet(maPhieu);
            dgvChiTiet.DataSource = dsCT;

            if (dgvChiTiet.Columns.Count > 0)
            {
                var visibleCols = new List<string> { "MaSP", "TenSP", "SoLuong", "GiaNhap", "ThanhTien" };
                foreach (DataGridViewColumn col in dgvChiTiet.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                dgvChiTiet.Columns["MaSP"].HeaderText = "Mã SP";
                dgvChiTiet.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                dgvChiTiet.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvChiTiet.Columns["GiaNhap"].HeaderText = "Giá nhập";
                dgvChiTiet.Columns["GiaNhap"].DefaultCellStyle.Format = "N0";
                dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành tiền";
                dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";
            }
        }
    }
}
