using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoThang : UserControl
    {
        public ucBaoCaoThang()
        {
            InitializeComponent();
        }

        private void ucBaoCaoThang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                int thang = DateTime.Now.Month;
                int nam = DateTime.Now.Year;
                lblMonth.Text = "Tháng " + thang + "/" + nam;

                DataTable dtDoanhThu = BaoCaoDAO.DoanhThuThang(thang, nam);
                if (dtDoanhThu.Rows.Count > 0)
                {
                    decimal tongDT = 0;
                    foreach (DataRow row in dtDoanhThu.Rows)
                    {
                        tongDT += Convert.ToDecimal(row["DoanhThu"]);
                    }
                    lblDoanhThuValue.Text = tongDT.ToString("N0") + "đ";
                    
                    // Mock comparison
                    lblCompareValue.Text = "+12% so với tháng trước";
                    lblCompareValue.ForeColor = Color.FromArgb(45, 106, 79);
                }

                DataTable dtTopSP = BaoCaoDAO.SanPhamBanChay(thang, nam);
                dgvTopSP.DataSource = dtTopSP;
                if (dgvTopSP.Columns.Count > 0)
                {
                    dgvTopSP.Columns["TenSP"].HeaderText = "Sản phẩm";
                    dgvTopSP.Columns["TongSL"].HeaderText = "SL bán";
                    dgvTopSP.Columns["TongDoanhThu"].HeaderText = "Doanh thu";
                    dgvTopSP.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải báo cáo tháng: " + ex.Message);
            }
        }
    }
}
