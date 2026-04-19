using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._3_BanHang
{
    public partial class ucChiTietDonHang : UserControl
    {
        private string _maDon;

        public ucChiTietDonHang()
        {
            InitializeComponent();
            LoadStatusList();
        }

        private void LoadStatusList()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new object[] { "Moi", "DangXuLy", "DaGiao", "HoanThanh", "Huy" });
        }

        public void SetMaDon(string maDon)
        {
            _maDon = maDon;
            LoadInfo();
            LoadItems();
            LoadTimeline();
        }

        private void LoadInfo()
        {
            try
            {
                DataTable dt = DonHangDAO.LayThongTinDon(_maDon);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblMaDon.Text = "Đơn hàng " + _maDon;
                    lblTenKH.Text = row["TenKH"].ToString();
                    lblSDT.Text = "SĐT: " + row["SoDienThoai"].ToString();
                    lblHinhThuc.Text = "Hình thức: " + row["HinhThucNhanHang"].ToString();
                    lblDiaChi.Text = "Địa chỉ: " + row["DiaChi"].ToString();
                    lblGhiChu.Text = "Ghi chú: " + row["GhiChu"].ToString();
                    lblTongTien.Text = string.Format("{0:#,##0}đ", row["TongTien"]);
                    
                    string status = row["TrangThai"].ToString();
                    lblStatusBadge.Text = status;
                    cboStatus.SelectedItem = status;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thông tin đơn: " + ex.Message);
            }
        }

        private void LoadItems()
        {
            try
            {
                dgvChiTiet.DataSource = DonHangDAO.LayChiTiet(_maDon);
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết sản phẩm: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            if (dgvChiTiet.Columns.Count == 0) return;
            dgvChiTiet.Columns["MaSP"].HeaderText = "Mã SP";
            dgvChiTiet.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành tiền";

            dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
            dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
            
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.MultiSelect = false;
        }

        private void LoadTimeline()
        {
            pnlTimeline.Controls.Clear();
            // In a real app, we'd query a LIC_SU_TRANG_THAI table.
            // For now, we'll just show a placeholder label.
            Label lblPlaceholder = new Label();
            lblPlaceholder.Text = "Lịch sử xử lý:\n- 08:40: Đơn hàng mới\n- 09:00: Đang chuẩn bị hàng";
            lblPlaceholder.AutoSize = true;
            lblPlaceholder.Location = new System.Drawing.Point(10, 10);
            pnlTimeline.Controls.Add(lblPlaceholder);
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (cboStatus.SelectedItem == null) return;
            try
            {
                string newStatus = cboStatus.SelectedItem.ToString();
                DonHangDAO.CapNhatTrangThai(_maDon, newStatus);
                MessageBox.Show("Cập nhật trạng thái thành công!", "Thông báo");
                LoadInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            // Trigger navigation back to list
            // Assuming we can find frmMain
            Control parent = this.Parent;
            while (parent != null && !(parent is Form))
            {
                parent = parent.Parent;
            }
            if (parent != null)
            {
                // This is a bit hacky but common in this project structure
                // Alternatively, use an event
                var frm = parent as Form;
                // Search for the method to switch uc
            }
        }
    }
}
