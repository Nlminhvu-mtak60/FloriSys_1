using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._3_BanHang
{
    public partial class ucDanhSachDon : BaseUserControl
    {
        private readonly DonHangRepository _dhRepo = new DonHangRepository();
        public event Action<string> XemChiTiet;
        public event Action TaoDonMoi;

        public ucDanhSachDon()
        {
            InitializeComponent();
        }

        private void ucDanhSachDon_Load(object sender, EventArgs e)
        {
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Tất cả trạng thái");
            cboTrangThai.Items.AddRange(new object[] { "Moi", "DangXuLy", "DaGiao", "HoanThanh", "Huy", "HoanHang" });
            cboTrangThai.SelectedIndex = 0;
            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                string key = txtTimKiem.Text.Trim();
                if (key == "🔍 Tìm mã đơn, tên khách...") key = "";
                string tt = cboTrangThai.SelectedIndex > 0 ? cboTrangThai.SelectedItem.ToString() : "";
                
                DateTime? ngayLoc = null;
                if (chkLocNgay.Checked)
                {
                    ngayLoc = dtpNgay.Value;
                }

                List<DonHang> dsDH = _dhRepo.LayDanhSach(key, tt, "", ngayLoc);
                dgvDonHang.DataSource = dsDH;
                FormatGrid();
                lblTongDon.Text = string.Format("Hiển thị {0} đơn hàng", dsDH.Count);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvDonHang.Columns.Count == 0) return;

            // Define columns we want to show
            var visibleCols = new List<string> { 
                "MaDon", "NgayTao", "TenKH", "SoDienThoai", "DiaChi", 
                "TongTien", "HinhThucDisplay", "TrangThaiDisplay", "TenNV" 
            };

            // Hide all other auto-generated columns
            foreach (DataGridViewColumn col in dgvDonHang.Columns)
            {
                if (!visibleCols.Contains(col.Name))
                {
                    col.Visible = false;
                }
            }

            // Set headers for visible columns
            if (dgvDonHang.Columns.Contains("MaDon")) dgvDonHang.Columns["MaDon"].HeaderText = "Mã đơn";
            if (dgvDonHang.Columns.Contains("NgayTao")) 
            {
                dgvDonHang.Columns["NgayTao"].HeaderText = "Ngày tạo";
                dgvDonHang.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
            if (dgvDonHang.Columns.Contains("TenKH")) dgvDonHang.Columns["TenKH"].HeaderText = "Khách hàng";
            if (dgvDonHang.Columns.Contains("SoDienThoai")) dgvDonHang.Columns["SoDienThoai"].HeaderText = "SĐT";
            if (dgvDonHang.Columns.Contains("DiaChi")) dgvDonHang.Columns["DiaChi"].HeaderText = "Địa chỉ";
            if (dgvDonHang.Columns.Contains("TongTien")) 
            {
                dgvDonHang.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
            }
            if (dgvDonHang.Columns.Contains("HinhThucDisplay")) dgvDonHang.Columns["HinhThucDisplay"].HeaderText = "Hình thức";
            if (dgvDonHang.Columns.Contains("TrangThaiDisplay")) dgvDonHang.Columns["TrangThaiDisplay"].HeaderText = "Trạng thái";
            if (dgvDonHang.Columns.Contains("TenNV")) dgvDonHang.Columns["TenNV"].HeaderText = "NV tạo";

            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonHang.ReadOnly = true;
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHang.MultiSelect = false;
        }

        private void btnTaoDon_Click(object sender, EventArgs e) { TaoDonMoi?.Invoke(); }
        private void btnLoc_Click(object sender, EventArgs e) { LoadData(); }

        private void chkLocNgay_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgay.Enabled = chkLocNgay.Checked;
            LoadData(); // Tự động lọc khi đổi trạng thái checkbox
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DonHang dh = dgvDonHang.Rows[e.RowIndex].DataBoundItem as DonHang;
                if (dh != null) XemChiTiet?.Invoke(dh.MaDon);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow != null)
            {
                DonHang dh = dgvDonHang.CurrentRow.DataBoundItem as DonHang;
                if (dh != null) XemChiTiet?.Invoke(dh.MaDon);
            }
        }
        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "🔍 Tìm mã đơn, tên khách...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "🔍 Tìm mã đơn, tên khách...";
                txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
