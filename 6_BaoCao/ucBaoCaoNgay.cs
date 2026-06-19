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

                chartDoanhThu.Series["DoanhThu"].Points.Clear();
                int index = 0;
                foreach (TopSanPhamNgay sp in _topSP)
                {
                    if (sp.DoanhThu > 0)
                    {
                        chartDoanhThu.Series["DoanhThu"].Points.AddXY(sp.TenSP, sp.DoanhThu);
                        chartDoanhThu.Series["DoanhThu"].Points[index].ToolTip = sp.TenSP + ": " + sp.DoanhThu.ToString("N0") + "đ";
                        index++;
                    }
                }
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
            if (chartDoanhThu.Series["DoanhThu"].Points.Count > 0)
            {
                chartStream = new System.IO.MemoryStream();
                chartDoanhThu.SaveImage(chartStream, ChartImageFormat.Png);
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
