using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using System.Windows.Forms.DataVisualization.Charting;

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

                // Vẽ biểu đồ Pie 3D cho Top Sản Phẩm
                pnlChartMock.Controls.Clear();
                Chart chart = new Chart();
                chart.Dock = DockStyle.Fill;
                chart.BackColor = Color.White;
                
                ChartArea area = new ChartArea("MainArea");
                area.Area3DStyle.Enable3D = true;
                area.Area3DStyle.Inclination = 45;
                area.Area3DStyle.Rotation = 0;
                chart.ChartAreas.Add(area);
                
                Series series = new Series("TopSP");
                series.ChartType = SeriesChartType.Pie;
                series.IsValueShownAsLabel = true;
                series.Label = "#VALX: #PERCENT{P0}";
                series.Font = new Font("Segoe UI", 8f, FontStyle.Regular);
                series["PieLabelStyle"] = "Outside";
                series["PieLineColor"] = "Gray";
                series.Palette = ChartColorPalette.Pastel;
                chart.Series.Add(series);
                
                int index = 0;
                foreach (DataRow row in dtTopSP.Rows)
                {
                    string name = row["TenSP"].ToString();
                    decimal val = Convert.ToDecimal(row["DoanhThu"]);
                    if (val > 0)
                    {
                        int ptIdx = series.Points.AddXY(name, val);
                        if (index == 0) // Explode the top product
                        {
                            series.Points[ptIdx].CustomProperties = "Exploded=true";
                        }
                        index++;
                    }
                }

                Title title = new Title("TỶ TRỌNG DOANH THU SẢN PHẨM", Docking.Top, new Font("Segoe UI", 10f, FontStyle.Bold), Color.FromArgb(64, 64, 64));
                chart.Titles.Add(title);
                
                pnlChartMock.Controls.Add(chart);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu báo cáo: " + ex.Message);
            }
        }
    }
}
