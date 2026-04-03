using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._3_BanHang
{
    public partial class ucDanhSachDon : UserControl
    {
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

        public void LoadData()
        {
            try
            {
                string key = txtTimKiem.Text.Trim();
                string tt = cboTrangThai.SelectedIndex > 0 ? cboTrangThai.SelectedItem.ToString() : "";
                DataTable dt = DonHangDAO.LayDanhSach(key, tt);
                dgvDonHang.DataSource = dt;
                FormatGrid();
                lblTongDon.Text = string.Format("Hiển thị {0} đơn hàng", dt.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvDonHang.Columns.Count == 0) return;
            dgvDonHang.Columns["MaDon"].HeaderText = "Mã đơn";
            dgvDonHang.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvDonHang.Columns["TenKH"].HeaderText = "Khách hàng";
            dgvDonHang.Columns["SoDienThoai"].HeaderText = "SĐT";
            dgvDonHang.Columns["HinhThucNhanHang"].HeaderText = "Hình thức";
            dgvDonHang.Columns["TongTien"].HeaderText = "Tổng tiền";
            dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "#,##0";
            dgvDonHang.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvDonHang.Columns["TenNV"].HeaderText = "NV tạo";
            if (dgvDonHang.Columns.Contains("GhiChu"))
                dgvDonHang.Columns["GhiChu"].Visible = false;
            dgvDonHang.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDonHang.ReadOnly = true;
            dgvDonHang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDonHang.MultiSelect = false;
        }

        private void btnTaoDon_Click(object sender, EventArgs e)
        {
            TaoDonMoi?.Invoke();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maDon = dgvDonHang.Rows[e.RowIndex].Cells["MaDon"].Value.ToString();
                XemChiTiet?.Invoke(maDon);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            if (dgvDonHang.CurrentRow != null)
            {
                string maDon = dgvDonHang.CurrentRow.Cells["MaDon"].Value.ToString();
                XemChiTiet?.Invoke(maDon);
            }
        }
    }
}
