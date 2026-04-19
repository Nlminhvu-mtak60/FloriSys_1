using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Services;

namespace FloriSys._4_KhoHang
{
    public partial class ucTonKho : UserControl
    {
        public ucTonKho() { InitializeComponent(); }
        private void ucTonKho_Load(object sender, EventArgs e) { LoadData(); }
        public void LoadData()
        {
            try
            {
                string key = txtTimKiem.Text.Trim();
                // Bỏ qua placeholder text
                if (key == "🔍 Tìm tên sản phẩm...") key = "";
                dgvTonKho.DataSource = SanPhamDAO.LayDanhSach(key, "", "DangBan");
                if (dgvTonKho.Columns.Count > 0)
                {
                    if (dgvTonKho.Columns.Contains("MaSP")) dgvTonKho.Columns["MaSP"].HeaderText = "Mã SP";
                    if (dgvTonKho.Columns.Contains("TenSP")) dgvTonKho.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    if (dgvTonKho.Columns.Contains("LoaiHoa")) dgvTonKho.Columns["LoaiHoa"].HeaderText = "Loại";
                    if (dgvTonKho.Columns.Contains("SoLuongTon")) dgvTonKho.Columns["SoLuongTon"].HeaderText = "Tồn kho";
                    if (dgvTonKho.Columns.Contains("MucTonToiThieu")) dgvTonKho.Columns["MucTonToiThieu"].HeaderText = "Tối thiểu";
                    if (dgvTonKho.Columns.Contains("GiaBan")) dgvTonKho.Columns["GiaBan"].HeaderText = "Giá bán";
                    if (dgvTonKho.Columns.Contains("GiaNhap")) dgvTonKho.Columns["GiaNhap"].HeaderText = "Giá nhập";
                    if (dgvTonKho.Columns.Contains("TrangThai")) dgvTonKho.Columns["TrangThai"].Visible = false;
                    dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "🔍 Tìm tên sản phẩm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "🔍 Tìm tên sản phẩm...";
                txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
