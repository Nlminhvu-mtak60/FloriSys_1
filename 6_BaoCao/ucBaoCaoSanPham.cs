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
    public partial class ucBaoCaoSanPham : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();
        public ucBaoCaoSanPham()
        {
            InitializeComponent();
        }


        private void ucBaoCaoSanPham_Load(object sender, EventArgs e)
        {

            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
            for (int i = currentYear - 2; i <= currentYear; i++) cboNam.Items.Add(i);

            cboThang.SelectedItem = currentMonth;
            cboNam.SelectedItem = currentYear;

            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                int? thang = cboThang.SelectedItem as int?;
                int? nam = cboNam.SelectedItem as int?;

                List<SanPhamBanChay> dsSP = _bcRepo.SanPhamBanChay(thang, nam);
                lblGridTitle.Text = "Danh sách sản phẩm";
                lblTitle.Text = "Sản phẩm bán chạy nhất";
                
                dgvSanPham.DataSource = dsSP;

                var visibleCols = new List<string> { "TenSP", "TongSoLuong", "TongDoanhThu" };
                var headers = new Dictionary<string, string>
                {
                    { "TenSP", "Tên sản phẩm" },
                    { "TongSoLuong", "Số lượng đã bán" },
                    { "TongDoanhThu", "Tổng doanh thu" }
                };
                var formats = new Dictionary<string, string>
                {
                    { "TongDoanhThu", "N0" }
                };
                GridHelper.FormatGrid(dgvSanPham, visibleCols, headers, formats);

                if (dsSP != null && dsSP.Count > 0)
                {
                    GridHelper.EnsureColumnExists(dgvSanPham, "colTyTrong", "Tỷ trọng");

                    // Calculate total revenue for percentage
                    decimal totalRevenue = 0;
                    foreach (SanPhamBanChay sp in dsSP)
                        totalRevenue += sp.TongDoanhThu;

                    // Set percentage values
                    for (int i = 0; i < dsSP.Count && i < dgvSanPham.Rows.Count; i++)
                    {
                        decimal percent = totalRevenue > 0 ? (dsSP[i].TongDoanhThu / totalRevenue) * 100 : 0;
                        dgvSanPham.Rows[i].Cells["colTyTrong"].Value = percent.ToString("N1") + "%";
                    }
                }

                // Draw chart
                ChartHelper.DrawProductPieChart(pnlChartArea, dsSP);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải dữ liệu sản phẩm: " + ex.Message);
            }
        }



        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            int? thang = cboThang.SelectedItem as int?;
            int? nam = cboNam.SelectedItem as int?;

            List<SanPhamBanChay> dsBanChay = _bcRepo.SanPhamBanChay(thang, nam);

            System.IO.MemoryStream chartStream = null;
            if (pnlChartArea.Controls.Count > 0 && pnlChartArea.Controls[0] is Chart chart)
            {
                chartStream = new System.IO.MemoryStream();
                chart.SaveImage(chartStream, ChartImageFormat.Png);
            }

            FloriSys.Services.ReportPdfHelper.ExportBaoCaoSanPham(thang, nam, dsBanChay, null, chartStream);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            int? thang = cboThang.SelectedItem as int?;
            int? nam = cboNam.SelectedItem as int?;

            List<SanPhamBanChay> dsBanChay = _bcRepo.SanPhamBanChay(thang, nam);

            FloriSys.Services.ReportExcelHelper.ExportBaoCaoSanPhamExcel(thang, nam, dsBanChay, null);
        }
    }
}
