using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._4_KhoHang
{
    public partial class ucHangHu : UserControl
    {
        public ucHangHu()
        {
            InitializeComponent();
            LoadLyDo();
        }

        private void LoadLyDo()
        {
            cboLyDo.Items.Clear();
            cboLyDo.Items.AddRange(new object[] { "Hoa héo/chết", "Hư hỏng do vận chuyển", "Quá hạn sử dụng", "Khác" });
            cboLyDo.SelectedIndex = 0;
        }

        private void ucHangHu_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            LoadHistory();
        }

        private void LoadSanPham()
        {
            try
            {
                DataTable dt = SanPhamDAO.LayDanhSach();
                cboSanPham.DataSource = dt;
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message);
            }
        }

        private void LoadHistory()
        {
            try
            {
                DataTable dt = HangHuDAO.LayLichSu(DateTime.Now.Month, DateTime.Now.Year);
                dgvHistory.DataSource = dt;
                FormatGrid();
                UpdateTotalLoss(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            if (dgvHistory.Columns.Count == 0) return;
            dgvHistory.Columns["MaPhieuHuy"].HeaderText = "Mã phiếu";
            dgvHistory.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvHistory.Columns["SoLuong"].HeaderText = "SL";
            dgvHistory.Columns["LyDo"].HeaderText = "Lý do";
            dgvHistory.Columns["NgayHuy"].HeaderText = "Ngày hủy";
            
            dgvHistory.Columns["NgayHuy"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void UpdateTotalLoss(DataTable dt)
        {
            int totalQty = 0;
            // In a real app, we'd multiply by GiaNhap to get money loss
            foreach (DataRow row in dt.Rows)
            {
                totalQty += Convert.ToInt32(row["SoLuong"]);
            }
            lblTotalLoss.Text = totalQty + " sản phẩm / (Tính toán thiệt hại...)";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null || txtSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm và nhập số lượng > 0");
                return;
            }

            try
            {
                string maSP = cboSanPham.SelectedValue.ToString();
                int sl = (int)txtSoLuong.Value;
                string lyDo = cboLyDo.SelectedItem.ToString();
                string ghiChu = txtNote.Text.Trim();

                HangHuDAO.GhiNhan(maSP, sl, lyDo, ghiChu);
                MessageBox.Show("Đã ghi nhận hàng hư thành công!", "Thông báo");
                
                txtSoLuong.Value = 0;
                txtNote.Clear();
                LoadHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ghi nhận: " + ex.Message);
            }
        }
    }
}
