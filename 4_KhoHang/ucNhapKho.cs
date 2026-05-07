using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Services;
using FloriSys.Shared;

namespace FloriSys._4_KhoHang
{
    public partial class ucNhapKho : BaseUserControl
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();
        private readonly PhieuNhapKhoRepository _pnkRepo = new PhieuNhapKhoRepository();
        private DataTable _danhSachNhap;

        public ucNhapKho()
        {
            InitializeComponent();
            _danhSachNhap = new DataTable();
            _danhSachNhap.Columns.Add("MaSP", typeof(string));
            _danhSachNhap.Columns.Add("TenSP", typeof(string));
            _danhSachNhap.Columns.Add("SoLuong", typeof(int));
            _danhSachNhap.Columns.Add("GiaNhap", typeof(decimal));
        }

        public override void LoadData() { LoadSanPham(); }

        private void ucNhapKho_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            dgvNhap.DataSource = _danhSachNhap;
        }

        private void LoadSanPham()
        {
            try
            {
                List<SanPham> dsSP = _spRepo.LaySanPhamDangBan();
                cboSanPham.DataSource = dsSP;
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private void btnThemDong_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null || numSoLuong.Value <= 0 || numGiaNhap.Value <= 0)
            { ShowWarning("Vui lòng chọn sản phẩm, nhập số lượng và giá nhập!"); return; }

            string maSP = cboSanPham.SelectedValue.ToString();
            int soLuong = (int)numSoLuong.Value;
            decimal giaNhap = numGiaNhap.Value;

            foreach (DataRow row in _danhSachNhap.Rows)
            {
                if (row["MaSP"].ToString() == maSP)
                {
                    row["SoLuong"] = Convert.ToInt32(row["SoLuong"]) + soLuong;
                    row["GiaNhap"] = giaNhap;
                    return;
                }
            }
            _danhSachNhap.Rows.Add(maSP, cboSanPham.Text, soLuong, giaNhap);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_danhSachNhap.Rows.Count == 0) { ShowWarning("Chưa có sản phẩm nhập!"); return; }
            try
            {
                string maPhieu = _pnkRepo.TaoPhieuNhapHoanChinh(SessionManager.MaNV, txtGhiChu.Text.Trim(), _danhSachNhap);
                ShowSuccess("Phiếu nhập " + maPhieu + " đã được tạo!");
                _danhSachNhap.Clear(); txtGhiChu.Clear();
            }
            catch (Exception ex) { ShowError("Lỗi: " + ex.Message); }
        }
    }
}
