using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Shared;
using FloriSys.Models;
using System.Windows.Forms.DataVisualization.Charting;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoNgay : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();
        public ucBaoCaoNgay()
        {
            InitializeComponent();
        }

        private void ucBaoCaoNgay_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                DateTime today = DateTime.Now;
                lblDate.Text = today.ToString("dddd, dd/MM/yyyy");

                // Load KPIs
                ThongKeDashboard stats = _bcRepo.ThongKeDashboard();
                if (stats != null)
                {
                    lblTongDonValue.Text = stats.DonHomNay.ToString();
                    lblDoanhThuValue.Text = stats.DoanhThuHomNay.ToString("N0") + "đ";
                }

                int slSP = _bcRepo.SoLuongSanPhamBanNgay(today);
                lblSoLuongSPValue.Text = slSP.ToString();

                // Load Top Products
                List<TopSanPhamNgay> dsTopSP = _bcRepo.TopSanPhamNgay(today);
                dgvTopSP.DataSource = dsTopSP;
                if (dgvTopSP.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "TenSP", "SLBan", "DoanhThu" };
                    foreach (DataGridViewColumn col in dgvTopSP.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

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
                foreach (TopSanPhamNgay sp in dsTopSP)
                {
                    if (sp.DoanhThu > 0)
                    {
                        int ptIdx = series.Points.AddXY(sp.TenSP, sp.DoanhThu);
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
                ShowError("Lỗi tải dữ liệu báo cáo: " + ex.Message);
            }
        }
    }
}
