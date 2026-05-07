using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Services;
using FloriSys.Shared;

namespace FloriSys._3_BanHang
{
    public partial class ucTaoDon : BaseUserControl
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();
        private readonly KhachHangRepository _khRepo = new KhachHangRepository();
        private readonly DonHangRepository _dhRepo = new DonHangRepository();
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

        public override void LoadData() { LoadSanPham(); }

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
                List<SanPham> dsSP = _spRepo.LaySanPhamDangBan(key);
                dgvSanPham.DataSource = dsSP;
                FormatGridSP();
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private void FormatGridSP()
        {
            if (dgvSanPham.Columns.Count == 0) return;

            var visibleCols = new List<string> { "TenSP", "GiaBan", "SoLuongTon" };
            foreach (DataGridViewColumn col in dgvSanPham.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

            dgvSanPham.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvSanPham.Columns["GiaBan"].HeaderText = "Giá bán";
            dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";
            dgvSanPham.Columns["SoLuongTon"].HeaderText = "Tồn kho";
            
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btnTimSP_Click(object sender, EventArgs e) { LoadSanPham(txtTimSP.Text.Trim()); }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null) return;
            SanPham sp = dgvSanPham.CurrentRow.DataBoundItem as SanPham;
            if (sp == null) return;

            if (sp.SoLuongTon <= 0) { ShowWarning("Sản phẩm đã hết hàng!"); return; }

            foreach (DataRow row in _gioHang.Rows)
            {
                if (row["MaSP"].ToString() == sp.MaSP)
                {
                    int sl = Convert.ToInt32(row["SoLuong"]) + 1;
                    if (sl > sp.SoLuongTon) { ShowWarning("Vượt quá tồn kho!"); return; }
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
            { ShowWarning("Vui lòng nhập thông tin khách hàng!"); return; }
            if (_gioHang.Rows.Count == 0)
            { ShowWarning("Giỏ hàng trống!"); return; }

            try
            {
                // Tìm hoặc tạo khách hàng
                string maKH = _khRepo.TimHoacTao(txtTenKH.Text.Trim(), txtSDT.Text.Trim(), txtDiaChi.Text.Trim());

                string hinhThuc = cboHinhThuc.SelectedIndex == 0 ? "TaiQuay" : "GiaoTanNoi";

                // Gọi hàm Transaction tập trung (TaoDon + ChiTiet + GiaoHang trong 1 transaction)
                string maDon = _dhRepo.TaoDonHangHoanChinh(maKH, SessionManager.MaNV, hinhThuc, txtGhiChu.Text.Trim(), _gioHang);

                ShowSuccess("Tạo đơn hàng " + maDon + " thành công!");
                _gioHang.Clear();
                txtTenKH.Clear(); txtSDT.Clear(); txtDiaChi.Clear(); txtGhiChu.Clear();
                TinhTong();
                DonDaTao?.Invoke();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi: " + ex.Message);
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
