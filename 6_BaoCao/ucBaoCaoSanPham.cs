using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoSanPham : UserControl
    {
        public ucBaoCaoSanPham()
        {
            InitializeComponent();
        }

        private void ucBaoCaoSanPham_Load(object sender, EventArgs e)
        {
            // Set default month/year
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            
            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
            for (int i = currentYear - 2; i <= currentYear; i++) cboNam.Items.Add(i);
            
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

                DataTable dt = BaoCaoDAO.SanPhamBanChay(thang, nam);
                dgvSanPham.DataSource = dt;

                if (dgvSanPham.Columns.Count > 0)
                {
                    dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    dgvSanPham.Columns["TongSL"].HeaderText = "Số lượng đã bán";
                    dgvSanPham.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";
                    dgvSanPham.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu sản phẩm: " + ex.Message);
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
