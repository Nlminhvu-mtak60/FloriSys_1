using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoNgay : UserControl
    {
        public ucBaoCaoNgay()
        {
            InitializeComponent();
        }

        private void ucBaoCaoNgay_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DateTime today = DateTime.Now;
                lblDate.Text = today.ToString("dddd, dd/MM/yyyy");

                // Load KPIs
                DataTable dtStats = BaoCaoDAO.ThongKeDashboard();
                if (dtStats.Rows.Count > 0)
                {
                    lblTongDonValue.Text = dtStats.Rows[0]["DonHomNay"].ToString();
                    decimal doanhThu = Convert.ToDecimal(dtStats.Rows[0]["DoanhThuHomNay"]);
                    lblDoanhThuValue.Text = doanhThu.ToString("N0") + "đ";
                }

                DataTable dtSL = BaoCaoDAO.SoLuongSanPhamBanNgay(today);
                if (dtSL.Rows.Count > 0)
                {
                    lblSoLuongSPValue.Text = dtSL.Rows[0]["TongSP"].ToString();
                }

                // Load Top Products
                DataTable dtTopSP = BaoCaoDAO.TopSanPhamNgay(today);
                dgvTopSP.DataSource = dtTopSP;
                if (dgvTopSP.Columns.Count > 0)
                {
                    dgvTopSP.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    dgvTopSP.Columns["SLBan"].HeaderText = "SL bán";
                    dgvTopSP.Columns["DoanhThu"].HeaderText = "Doanh thu";
                    dgvTopSP.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
                }

                // Mock Chart (Optional: adjust bar heights based on data if available)
                // In a real app, we'd use a Chart control or custom drawing.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu báo cáo: " + ex.Message);
            }
        }
    }
}
