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
    public partial class ucBaoCaoThang : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();

        // Data cache for PDF
        private BaoCaoDoanhThu _currentDT;
        private BaoCaoDoanhThu _prevDT;
        private List<DoanhThuNgay> _dsNgay;
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

                // === Biểu đồ cột doanh thu theo ngày ===
                // Call this first to populate _dsNgay
                DrawChart(thang, nam);

                // KPI - Doanh thu tháng
                _currentDT = _bcRepo.DoanhThuThang(thang, nam);
                if (_currentDT != null)
                {
                    // Fix mismatch by explicitly summing _dsNgay to exclude HoanHang reliably
                    if (_dsNgay != null && _dsNgay.Count > 0)
                    {
                        _currentDT.TongDoanhThu = 0;
                        foreach (var dt in _dsNgay) _currentDT.TongDoanhThu += dt.DoanhThu;
                        
                        _currentDT.TongDon = 0;
                        foreach (var dt in _dsNgay) _currentDT.TongDon += dt.SoDon;
                    }

                    lblDoanhThuValue.Text = _currentDT.TongDoanhThu.ToString("N0") + "đ";
                }

                // So sánh với tháng trước
                int thangTruoc = thang == 1 ? 12 : thang - 1;
                int namTruoc = thang == 1 ? nam - 1 : nam;
                _prevDT = _bcRepo.DoanhThuThang(thangTruoc, namTruoc);
                
                // Fetch prev _dsNgay to fix mismatch for previous month
                var prevDsNgay = _bcRepo.DoanhThuTheoNgayTrongThang(thangTruoc, namTruoc);
                if (_prevDT != null && prevDsNgay != null && prevDsNgay.Count > 0)
                {
                    _prevDT.TongDoanhThu = 0;
                    foreach (var dt in prevDsNgay) _prevDT.TongDoanhThu += dt.DoanhThu;
                    
                    _prevDT.TongDon = 0;
                    foreach (var dt in prevDsNgay) _prevDT.TongDon += dt.SoDon;
                }

                if (_currentDT != null && _prevDT != null)
                {
                    if (_prevDT.TongDoanhThu > 0)
                    {
                        decimal phanTram = ((_currentDT.TongDoanhThu - _prevDT.TongDoanhThu) / _prevDT.TongDoanhThu) * 100;
                        lblCompareValue.Text = (phanTram >= 0 ? "+" : "") + phanTram.ToString("N1") + "% So với tháng trước";
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

                var visibleCols = new List<string> { "TenSP", "TongSoLuong", "TongDoanhThu" };
                var headers = new Dictionary<string, string>
                {
                    { "TenSP", "Sản phẩm" },
                    { "TongSoLuong", "SL bán" },
                    { "TongDoanhThu", "Doanh thu" }
                };
                var formats = new Dictionary<string, string>
                {
                    { "TongDoanhThu", "N0" }
                };
                GridHelper.FormatGrid(dgvTopSP, visibleCols, headers, formats);

            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải báo cáo tháng: " + ex.Message);
            }
        }

        private void DrawChart(int thang, int nam)
        {
            try
            {
                _dsNgay = _bcRepo.DoanhThuTheoNgayTrongThang(thang, nam);
            }
            catch
            {
                _dsNgay = null;
            }
            ChartHelper.DrawRevenueColumnChart(pnlChartMock, _dsNgay, thang, nam);
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
            FloriSys.Services.ReportExcelHelper.ExportBaoCaoThangExcel(thang, nam, _currentDT, _prevDT, _topSP, _dsNgay);
        }
    }
}
