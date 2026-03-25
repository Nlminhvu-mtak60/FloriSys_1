using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._4_KhoHang
{
    public partial class ucCauHinhTonKho : UserControl
    {
        public ucCauHinhTonKho()
        {
            InitializeComponent();
        }

        private void ucCauHinhTonKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = SanPhamDAO.LayDanhSach(txtTimKiem.Text);
            dgvSanPham.DataSource = dt;

            // Format columns
            foreach (DataGridViewColumn col in dgvSanPham.Columns)
            {
                col.ReadOnly = true;
            }

            if (dgvSanPham.Columns.Contains("MaSP")) dgvSanPham.Columns["MaSP"].HeaderText = "Mã SP";
            if (dgvSanPham.Columns.Contains("TenSP")) dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            if (dgvSanPham.Columns.Contains("LoaiHoa")) dgvSanPham.Columns["LoaiHoa"].HeaderText = "Loại";
            if (dgvSanPham.Columns.Contains("SoLuongTon")) dgvSanPham.Columns["SoLuongTon"].HeaderText = "Tồn thực tế";
            
            if (dgvSanPham.Columns.Contains("MucTonToiThieu"))
            {
                dgvSanPham.Columns["MucTonToiThieu"].HeaderText = "Ngưỡng tối thiểu";
                dgvSanPham.Columns["MucTonToiThieu"].ReadOnly = false; // For editing
                dgvSanPham.Columns["MucTonToiThieu"].DefaultCellStyle.BackColor = Color.LightYellow;
            }

            // Hide unneeded
            if (dgvSanPham.Columns.Contains("GiaBan")) dgvSanPham.Columns["GiaBan"].Visible = false;
            if (dgvSanPham.Columns.Contains("GiaNhap")) dgvSanPham.Columns["GiaNhap"].Visible = false;
            if (dgvSanPham.Columns.Contains("TrangThai")) dgvSanPham.Columns["TrangThai"].Visible = false;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            dgvSanPham.EndEdit();
            bool hasError = false;

            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                if (row.Cells["MaSP"].Value != null && row.Cells["MucTonToiThieu"].Value != null)
                {
                    string maSP = row.Cells["MaSP"].Value.ToString();
                    if (int.TryParse(row.Cells["MucTonToiThieu"].Value.ToString(), out int mucTon))
                    {
                        SanPhamDAO.CapNhatMucTonToiThieu(maSP, mucTon);
                    }
                    else
                    {
                        hasError = true;
                    }
                }
            }

            if (hasError)
            {
                MessageBox.Show("Có một số giá trị không hợp lệ (không phải số nguyên).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("Đã lưu cấu hình ngưỡng tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LoadData();
        }

        private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSanPham.Columns[e.ColumnIndex].Name == "SoLuongTon")
            {
                int ton = Convert.ToInt32(e.Value);
                int nguong = Convert.ToInt32(dgvSanPham.Rows[e.RowIndex].Cells["MucTonToiThieu"].Value);

                if (ton == 0)
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(dgvSanPham.Font, FontStyle.Bold);
                }
                else if (ton < nguong)
                {
                    e.CellStyle.ForeColor = Color.OrangeRed;
                    e.CellStyle.Font = new Font(dgvSanPham.Font, FontStyle.Bold);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
            }
        }
    }
}
