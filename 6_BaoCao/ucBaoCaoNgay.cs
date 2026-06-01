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

        // Data cache for PDF
        private BaoCaoDoanhThu _currentDT;
        private List<TopSanPhamNgay> _topSP;
        private int _slSP;

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
                    _currentDT = new BaoCaoDoanhThu { TongDon = stats.DonHomNay, TongDoanhThu = stats.DoanhThuHomNay };
                }
                else
                {
                    _currentDT = null;
                }

                _slSP = _bcRepo.SoLuongSanPhamBanNgay(today);
                lblSoLuongSPValue.Text = _slSP.ToString();

                // Top sản phẩm tháng
                _topSP = _bcRepo.TopSanPhamNgay(today);
                dgvTopSP.DataSource = _topSP;
                if (dgvTopSP.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "TenSP", "SLBan", "DoanhThu" };
                    foreach (DataGridViewColumn col in dgvTopSP.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                    dgvTopSP.Columns["TenSP"].HeaderText = "Sản phẩm";
                    dgvTopSP.Columns["SLBan"].HeaderText = "SL";
                    dgvTopSP.Columns["DoanhThu"].HeaderText = "Doanh thu";
                    dgvTopSP.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
                }

                pnlChartMock.Controls.Clear();
                Chart chart = new Chart();
                chart.Dock = DockStyle.Fill;
                chart.BackColor = Color.FromArgb(249, 250, 251);

                ChartArea area = new ChartArea("MainArea");
                area.BackColor = Color.FromArgb(249, 250, 251);
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(229, 231, 235);
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8f);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8f);
                chart.ChartAreas.Add(area);

                Series series = new Series("DoanhThu");
                series.ChartType = SeriesChartType.Doughnut;
                series["PieLabelStyle"] = "Disabled";
                series.LegendText = "#VALX";
                chart.Series.Add(series);
                
                int index = 0;
                foreach (TopSanPhamNgay sp in _topSP)
                {
                    if (sp.DoanhThu > 0)
                    {
                        series.Points.AddXY(sp.TenSP, sp.DoanhThu);
                        series.Points[index].ToolTip = sp.TenSP + ": " + sp.DoanhThu.ToString("N0") + "đ";
                        index++;
                    }
                }
                
                Title title = new Title("TỶ TRỌNG DOANH THU SẢN PHẨM", Docking.Top, new Font("Segoe UI", 10f, FontStyle.Bold), Color.FromArgb(64, 64, 64));
                chart.Titles.Add(title);
                
                Legend legend = new Legend("MainLegend");
                legend.Docking = Docking.Bottom;
                legend.Alignment = StringAlignment.Center;
                legend.Font = new Font("Segoe UI", 8f);
                chart.Legends.Add(legend);

                pnlChartMock.Controls.Add(chart);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải dữ liệu báo cáo: " + ex.Message);
            }
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            if (_currentDT == null)
            {
                ShowWarning("Chưa có dữ liệu để xuất PDF!");
                return;
            }

            // Capture chart as image
            System.IO.MemoryStream chartStream = null;
            if (pnlChartMock.Controls.Count > 0 && pnlChartMock.Controls[0] is Chart chart)
            {
                chartStream = new System.IO.MemoryStream();
                chart.SaveImage(chartStream, ChartImageFormat.Png);
            }

            FloriSys.Services.ReportPdfHelper.ExportBaoCaoNgay(DateTime.Now, _currentDT, _topSP, _slSP, chartStream);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (_currentDT == null)
            {
                ShowWarning("Chưa có dữ liệu để xuất Excel!");
                return;
            }

            FloriSys.Services.ReportExcelHelper.ExportBaoCaoNgayExcel(DateTime.Now, _currentDT, _topSP, _slSP);
        }
    }
}
