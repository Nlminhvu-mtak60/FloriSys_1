using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._7_DanhMuc
{
    public partial class ucSanPham : UserControl
    {
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
            // Basic categories from DB or common ones
            cboLoai.Items.AddRange(new object[] { "Hoa tươi", "Bó hoa", "Lẵng hoa", "Phụ kiện", "Chậu cây" });
            cboLoai.SelectedIndex = 0;
        }

        public void LoadData()
        {
            try
            {
                string key = txtSearch.Text.Trim();
                string loai = cboLoai.SelectedIndex > 0 ? cboLoai.SelectedItem.ToString() : "";
                DataTable dt = SanPhamDAO.LayDanhSach(key, loai);
                dgvSanPham.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvSanPham.Columns.Count == 0) return;

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
                int ton = Convert.ToInt32(row.Cells["SoLuongTon"].Value);
                int min = Convert.ToInt32(row.Cells["MucTonToiThieu"].Value);
                if (ton <= min)
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
            MessageBox.Show("Tính năng Thêm sản phẩm đang được cập nhật...", "Thông báo");
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Logic for editing
            }
        }
    }
}
