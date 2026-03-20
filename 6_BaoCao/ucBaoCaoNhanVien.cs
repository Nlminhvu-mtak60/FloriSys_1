using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoNhanVien : UserControl
    {
        public ucBaoCaoNhanVien()
        {
            InitializeComponent();
        }

        private void ucBaoCaoNhanVien_Load(object sender, EventArgs e)
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
            for (int i = currentYear - 1; i <= currentYear; i++) cboNam.Items.Add(i);
            cboThang.SelectedItem = currentMonth;
            cboNam.SelectedItem = currentYear;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                int? thang = cboThang.SelectedItem as int?;
                int? nam = cboNam.SelectedItem as int?;
                DataTable dt = BaoCaoDAO.HieuSuatNhanVien(thang, nam);
                dgvNhanVien.DataSource = dt;

                if (dgvNhanVien.Columns.Count > 0)
                {
                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên nhân viên";
                    dgvNhanVien.Columns["SoDonHang"].HeaderText = "Số đơn hàng";
                    dgvNhanVien.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";
                    dgvNhanVien.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hiệu suất nhân viên: " + ex.Message);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
