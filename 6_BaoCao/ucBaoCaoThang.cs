using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FloriSys.DataAccess;
using FloriSys.Shared;
using FloriSys.Models;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoThang : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();

        // Data cache for PDF
        private BaoCaoDoanhThu _currentDT;
        private BaoCaoDoanhThu _prevDT;
        private List<SanPhamBanChay> _topSP;

        public ucBaoCaoThang()
        {
            InitializeComponent();
        }

        private void ucBaoCaoThang_Load(object sender, EventArgs e)
        {
            if (cboThang.Items.Count > 0)
                cboThang.SelectedIndex = DateTime.Now.Month - 1;
            nudNam.Value = DateTime.Now.Year;

            LoadData();
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                int thang = cboThang.SelectedIndex >= 0 ? cboThang.SelectedIndex + 1 : DateTime.Now.Month;
                int nam = (int)nudNam.Value > 0 ? (int)nudNam.Value : DateTime.Now.Year;
                //lblMonth.Text = "Tháng " + thang + "/" + nam;

                // KPI - Doanh thu tháng
                // KPI - Doanh thu tháng
                _currentDT = _bcRepo.DoanhThuThang(thang, nam);
                if (_currentDT != null)
                {
                    lblDoanhThuValue.Text = _currentDT.TongDoanhThu.ToString("N0") + "đ";
                }

                // So sánh với tháng trước
                int thangTruoc = thang == 1 ? 12 : thang - 1;
                int namTruoc = thang == 1 ? nam - 1 : nam;
                _prevDT = _bcRepo.DoanhThuThang(thangTruoc, namTruoc);
                if (_currentDT != null && _prevDT != null)
                {
                    if (_prevDT.TongDoanhThu > 0)
                    {
                        decimal phanTram = ((_currentDT.TongDoanhThu - _prevDT.TongDoanhThu) / _prevDT.TongDoanhThu) * 100;
                        lblCompareValue.Text = (phanTram >= 0 ? "+" : "") + phanTram.ToString("N1") + "% so với tháng trước";
                        lblCompareValue.ForeColor = phanTram >= 0 ? Color.FromArgb(45, 106, 79) : Color.FromArgb(220, 38, 38);
                    }
                    else
                    {
                        lblCompareValue.Text = "Chưa có dữ liệu tháng trước";
                        lblCompareValue.ForeColor = Color.Gray;
                    }
                }

                // Top sản phẩm tháng
                _topSP = _bcRepo.SanPhamBanChay(thang, nam);
                dgvTopSP.DataSource = _topSP;
                if (dgvTopSP.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "TenSP", "TongSoLuong", "TongDoanhThu" };
                    foreach (DataGridViewColumn col in dgvTopSP.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                    dgvTopSP.Columns["TenSP"].HeaderText = "Sản phẩm";
                    dgvTopSP.Columns["TongSoLuong"].HeaderText = "SL bán";
                    dgvTopSP.Columns["TongDoanhThu"].HeaderText = "Doanh thu";
                    dgvTopSP.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                }

                // Biểu đồ cột doanh thu theo ngày
                DrawChart(thang, nam);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải báo cáo tháng: " + ex.Message);
            }
        }

        private void DrawChart(int thang, int nam)
        {
            pnlChartMock.Controls.Clear();

            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;
            chart.BackColor = Color.White;

            ChartArea area = new ChartArea("MainArea");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 7f);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 7f);
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisX.Title = "Ngày";
            area.AxisY.Title = "Doanh thu (đ)";
            area.AxisX.TitleFont = new Font("Segoe UI", 8f);
            area.AxisY.TitleFont = new Font("Segoe UI", 8f);
            chart.ChartAreas.Add(area);

            Series series = new Series("DoanhThu");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(232, 57, 77);
            series.BorderWidth = 0;
            series.IsVisibleInLegend = false;
            chart.Series.Add(series);

            try
            {
                List<DoanhThuNgay> dsNgay = _bcRepo.DoanhThuTheoNgayTrongThang(thang, nam);
                foreach (DoanhThuNgay item in dsNgay)
                {
                    int idx = series.Points.AddXY(item.Ngay.Day, item.DoanhThu);
                    if (item.DoanhThu == 0) series.Points[idx].Color = Color.FromArgb(229, 231, 235);
                }
            }
            catch
            {
                // SP chưa tồn tại, vẽ placeholder
                int soNgay = DateTime.DaysInMonth(nam, thang);
                Random rnd = new Random(42);
                for (int i = 1; i <= soNgay; i++)
                {
                    series.Points.AddXY(i, rnd.Next(100000, 2000000));
                }
            }

            Legend legend = new Legend("MainLegend");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.Font = new Font("Segoe UI", 8f);

            LegendItem itemCoDoanhThu = new LegendItem();
            itemCoDoanhThu.Name = "Có doanh thu";
            itemCoDoanhThu.Color = Color.FromArgb(232, 57, 77);
            legend.CustomItems.Add(itemCoDoanhThu);

            LegendItem itemKhongDoanhThu = new LegendItem();
            itemKhongDoanhThu.Name = "Không có doanh thu";
            itemKhongDoanhThu.Color = Color.FromArgb(229, 231, 235);
            legend.CustomItems.Add(itemKhongDoanhThu);
            
            chart.Legends.Add(legend);

            pnlChartMock.Controls.Add(chart);
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            if (_currentDT == null)
            {
                ShowWarning("Vui lòng xem báo cáo trước khi xuất PDF!");
                return;
            }

            int thang = cboThang.SelectedIndex + 1;
            int nam = (int)nudNam.Value;

            // Capture chart as image
            System.IO.MemoryStream chartStream = null;
            if (pnlChartMock.Controls.Count > 0 && pnlChartMock.Controls[0] is Chart chart)
            {
                chartStream = new System.IO.MemoryStream();
                chart.SaveImage(chartStream, ChartImageFormat.Png);
            }

            FloriSys.Services.ReportPdfHelper.ExportBaoCaoThang(thang, nam, _currentDT, _prevDT, _topSP, chartStream);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (_currentDT == null)
            {
                ShowWarning("Vui lòng xem báo cáo trước khi xuất Excel!");
                return;
            }

            int thang = cboThang.SelectedIndex + 1;
            int nam = (int)nudNam.Value;
            FloriSys.Services.ReportExcelHelper.ExportBaoCaoThangExcel(thang, nam, _currentDT, _prevDT, _topSP);
        }
    }
}
