using System;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.Models;
using FloriSys.DataAccess;

namespace FloriSys._7_DanhMuc
{
    public partial class frmThemSanPham : Form
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();

        public frmThemSanPham()
        {
            InitializeComponent();
        }

        private void frmThemSanPham_Load(object sender, EventArgs e)
        {
            txtMaSP.Text = _spRepo.LayMaSPSinhTuDong();
            cboLoai.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SanPham sp = new SanPham
                {
                    MaSP = txtMaSP.Text,
                    TenSP = txtTenSP.Text.Trim(),
                    LoaiHoa = cboLoai.SelectedItem.ToString(),
                    GiaBan = numGiaBan.Value,
                    GiaNhap = numGiaNhap.Value,
                    MucTonToiThieu = (int)numToiThieu.Value
                };

                _spRepo.ThemSanPham(sp);
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
