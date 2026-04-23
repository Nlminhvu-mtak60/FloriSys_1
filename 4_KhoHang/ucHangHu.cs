using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

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
                List<SanPham> dsSP = SanPhamDAO.LayDanhSach();
                cboSanPham.DataSource = dsSP;
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
                List<HangHu> dsHH = HangHuDAO.LayLichSu(DateTime.Now.Month, DateTime.Now.Year);
                dgvHistory.DataSource = dsHH;
                FormatGrid();
                UpdateTotalLoss(dsHH);
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

        private void UpdateTotalLoss(List<HangHu> dsHH)
        {
            int totalQty = 0;
            foreach (HangHu hh in dsHH)
            {
                totalQty += hh.SoLuong;
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
                HangHu hh = new HangHu
                {
                    MaSP = cboSanPham.SelectedValue.ToString(),
                    SoLuong = (int)txtSoLuong.Value,
                    LyDo = cboLyDo.SelectedItem.ToString(),
                    GhiChu = txtNote.Text.Trim()
                };

                HangHuDAO.GhiNhan(hh);
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
