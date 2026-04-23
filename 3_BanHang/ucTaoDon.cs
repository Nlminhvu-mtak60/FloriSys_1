using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Services;

namespace FloriSys._3_BanHang
{
    public partial class ucTaoDon : UserControl
    {
        private DataTable _gioHang;
        public event Action DonDaTao;

        public ucTaoDon()
        {
            InitializeComponent();
            _gioHang = new DataTable();
            _gioHang.Columns.Add("MaSP", typeof(string));
            _gioHang.Columns.Add("TenSP", typeof(string));
            _gioHang.Columns.Add("SoLuong", typeof(int));
            _gioHang.Columns.Add("DonGia", typeof(decimal));
            _gioHang.Columns.Add("ThanhTien", typeof(decimal));
        }

        private void ucTaoDon_Load(object sender, EventArgs e)
        {
            cboHinhThuc.Items.Clear();
            cboHinhThuc.Items.Add("Nhận tại quầy");
            cboHinhThuc.Items.Add("Giao tận nơi");
            cboHinhThuc.SelectedIndex = 0;
            LoadSanPham();
            dgvGioHang.DataSource = _gioHang;
            TinhTong();
        }

        private void LoadSanPham(string key = "")
        {
            try
            {
                List<SanPham> dsSP = SanPhamDAO.LaySanPhamDangBan(key);
                dgvSanPham.DataSource = dsSP;
                FormatGridSP();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void FormatGridSP()
        {
            if (dgvSanPham.Columns.Count == 0) return;
            dgvSanPham.Columns["MaSP"].Visible = false;
            dgvSanPham.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvSanPham.Columns["GiaBan"].HeaderText = "Giá bán";
            dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";
            dgvSanPham.Columns["SoLuongTon"].HeaderText = "Tồn kho";
            if (dgvSanPham.Columns.Contains("LoaiHoa")) dgvSanPham.Columns["LoaiHoa"].Visible = false;
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnTimSP_Click(object sender, EventArgs e) { LoadSanPham(txtTimSP.Text.Trim()); }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;
            SanPham sp = dgvSanPham.CurrentRow.DataBoundItem as SanPham;
            if (sp == null) return;

            if (sp.SoLuongTon <= 0) { MessageBox.Show("Sản phẩm đã hết hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            foreach (DataRow row in _gioHang.Rows)
            {
                if (row["MaSP"].ToString() == sp.MaSP)
                {
                    int sl = Convert.ToInt32(row["SoLuong"]) + 1;
                    if (sl > sp.SoLuongTon) { MessageBox.Show("Vượt quá tồn kho!"); return; }
                    row["SoLuong"] = sl;
                    row["ThanhTien"] = sl * sp.GiaBan;
                    TinhTong();
                    return;
                }
            }
            _gioHang.Rows.Add(sp.MaSP, sp.TenSP, 1, sp.GiaBan, sp.GiaBan);
            TinhTong();
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (dgvGioHang.CurrentRow == null) return;
            _gioHang.Rows.RemoveAt(dgvGioHang.CurrentRow.Index);
            TinhTong();
        }

        private void TinhTong()
        {
            decimal tong = 0;
            foreach (DataRow row in _gioHang.Rows)
                tong += Convert.ToDecimal(row["ThanhTien"]);
            lblTongTien.Text = string.Format("Tổng cộng: {0:#,##0}đ", tong);
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenKH.Text.Trim()) || string.IsNullOrEmpty(txtSDT.Text.Trim()))
            { MessageBox.Show("Vui lòng nhập thông tin khách hàng!"); return; }
            if (_gioHang.Rows.Count == 0)
            { MessageBox.Show("Giỏ hàng trống!"); return; }

            try
            {
                // Tìm hoặc tạo khách hàng
                KhachHang khTim = KhachHangDAO.TimTheoSDT(txtSDT.Text.Trim());
                string maKH;
                if (khTim != null)
                {
                    maKH = khTim.MaKH;
                }
                else
                {
                    KhachHang khMoi = new KhachHang
                    {
                        HoTen = txtTenKH.Text.Trim(),
                        SoDienThoai = txtSDT.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim()
                    };
                    maKH = KhachHangDAO.ThemKhachHang(khMoi);
                }

                string hinhThuc = cboHinhThuc.SelectedIndex == 0 ? "TaiQuay" : "GiaoTanNoi";
                string maDon = DonHangDAO.TaoDonHang(maKH, SessionManager.MaNV, hinhThuc, txtGhiChu.Text.Trim());

                foreach (DataRow row in _gioHang.Rows)
                {
                    DonHangDAO.ThemChiTiet(maDon, row["MaSP"].ToString(),
                        Convert.ToInt32(row["SoLuong"]), Convert.ToDecimal(row["DonGia"]));
                }

                // Nếu giao tận nơi → tạo lệnh giao hàng
                if (hinhThuc == "GiaoTanNoi")
                    GiaoHangDAO.TaoGiaoHang(maDon);

                MessageBox.Show("Tạo đơn hàng " + maDon + " thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _gioHang.Clear();
                txtTenKH.Clear(); txtSDT.Clear(); txtDiaChi.Clear(); txtGhiChu.Clear();
                TinhTong();
                DonDaTao?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            _gioHang.Clear();
            txtTenKH.Clear(); txtSDT.Clear(); txtDiaChi.Clear(); txtGhiChu.Clear();
            TinhTong();
        }
    }
}
