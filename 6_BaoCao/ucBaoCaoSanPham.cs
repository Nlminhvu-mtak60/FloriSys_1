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

                if (dgvSanPham.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "TenSP", "TongSoLuong", "TongDoanhThu" };
                    foreach (DataGridViewColumn col in dgvSanPham.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                    dgvSanPham.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    dgvSanPham.Columns["TongSoLuong"].HeaderText = "Số lượng đã bán";
                    dgvSanPham.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";
                    dgvSanPham.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";

                    // Add percentage column if not exists
                    if (!dgvSanPham.Columns.Contains("colTyTrong"))
                    {
                        DataGridViewTextBoxColumn colPercent = new DataGridViewTextBoxColumn();
                        colPercent.Name = "colTyTrong";
                        colPercent.HeaderText = "Tỷ trọng";
                        colPercent.Width = 120;
                        dgvSanPham.Columns.Add(colPercent);
                    }

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
                DrawPieChart(dsSP);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải dữ liệu sản phẩm: " + ex.Message);
            }
        }

        private void DrawPieChart(List<SanPhamBanChay> dsSP)
        {
            pnlChartArea.Controls.Clear();

            if (dsSP.Count == 0) return;

            // Sắp xếp theo doanh thu giảm dần để top 5 luôn là sản phẩm doanh thu cao nhất
            dsSP = new List<SanPhamBanChay>(dsSP);
            dsSP.Sort((a, b) => b.TongDoanhThu.CompareTo(a.TongDoanhThu));

            Chart chart = new Chart();
            chart.Name = "chartSP";
            chart.Dock = DockStyle.Fill;
            chart.BackColor = Color.White;

            ChartArea area = new ChartArea("Main");
            area.BackColor = Color.White;
            area.Area3DStyle.Enable3D = true;
            area.Area3DStyle.Inclination = 50;
            chart.ChartAreas.Add(area);

            Series series = new Series("SP");
            series.ChartType = SeriesChartType.Pie;
            series.Label = "#PERCENT{P0}";
            series.Font = new Font("Segoe UI", 8f);
            series["PieLabelStyle"] = "Outside";
            series["PieLineColor"] = "Gray";
            series.Palette = ChartColorPalette.Pastel;
            series.LegendText = "#VALX";
            chart.Series.Add(series);

            int count = 0;
            decimal doanhThuKhac = 0;

            foreach (SanPhamBanChay sp in dsSP)
            {
                if (sp.TongDoanhThu > 0)
                {
                    if (count < 5)
                    {
                        int idx = series.Points.AddXY(sp.TenSP, sp.TongDoanhThu);
                        if (count == 0) series.Points[idx].CustomProperties = "Exploded=true";
                    }
                    else
                    {
                        doanhThuKhac += sp.TongDoanhThu;
                    }
                    count++;
                }
            }

            if (doanhThuKhac > 0)
            {
                series.Points.AddXY("Khác", doanhThuKhac);
            }

            Title title = new Title("TỶ TRỌNG DOANH THU", Docking.Top, new Font("Segoe UI", 10f, FontStyle.Bold), Color.FromArgb(64, 64, 64));
            chart.Titles.Add(title);

            Legend legend = new Legend("MainLegend");
            legend.Docking = Docking.Bottom;
            legend.Alignment = StringAlignment.Center;
            legend.Font = new Font("Segoe UI", 8f);
            chart.Legends.Add(legend);

            pnlChartArea.Controls.Add(chart);
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
