using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;
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
        }

        public override void LoadData() { LoadHistory(); }

        public void SetMaDon(string maDon)
        {
            _maDon = maDon;
            txtMaDon.Text = _maDon;
            lblSubTitle.Text = "Đơn hàng " + _maDon;
            LoadHistory();
        }

        private void LoadHistory()
        {
            try
            {
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
            foreach (DataGridViewColumn col in dgvPhanHoi.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

            dgvPhanHoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            dgvPhanHoi.Columns["MaPH"].HeaderText = "Mã PH";
            dgvPhanHoi.Columns["MaPH"].FillWeight = 80;
            
            dgvPhanHoi.Columns["MaDon"].Visible = false;
            
            dgvPhanHoi.Columns["TenKH"].HeaderText = "Khách hàng";
            dgvPhanHoi.Columns["TenKH"].FillWeight = 120;
            
            dgvPhanHoi.Columns["NoiDung"].HeaderText = "Nội dung";
            dgvPhanHoi.Columns["NoiDung"].FillWeight = 200;
            
            dgvPhanHoi.Columns["NgayGhi"].HeaderText = "Ngày ghi";
            dgvPhanHoi.Columns["NgayGhi"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dgvPhanHoi.Columns["NgayGhi"].FillWeight = 100;
            
            dgvPhanHoi.Columns["TrangThaiXuLy"].Visible = false;
            
            if (dgvPhanHoi.Columns.Contains("TrangThaiDisplay"))
            {
                dgvPhanHoi.Columns["TrangThaiDisplay"].HeaderText = "Trạng thái";
                dgvPhanHoi.Columns["TrangThaiDisplay"].FillWeight = 100;
            }
            
            dgvPhanHoi.Columns["KetQuaXuLy"].HeaderText = "Kết quả";
            dgvPhanHoi.Columns["KetQuaXuLy"].FillWeight = 150;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_maDon))
            {
                ShowWarning("Vui lòng nhập mã đơn hàng trước!");
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
                // Kiểm tra đơn hàng tồn tại
                var dhRepo = new DonHangRepository();
                var dh = dhRepo.LayThongTinDon(_maDon);
                if (dh == null)
                {
                    ShowWarning("Không tìm thấy đơn hàng: " + _maDon);
                    return;
                }

                _phRepo.GhiNhan(_maDon, noiDung);
                ShowSuccess("Đã ghi nhận phản hồi thành công!");
                txtNoiDung.Clear();
                LoadHistory();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi lưu phản hồi: " + ex.Message);
            }
        }
    }
}
