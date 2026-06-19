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
            dgvPhieuNhap.AutoGenerateColumns = false;
            dgvPhieuNhap.DataSource = dsPNK;
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
            dgvChiTiet.AutoGenerateColumns = false;
            dgvChiTiet.DataSource = dsCT;
        }
    }
}
