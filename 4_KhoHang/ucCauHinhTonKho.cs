using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._4_KhoHang
{
    public partial class ucCauHinhTonKho : BaseUserControl
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();

        public ucCauHinhTonKho()
        {
            InitializeComponent();
        }

        private void ucCauHinhTonKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void LoadData()
        {
            string keyword = txtTimKiem.Text == "🔍 Tìm mã hoặc tên sản phẩm..." ? "" : txtTimKiem.Text.Trim();
            List<SanPham> dsSP = _spRepo.LayDanhSach(keyword);
            dgvSanPham.AutoGenerateColumns = false;
            dgvSanPham.DataSource = null;
            dgvSanPham.DataSource = dsSP;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "🔍 Tìm mã hoặc tên sản phẩm...")
                LoadData();
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "🔍 Tìm mã hoặc tên sản phẩm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "🔍 Tìm mã hoặc tên sản phẩm...";
                txtTimKiem.ForeColor = Color.Gray;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            dgvSanPham.EndEdit();
            bool hasError = false;

            foreach (DataGridViewRow row in dgvSanPham.Rows)
            {
                if (row.Cells["colMaSP"].Value != null && row.Cells["colMucTonToiThieu"].Value != null)
                {
                    string maSP = row.Cells["colMaSP"].Value.ToString();
                    if (int.TryParse(row.Cells["colMucTonToiThieu"].Value.ToString(), out int mucTon))
                    {
                        _spRepo.CapNhatMucTonToiThieu(maSP, mucTon);
                    }
                    else
                    {
                        hasError = true;
                    }
                }
            }

            if (hasError)
                ShowWarning("Có một số giá trị không hợp lệ (không phải số nguyên).");
            else
                ShowSuccess("Đã lưu cấu hình ngưỡng tồn kho thành công!");
            LoadData();
        }

        private void dgvSanPham_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSanPham.Columns[e.ColumnIndex].Name == "colSoLuongTon")
            {
                int ton = Convert.ToInt32(e.Value);
                int nguong = Convert.ToInt32(dgvSanPham.Rows[e.RowIndex].Cells["colMucTonToiThieu"].Value);

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
