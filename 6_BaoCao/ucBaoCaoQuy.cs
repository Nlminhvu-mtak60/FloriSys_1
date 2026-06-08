using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FloriSys.DataAccess;
using FloriSys.Shared;
using FloriSys.Models;
using FloriSys.Services;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoQuy : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();

        // Cache data for PDF export
        private BaoCaoDoanhThu _currentDT;
        private BaoCaoDoanhThu _prevDT;
        private List<DoanhThuThang> _dsThang;
        private List<SanPhamBanChay> _topSP;

        public ucBaoCaoQuy()
        {
            InitializeComponent();
        }

        private void ucBaoCaoQuy_Load(object sender, EventArgs e)
        {
            // Auto-select current quarter
            int currentQuarter = (DateTime.Now.Month - 1) / 3;
            cboQuy.SelectedIndex = currentQuarter;
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
                if (cboQuy.SelectedIndex < 0) return;

                int quy = cboQuy.SelectedIndex + 1; // 1..4
                int nam = (int)nudNam.Value;

                // === Biểu đồ doanh thu theo tháng trong quý ===
                // We call DrawChart first to populate _dsThang
                DrawChart(quy, nam);

                // === KPI: Doanh thu quý hiện tại ===
                _currentDT = _bcRepo.DoanhThuQuy(quy, nam);
                if (_currentDT != null)
                {
                    // Fix mismatch by explicitly summing _dsThang to exclude HoanHang reliably
                    if (_dsThang != null && _dsThang.Count > 0)
                    {
                        _currentDT.TongDoanhThu = 0;
                        foreach (var dt in _dsThang) _currentDT.TongDoanhThu += dt.DoanhThu;
                        
                        _currentDT.TongDon = 0;
                        foreach (var dt in _dsThang) _currentDT.TongDon += dt.SoDon;
                    }

                    lblDoanhThuValue.Text = _currentDT.TongDoanhThu.ToString("N0") + "đ";
                    lblSoDonValue.Text = _currentDT.TongDon.ToString();
                    decimal tbThang = _currentDT.TongDoanhThu / 3;
                    lblTBThangValue.Text = tbThang.ToString("N0") + "đ";
                }
                else
                {
                    lblDoanhThuValue.Text = "0đ";
                    lblSoDonValue.Text = "0";
                    lblTBThangValue.Text = "0đ";
                }

                // === So sánh với quý trước ===
                int quyTruoc = quy == 1 ? 4 : quy - 1;
                int namTruoc = quy == 1 ? nam - 1 : nam;
                _prevDT = _bcRepo.DoanhThuQuy(quyTruoc, namTruoc);
                
                // Fetch prev _dsThang to fix mismatch for previous quarter
                var prevDsThang = _bcRepo.DoanhThuTheoThangTrongQuy(quyTruoc, namTruoc);
                if (_prevDT != null && prevDsThang != null && prevDsThang.Count > 0)
                {
                    _prevDT.TongDoanhThu = 0;
                    foreach (var dt in prevDsThang) _prevDT.TongDoanhThu += dt.DoanhThu;
                    
                    _prevDT.TongDon = 0;
                    foreach (var dt in prevDsThang) _prevDT.TongDon += dt.SoDon;
                }

                if (_currentDT != null && _prevDT != null && _prevDT.TongDoanhThu > 0)
                {
                    decimal phanTram = ((_currentDT.TongDoanhThu - _prevDT.TongDoanhThu) / _prevDT.TongDoanhThu) * 100;
                    lblCompareValue.Text = (phanTram >= 0 ? "▲ +" : "▼ ") + phanTram.ToString("N1") + "% so với quý trước (Q" + quyTruoc + "/" + namTruoc + ")";
                    lblCompareValue.ForeColor = phanTram >= 0 ? Color.FromArgb(45, 106, 79) : Color.FromArgb(220, 38, 38);
                }
                else
                {
                    lblCompareValue.Text = "Chưa có dữ liệu quý trước để so sánh";
                    lblCompareValue.ForeColor = Color.Gray;
                }

                // === Top sản phẩm quý ===
                try
                {
                    _topSP = _bcRepo.SanPhamBanChayQuy(quy, nam);
                }
                catch { 
                    _topSP = new List<SanPhamBanChay>();
                }

                dgvTopSP.DataSource = _topSP;
                if (dgvTopSP.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "TenSP", "TongSoLuong", "TongDoanhThu" };
                    foreach (DataGridViewColumn col in dgvTopSP.Columns)
                    {
                        if (!visibleCols.Contains(col.Name)) col.Visible = false;
                    }
                    if (dgvTopSP.Columns.Contains("TenSP")) dgvTopSP.Columns["TenSP"].HeaderText = "Sản phẩm";
                    if (dgvTopSP.Columns.Contains("TongSoLuong")) dgvTopSP.Columns["TongSoLuong"].HeaderText = "SL bán";
                    if (dgvTopSP.Columns.Contains("TongDoanhThu"))
                    {
                        dgvTopSP.Columns["TongDoanhThu"].HeaderText = "Doanh thu";
                        dgvTopSP.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                    }
                }

            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải báo cáo quý: " + ex.Message);
            }
        }

        private void DrawChart(int quy, int nam)
        {
            pnlChartMock.Controls.Clear();

            Chart chart = new Chart();
            chart.Dock = DockStyle.Fill;
            chart.BackColor = Color.White;

            ChartArea area = new ChartArea("MainArea");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9f);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8f);
            area.AxisY.LabelStyle.Format = "N0";
            area.AxisX.Title = "Tháng";
            area.AxisY.Title = "Doanh thu (đ)";
            area.AxisX.TitleFont = new Font("Segoe UI", 9f);
            area.AxisY.TitleFont = new Font("Segoe UI", 9f);
            area.AxisX.Interval = 1;
            chart.ChartAreas.Add(area);

            Series series = new Series("DoanhThu");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.FromArgb(232, 57, 77);
            series.BorderWidth = 0;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "N0";
            series.Font = new Font("Segoe UI", 7f);
            series.IsVisibleInLegend = false;
            chart.Series.Add(series);

            try
            {
                _dsThang = _bcRepo.DoanhThuTheoThangTrongQuy(quy, nam);
                foreach (DoanhThuThang item in _dsThang)
                {
                    int idx = series.Points.AddXY("Tháng " + item.Thang, item.DoanhThu);
                    if (item.DoanhThu == 0) series.Points[idx].Color = Color.FromArgb(229, 231, 235);
                }
            }
            catch
            {
                // Fallback: 3 empty months
                int thangDau = (quy - 1) * 3 + 1;
                for (int i = 0; i < 3; i++)
                {
                    series.Points.AddXY("Tháng " + (thangDau + i), 0);
                }
            }

            Title title = new Title("DOANH THU THEO THÁNG TRONG QUÝ " + quy,
                Docking.Top, new Font("Segoe UI", 10f, FontStyle.Bold), Color.FromArgb(64, 64, 64));
            chart.Titles.Add(title);

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
                ShowWarning("Vui lòng xem báo cáo trước khi xuất!");
                return;
            }

            int quy = cboQuy.SelectedIndex + 1;
            int nam = (int)nudNam.Value;

            // Capture chart as image
            System.IO.MemoryStream chartStream = null;
            if (pnlChartMock.Controls.Count > 0 && pnlChartMock.Controls[0] is Chart chart)
            {
                chartStream = new System.IO.MemoryStream();
                chart.SaveImage(chartStream, ChartImageFormat.Png);
            }

            FloriSys.Services.ReportPdfHelper.ExportBaoCaoQuy(quy, nam, _currentDT, _prevDT, _dsThang, _topSP, chartStream);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (_currentDT == null)
            {
                ShowWarning("Vui lòng xem báo cáo trước khi xuất!");
                return;
            }

            int quy = cboQuy.SelectedIndex + 1;
            int nam = (int)nudNam.Value;

            FloriSys.Services.ReportExcelHelper.ExportBaoCaoQuyExcel(quy, nam, _currentDT, _prevDT, _dsThang, _topSP);
        }

        private void cboQuy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
