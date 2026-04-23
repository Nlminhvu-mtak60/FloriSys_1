using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._4_KhoHang
{
    public partial class ucXuatKho : UserControl
    {
        public ucXuatKho()
        {
            InitializeComponent();
        }

        private void ucXuatKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                List<DonChoXuatKho> dsXK = DonHangDAO.LayDonChoXuatKho();
                dgvXuatKho.DataSource = dsXK;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách xuất kho: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            if (dgvXuatKho.Columns.Count == 0) return;

            // Header names
            dgvXuatKho.Columns["MaDon"].HeaderText = "Mã đơn";
            dgvXuatKho.Columns["TenKH"].HeaderText = "Khách hàng";
            dgvXuatKho.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvXuatKho.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvXuatKho.Columns["SoLuongTon"].HeaderText = "Tồn hiện tại";
            dgvXuatKho.Columns["TinhTrangKho"].HeaderText = "Tình trạng";

            // Add button column if not exists
            if (!dgvXuatKho.Columns.Contains("btnXuat"))
            {
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.Name = "btnXuat";
                btnCol.HeaderText = "Thao tác";
                btnCol.Text = "Xác nhận xuất";
                btnCol.UseColumnTextForButtonValue = true;
                dgvXuatKho.Columns.Add(btnCol);
            }

            dgvXuatKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dgvXuatKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvXuatKho.Columns[e.ColumnIndex].Name == "btnXuat")
            {
                DonChoXuatKho item = dgvXuatKho.Rows[e.RowIndex].DataBoundItem as DonChoXuatKho;
                if (item == null) return;

                if (item.TinhTrangKho == "KhongDu")
                {
                    MessageBox.Show("Không thể xuất kho vì tồn kho không đủ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show($"Xác nhận xuất kho cho đơn {item.MaDon}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    XacNhanXuat(item.MaDon);
                }
            }
        }

        private void XacNhanXuat(string maDon)
        {
            try
            {
                // Thay đổi trạng thái sang DangXuLy để trừ tồn kho trong DB
                DonHangDAO.CapNhatTrangThai(maDon, "DangXuLy");
                MessageBox.Show("Xuất kho thành công! Đã trừ tồn kho.", "Thông báo");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xác nhận xuất kho: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
