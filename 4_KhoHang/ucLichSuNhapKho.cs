using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._4_KhoHang
{
    public partial class ucLichSuNhapKho : UserControl
    {
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
            DataTable dt = NhanVienDAO.LayDanhSach();
            DataRow dr = dt.NewRow();
            dr["MaNV"] = "";
            dr["HoTen"] = "Tất cả nhân viên";
            dt.Rows.InsertAt(dr, 0);

            cboNhanVien.DataSource = dt;
            cboNhanVien.DisplayMember = "HoTen";
            cboNhanVien.ValueMember = "MaNV";
        }

        private void LoadData()
        {
            string keyword = txtTimKiem.Text;
            string maNV = cboNhanVien.SelectedValue?.ToString();
            DateTime fromDate = dtpTuNgay.Value;
            DateTime toDate = dtpDenNgay.Value;

            dgvPhieuNhap.DataSource = PhieuNhapKhoDAO.LayDanhSach(keyword, maNV, fromDate, toDate);
            
            // Format columns
            if (dgvPhieuNhap.Columns.Count > 0)
            {
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
                string maPhieu = dgvPhieuNhap.Rows[e.RowIndex].Cells["MaPhieu"].Value.ToString();
                LoadChiTiet(maPhieu);
            }
        }

        private void LoadChiTiet(string maPhieu)
        {
            lblDetailTitle.Text = "Chi tiết phiếu nhập: " + maPhieu;
            dgvChiTiet.DataSource = PhieuNhapKhoDAO.LayChiTiet(maPhieu);

            if (dgvChiTiet.Columns.Count > 0)
            {
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
