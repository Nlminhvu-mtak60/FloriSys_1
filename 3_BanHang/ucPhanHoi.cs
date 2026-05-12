using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._3_BanHang
{
    public partial class ucPhanHoi : BaseUserControl
    {
        private readonly PhanHoiRepository _phRepo = new PhanHoiRepository();
        private string _maDon;

        public ucPhanHoi()
        {
            InitializeComponent();
            txtMaDon.ReadOnly = false;
            txtMaDon.BackColor = System.Drawing.Color.White;
            txtMaDon.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SetMaDon(txtMaDon.Text.Trim());
                }
            };

            // Khởi tạo ComboBox trạng thái
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add(new { Text = "Chưa xử lý", Value = "ChuaXuLy" });
            cboTrangThai.Items.Add(new { Text = "Đang xử lý", Value = "DangXuLy" });
            cboTrangThai.Items.Add(new { Text = "Đã xử lý", Value = "DaXuLy" });
            cboTrangThai.DisplayMember = "Text";
            cboTrangThai.ValueMember = "Value";

            this.Load += (s, e) => LoadData();
        }

        public override void LoadData() { LoadHistory(); }

        public void SetMaDon(string maDon)
        {
            _maDon = (maDon ?? "").Trim();
            txtMaDon.Text = _maDon;
            lblSubTitle.Text = string.IsNullOrEmpty(_maDon) ? "Đang xem toàn bộ phản hồi" : "Phản hồi cho đơn: " + _maDon;
            LoadHistory();
            txtNoiDung.Clear();
            txtNoiDung.Focus();
        }

        private void LoadHistory()
        {
            try
            {
                dgvPhanHoi.AutoGenerateColumns = true;
                dgvPhanHoi.DataSource = _phRepo.LayDanhSach(_maDon);
                FormatGrid();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải lịch sử phản hồi: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvPhanHoi.Columns.Count == 0) return;
            
            var visibleCols = new List<string> { "MaPH", "TenKH", "NoiDung", "NgayGhi", "TrangThaiDisplay" };
            foreach (DataGridViewColumn col in dgvPhanHoi.Columns) 
            { 
                if (visibleCols.Contains(col.Name)) col.Visible = true;
                else col.Visible = false;
            }

            dgvPhanHoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            dgvPhanHoi.Columns["MaPH"].HeaderText = "Mã PH";
            dgvPhanHoi.Columns["MaPH"].FillWeight = 80;
            
            dgvPhanHoi.Columns["TenKH"].HeaderText = "Khách hàng";
            dgvPhanHoi.Columns["TenKH"].FillWeight = 120;
            
            dgvPhanHoi.Columns["NoiDung"].HeaderText = "Nội dung";
            dgvPhanHoi.Columns["NoiDung"].FillWeight = 200;
            
            dgvPhanHoi.Columns["NgayGhi"].HeaderText = "Ngày ghi";
            dgvPhanHoi.Columns["NgayGhi"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvPhanHoi.Columns["NgayGhi"].FillWeight = 100;
            
            if (dgvPhanHoi.Columns.Contains("TrangThaiDisplay"))
            {
                dgvPhanHoi.Columns["TrangThaiDisplay"].HeaderText = "Trạng thái";
                dgvPhanHoi.Columns["TrangThaiDisplay"].FillWeight = 100;
                dgvPhanHoi.Columns["TrangThaiDisplay"].Visible = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string maDonInput = txtMaDon.Text.Trim();
            if (string.IsNullOrEmpty(maDonInput))
            {
                ShowWarning("Vui lòng nhập mã đơn hàng!");
                return;
            }

            string noiDung = txtNoiDung.Text.Trim();
            if (string.IsNullOrEmpty(noiDung))
            {
                ShowWarning("Vui lòng nhập nội dung phản hồi!");
                return;
            }

            try
            {
                var dhRepo = new DonHangRepository();
                var dh = dhRepo.LayThongTinDon(maDonInput);
                if (dh == null)
                {
                    ShowWarning("Không tìm thấy đơn hàng: " + maDonInput);
                    return;
                }

                _phRepo.GhiNhan(maDonInput, noiDung);
                ShowSuccess("Đã ghi nhận phản hồi thành công!");
                txtNoiDung.Clear();
                LoadHistory();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi lưu phản hồi: " + ex.Message);
            }
        }

        private void dgvPhanHoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var phList = dgvPhanHoi.DataSource as List<PhanHoi>;
            if (phList == null || e.RowIndex >= phList.Count) return;

            var ph = phList[e.RowIndex];
            lblMaPH.Text = "Mã phản hồi: " + ph.MaPH;
            txtKetQua.Text = ph.KetQuaXuLy;

            // Set combobox
            foreach (var item in cboTrangThai.Items)
            {
                if (((dynamic)item).Value == ph.TrangThaiXuLy)
                {
                    cboTrangThai.SelectedItem = item;
                    break;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lblMaPH.Text.Contains("..."))
            {
                ShowWarning("Vui lòng chọn một phản hồi từ danh sách bên phải!");
                return;
            }

            string maPH = lblMaPH.Text.Replace("Mã phản hồi: ", "");
            string trangThai = ((dynamic)cboTrangThai.SelectedItem).Value;
            string ketQua = txtKetQua.Text.Trim();

            try
            {
                _phRepo.CapNhatXuLy(maPH, trangThai, ketQua);
                ShowSuccess("Cập nhật kết quả xử lý thành công!");
                LoadHistory();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi cập nhật: " + ex.Message);
            }
        }
    }
}
