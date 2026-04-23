using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Services;

namespace FloriSys._4_KhoHang
{
    public partial class ucNhapKho : UserControl
    {
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
        private void ucNhapKho_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            dgvNhap.DataSource = _danhSachNhap;
        }
        private void LoadSanPham()
        {
            try
            {
                List<SanPham> dsSP = SanPhamDAO.LaySanPhamDangBan();
                cboSanPham.DataSource = dsSP;
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btnThemDong_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null || numSoLuong.Value <= 0 || numGiaNhap.Value <= 0)
            { MessageBox.Show("Vui lòng chọn sản phẩm, nhập số lượng và giá nhập!"); return; }
            _danhSachNhap.Rows.Add(cboSanPham.SelectedValue.ToString(), cboSanPham.Text, (int)numSoLuong.Value, numGiaNhap.Value);
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_danhSachNhap.Rows.Count == 0) { MessageBox.Show("Chưa có sản phẩm nhập!"); return; }
            try
            {
                string maPhieu = PhieuNhapKhoDAO.TaoPhieuNhap(SessionManager.MaNV, txtGhiChu.Text.Trim());
                foreach (DataRow row in _danhSachNhap.Rows)
                    PhieuNhapKhoDAO.ThemChiTiet(maPhieu, row["MaSP"].ToString(), Convert.ToInt32(row["SoLuong"]), Convert.ToDecimal(row["GiaNhap"]));
                MessageBox.Show("Phiếu nhập " + maPhieu + " đã được tạo!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _danhSachNhap.Clear(); txtGhiChu.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void lblSP_Click(object sender, EventArgs e)
        {

        }
    }
}
