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

            var visibleCols = new List<string> { "MaSP", "TenSP", "LoaiHoa", "GiaBan", "SoLuongTon" };
            
            // Chỉ những người có quyền Sửa (Admin, Warehouse) mới được xem Giá nhập, Mức tồn tối thiểu, Trạng thái
            bool canEdit = FloriSys.Services.SessionManager.Instance.HasPermission("SanPham", "Sua");
            if (canEdit)
            {
                visibleCols.Add("GiaNhap");
                visibleCols.Add("MucTonToiThieu");
                visibleCols.Add("TrangThai");
            }

            foreach (DataGridViewColumn col in dgvSanPham.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

            dgvSanPham.Columns["MaSP"].HeaderText = "Mã SP";
            dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns["LoaiHoa"].HeaderText = "Loại";
            dgvSanPham.Columns["GiaBan"].HeaderText = "Giá bán";
            dgvSanPham.Columns["SoLuongTon"].HeaderText = "Tồn kho";

            if (canEdit)
            {
                dgvSanPham.Columns["GiaNhap"].HeaderText = "Giá nhập";
                dgvSanPham.Columns["MucTonToiThieu"].HeaderText = "Tối thiểu";
                dgvSanPham.Columns["TrangThai"].HeaderText = "Trạng thái";
            }

            dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "#,##0";
            dgvSanPham.Columns["GiaNhap"].DefaultCellStyle.Format = "#,##0";
            
            dgvSanPham.ReadOnly = true;
            dgvSanPham.AllowUserToAddRows = false;
            dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvSanPham.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Highlight low stock and inactive products
            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                SanPham sp = row.DataBoundItem as SanPham;
                if (sp != null)
                {
                    if (sp.SoLuongTon <= sp.MucTonToiThieu)
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.MistyRose;
                    
                    if (sp.TrangThai == "NgungBan")
                        row.DefaultCellStyle.ForeColor = System.Drawing.Color.Gray;
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
            if (!CheckPermission("SanPham", "Them")) return;

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
                // Logic for editing - should also check "Sua"
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("SanPham", "Xoa")) return;

            if (dgvSanPham.CurrentRow == null)
            {
                ShowWarning("Vui lòng chọn sản phẩm cần xóa.");
                return;
            }

            SanPham sp = dgvSanPham.CurrentRow.DataBoundItem as SanPham;
            if (sp == null) return;

            if (Confirm($"Bạn có chắc muốn XÓA VĨNH VIỄN sản phẩm \"{sp.TenSP}\" ({sp.MaSP})?\n\nHành động này không thể hoàn tác và có thể thất bại nếu sản phẩm đã có trong lịch sử đơn hàng."))
            {
                try
                {
                    _spRepo.Xoa(sp.MaSP);
                    ShowSuccess("Đã xóa sản phẩm khỏi hệ thống.");
                    LoadData();
                }
                catch (Exception)
                {
                    ShowError("Không thể xóa sản phẩm này vì đã có dữ liệu liên quan (đơn hàng, nhập kho). Vui lòng sử dụng chức năng 'Đổi trạng thái' để ngừng kinh doanh thay vì xóa.");
                }
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            if (!CheckPermission("SanPham", "Sua")) return;

            if (dgvSanPham.CurrentRow == null)
            {
                ShowWarning("Vui lòng chọn sản phẩm.");
                return;
            }

            SanPham sp = dgvSanPham.CurrentRow.DataBoundItem as SanPham;
            if (sp == null) return;

            string trangThaiMoi = sp.TrangThai == "DangBan" ? "NgungBan" : "DangBan";
            string title = trangThaiMoi == "NgungBan" ? "Ngừng bán" : "Kinh doanh lại";

            if (Confirm($"Bạn có chắc muốn {title.ToLower()} sản phẩm \"{sp.TenSP}\" ({sp.MaSP})?"))
            {
                try
                {
                    _spRepo.DoiTrangThai(sp.MaSP, trangThaiMoi);
                    ShowSuccess($"Đã chuyển trạng thái sản phẩm sang {title}");
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
