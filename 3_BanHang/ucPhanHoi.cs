using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._3_BanHang
{
    public partial class ucPhanHoi : UserControl
    {
        private string _maDon;

        public ucPhanHoi()
        {
            InitializeComponent();
        }

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
                dgvPhanHoi.DataSource = PhanHoiDAO.LayDanhSach(_maDon);
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lịch sử phản hồi: " + ex.Message);
            }
        }

        private void FormatGrid()
        {
            if (dgvPhanHoi.Columns.Count == 0) return;
            dgvPhanHoi.Columns["MaPH"].HeaderText = "Mã PH";
            dgvPhanHoi.Columns["MaDon"].Visible = false;
            dgvPhanHoi.Columns["TenKH"].HeaderText = "Khách hàng";
            dgvPhanHoi.Columns["NoiDung"].HeaderText = "Nội dung";
            dgvPhanHoi.Columns["NgayGhi"].HeaderText = "Ngày ghi";
            dgvPhanHoi.Columns["TrangThaiXuLy"].HeaderText = "Trạng thái";
            dgvPhanHoi.Columns["KetQuaXuLy"].HeaderText = "Kết quả";

            dgvPhanHoi.Columns["NgayGhi"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string noiDung = txtNoiDung.Text.Trim();
            if (string.IsNullOrEmpty(noiDung))
            {
                MessageBox.Show("Vui lòng nhập nội dung phản hồi!");
                return;
            }

            try
            {
                PhanHoiDAO.GhiNhan(_maDon, noiDung);
                MessageBox.Show("Đã ghi nhận phản hồi thành công!", "Thông báo");
                txtNoiDung.Clear();
                LoadHistory();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu phản hồi: " + ex.Message);
            }
        }
    }
}
