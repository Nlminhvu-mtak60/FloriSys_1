using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._7_DanhMuc
{
    public partial class ucSanPham : BaseUserControl
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();

        public ucSanPham()
        {
            InitializeComponent();
        }

        private void ucSanPham_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadData();
        }

        private void LoadCategories()
        {
            cboLoai.Items.Clear();
            cboLoai.Items.Add("Tất cả loại");
            cboLoai.Items.AddRange(new object[] { "Hoa tươi", "Bó hoa", "Lẵng hoa", "Phụ kiện", "Chậu cây" });
            cboLoai.SelectedIndex = 0;
        }

        public override void LoadData()
        {
            try
            {
                string key = txtSearch.Text.Trim();
                string loai = cboLoai.SelectedIndex > 0 ? cboLoai.SelectedItem.ToString() : "";
                List<SanPham> dsSP = _spRepo.LayDanhSach(key, loai);
                dgvSanPham.DataSource = dsSP;
                FormatGrid();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvSanPham.Columns.Count == 0) return;

            var visibleCols = new List<string> { "MaSP", "TenSP", "LoaiHoa", "GiaBan", "GiaNhap", "SoLuongTon", "MucTonToiThieu", "TrangThai" };
            foreach (DataGridViewColumn col in dgvSanPham.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

            dgvSanPham.Columns["MaSP"].HeaderText = "Mã SP";
            dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns["LoaiHoa"].HeaderText = "Loại";
            dgvSanPham.Columns["GiaBan"].HeaderText = "Giá bán";
            dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá nhập";
            dgvSanPham.Columns["SoLuongTon"].HeaderText = "Tồn kho";
            dgvSanPham.Columns["MucTonToiThieu"].HeaderText = "Tối thiểu";
            dgvSanPham.Columns["TrangThai"].HeaderText = "Trạng thái";

            dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";
            dgvSanPham.Columns["GiaNhap"].DefaultCellStyle.Format = "#,##0";
            
            // Highlight low stock
            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                SanPham sp = row.DataBoundItem as SanPham;
                if (sp != null && sp.SoLuongTon <= sp.MucTonToiThieu)
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
                }
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Optional: Auto-filter as typing
            // LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frmThemSanPham frm = new frmThemSanPham())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Logic for editing
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null)
            {
                ShowWarning("Vui lòng chọn sản phẩm cần xóa.");
                return;
            }

            SanPham sp = dgvSanPham.CurrentRow.DataBoundItem as SanPham;
            if (sp == null) return;

            if (sp.TrangThai == "NgungBan")
            {
                ShowWarning("Sản phẩm này đã ngừng bán rồi.");
                return;
            }

            if (Confirm($"Bạn có chắc muốn ngừng bán sản phẩm \"{sp.TenSP}\" ({sp.MaSP})?\n\nSản phẩm sẽ chuyển sang trạng thái 'Ngừng bán' và không hiển thị khi tạo đơn."))
            {
                try
                {
                    _spRepo.NgungBanSanPham(sp.MaSP);
                    ShowSuccess("Đã ngừng bán sản phẩm " + sp.TenSP);
                    LoadData();
                }
                catch (Exception ex)
                {
                    ShowError("Lỗi: " + ex.Message);
                }
            }
        }
    }
}
