using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._3_BanHang
{
    public partial class ucTraHang : UserControl
    {
        private string _maDon;

        public ucTraHang()
        {
            InitializeComponent();
            LoadLyDo();
            LoadHinhThuc();
            txtMaDon.ReadOnly = false;
            txtMaDon.BackColor = System.Drawing.Color.White;
            txtMaDon.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SetMaDon(txtMaDon.Text.Trim());
                }
            };
        }

        private void LoadLyDo()
        {
            cboLyDo.Items.Clear();
            cboLyDo.Items.AddRange(new object[] { "Hoa héo/hỏng", "Giao sai sản phẩm", "Giao trễ dịp lễ", "Khách đổi ý" });
            cboLyDo.SelectedIndex = 0;
        }

        private void LoadHinhThuc()
        {
            cboHoanTien.Items.Clear();
            cboHoanTien.Items.AddRange(new object[] { "Hoàn tiền toàn bộ", "Hoàn tiền một phần", "Không hoàn tiền" });
            cboHoanTien.SelectedIndex = 0;
        }

        public void SetMaDon(string maDon)
        {
            _maDon = maDon;
            txtMaDon.Text = _maDon;
            LoadOrderProducts();
        }

        private void LoadOrderProducts()
        {
            try
            {
                // Get chi tiết đơn hàng as List, then convert to DataTable for editing
                List<ChiTietDonHang> dsCT = DonHangDAO.LayChiTiet(_maDon);
                
                DataTable dt = new DataTable();
                dt.Columns.Add("MaSP", typeof(string));
                dt.Columns.Add("TenSP", typeof(string));
                dt.Columns.Add("SoLuong", typeof(int));
                dt.Columns.Add("DonGia", typeof(decimal));
                dt.Columns.Add("ThanhTien", typeof(decimal));
                dt.Columns.Add("SLTra", typeof(int));
                dt.Columns["SLTra"].DefaultValue = 0;
                dt.Columns.Add("CoNhapKho", typeof(bool));
                dt.Columns["CoNhapKho"].DefaultValue = true;

                foreach (ChiTietDonHang ct in dsCT)
                {
                    dt.Rows.Add(ct.MaSP, ct.TenSP, ct.SoLuong, ct.DonGia, ct.ThanhTien, 0, true);
                }

                dgvSanPhamTra.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm đơn: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            if (dgvSanPhamTra.Columns.Count == 0) return;
            dgvSanPhamTra.Columns["MaSP"].HeaderText = "Mã SP";
            dgvSanPhamTra.Columns["TenSP"].HeaderText = "Tên SP";
            dgvSanPhamTra.Columns["SoLuong"].HeaderText = "SL Mua";
            dgvSanPhamTra.Columns["SoLuong"].ReadOnly = true;
            dgvSanPhamTra.Columns["DonGia"].Visible = false;
            dgvSanPhamTra.Columns["ThanhTien"].Visible = false;

            dgvSanPhamTra.Columns["SLTra"].HeaderText = "SL Trả";
            dgvSanPhamTra.Columns["CoNhapKho"].HeaderText = "Nhập lại kho?";
        }

        private void btnDuyet_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maDon)) return;

            try
            {
                string lyDo = cboLyDo.SelectedItem.ToString();
                string hinhThuc = cboHoanTien.SelectedItem.ToString();
                string ghiChu = txtGhiChu.Text.Trim();

                string maPhieu = TraHangDAO.ThemPhieuTra(_maDon, lyDo, hinhThuc, ghiChu);

                foreach (DataGridViewRow row in dgvSanPhamTra.Rows)
                {
                    int slTra = Convert.ToInt32(row.Cells["SLTra"].Value);
                    if (slTra > 0)
                    {
                        string maSP = row.Cells["MaSP"].Value.ToString();
                        bool restock = Convert.ToBoolean(row.Cells["CoNhapKho"].Value);
                        TraHangDAO.ThemChiTietTra(maPhieu, maSP, slTra, restock);
                    }
                }

                MessageBox.Show("Đã duyệt phiếu trả hàng thành công!", "Thông báo");
                // Navigation or refresh here
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xử lý trả hàng: " + ex.Message);
            }
        }
    }
}
