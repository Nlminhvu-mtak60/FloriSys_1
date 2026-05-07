using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._4_KhoHang
{
    public partial class ucHangHu : BaseUserControl
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();
        private readonly HangHuRepository _hhRepo = new HangHuRepository();

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
            LoadData();
        }

        private void LoadSanPham()
        {
            try
            {
                List<SanPham> dsSP = _spRepo.LayDanhSach();
                cboSanPham.DataSource = dsSP;
                cboSanPham.DisplayMember = "TenSP";
                cboSanPham.ValueMember = "MaSP";
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải sản phẩm: " + ex.Message);
            }
        }

        public override void LoadData()
        {
            try
            {
                List<HangHu> dsHH = _hhRepo.LayLichSu(DateTime.Now.Month, DateTime.Now.Year);
                dgvHistory.DataSource = dsHH;
                FormatGrid();
                UpdateTotalLoss(dsHH);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải lịch sử: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvHistory.Columns.Count == 0) return;

            var visibleCols = new List<string> { "MaPhieuHuy", "TenSP", "SoLuong", "LyDo", "NgayHuy" };
            foreach (DataGridViewColumn col in dgvHistory.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

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
            decimal totalLoss = 0;
            foreach (HangHu hh in dsHH) 
            {
                totalQty += hh.SoLuong;
                totalLoss += (hh.SoLuong * hh.GiaNhap);
            }
            lblTotalLoss.Text = string.Format("{0} sản phẩm / Thiệt hại ước tính: {1:#,##0} VNĐ", totalQty, totalLoss);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboSanPham.SelectedValue == null || txtSoLuong.Value <= 0)
            {
                ShowWarning("Vui lòng chọn sản phẩm và nhập số lượng > 0");
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

                _hhRepo.GhiNhan(hh);
                ShowSuccess("Đã ghi nhận hàng hư thành công!");
                txtSoLuong.Value = 0;
                txtNote.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi ghi nhận: " + ex.Message);
            }
        }
    }
}
