using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoSanPham : UserControl
    {
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

        private void LoadData()
        {
            try
            {
                int? thang = cboThang.SelectedItem as int?;
                int? nam = cboNam.SelectedItem as int?;

                List<SanPhamBanChay> dsSP = BaoCaoDAO.SanPhamBanChay(thang, nam);
                dgvSanPham.DataSource = dsSP;

                if (dgvSanPham.Columns.Count > 0)
                {
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
                MessageBox.Show("Lỗi tải dữ liệu sản phẩm: " + ex.Message);
            }
        }

        private void DrawPieChart(List<SanPhamBanChay> dsSP)
        {
            string chartName = "chartSP";
            Control existing = pnlGridCard.Controls[chartName];
            if (existing != null) pnlGridCard.Controls.Remove(existing);

            if (dsSP.Count == 0) return;

            Chart chart = new Chart();
            chart.Name = chartName;
            chart.Size = new Size(280, 200);
            chart.Location = new Point(pnlGridCard.Width - 310, 20);
            chart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chart.BackColor = Color.White;

            ChartArea area = new ChartArea("Main");
            area.BackColor = Color.White;
            area.Area3DStyle.Enable3D = true;
            area.Area3DStyle.Inclination = 50;
            chart.ChartAreas.Add(area);

            Series series = new Series("SP");
            series.ChartType = SeriesChartType.Pie;
            series.Label = "#PERCENT{P0}";
            series.Font = new Font("Segoe UI", 7f);
            series["PieLabelStyle"] = "Outside";
            series.Palette = ChartColorPalette.Pastel;
            chart.Series.Add(series);

            int count = 0;
            foreach (SanPhamBanChay sp in dsSP)
            {
                if (count >= 5) break;
                if (sp.TongDoanhThu > 0)
                {
                    int idx = series.Points.AddXY(sp.TenSP, sp.TongDoanhThu);
                    if (count == 0) series.Points[idx].CustomProperties = "Exploded=true";
                }
                count++;
            }

            pnlGridCard.Controls.Add(chart);
            chart.BringToFront();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
