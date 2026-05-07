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
    public partial class ucBaoCaoNhanVien : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();
        public ucBaoCaoNhanVien()
        {
            InitializeComponent();
        }

        private void ucBaoCaoNhanVien_Load(object sender, EventArgs e)
        {
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;
            for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
            for (int i = currentYear - 1; i <= currentYear; i++) cboNam.Items.Add(i);
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
                List<HieuSuatNhanVien> dsNV = _bcRepo.HieuSuatNhanVien(thang, nam);
                dgvNhanVien.DataSource = dsNV;

                if (dgvNhanVien.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "HoTen", "SoDonTao", "TongDoanhThu" };
                    foreach (DataGridViewColumn col in dgvNhanVien.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên nhân viên";
                    dgvNhanVien.Columns["SoDonTao"].HeaderText = "Số đơn hàng";
                    dgvNhanVien.Columns["TongDoanhThu"].HeaderText = "Tổng doanh thu";
                    dgvNhanVien.Columns["TongDoanhThu"].DefaultCellStyle.Format = "N0";
                }

                // Draw bar chart
                DrawBarChart(dsNV);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải hiệu suất nhân viên: " + ex.Message);
            }
        }

        private void DrawBarChart(List<HieuSuatNhanVien> dsNV)
        {
            pnlChartArea.Controls.Clear();

            if (dsNV.Count == 0) return;

            Chart chart = new Chart();
            chart.Name = "chartNV";
            chart.Dock = DockStyle.Fill;
            chart.BackColor = Color.White;

            ChartArea area = new ChartArea("Main");
            area.BackColor = Color.White;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(243, 244, 246);
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 7f);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 7f);
            area.AxisY.LabelStyle.Format = "N0";
            chart.ChartAreas.Add(area);

            // Revenue series
            Series sDT = new Series("Doanh thu");
            sDT.ChartType = SeriesChartType.Bar;
            sDT.Color = Color.FromArgb(232, 57, 77);
            sDT.Font = new Font("Segoe UI", 7f);
            chart.Series.Add(sDT);

            // Orders series
            Series sDon = new Series("Số đơn");
            sDon.ChartType = SeriesChartType.Bar;
            sDon.Color = Color.FromArgb(253, 186, 194);
            sDon.Font = new Font("Segoe UI", 7f);
            chart.Series.Add(sDon);

            Legend legend = new Legend();
            legend.Font = new Font("Segoe UI", 7f);
            legend.Docking = Docking.Bottom;
            chart.Legends.Add(legend);

            Title title = new Title("SO SÁNH HIỆU SUẤT NV", Docking.Top, new Font("Segoe UI", 10f, FontStyle.Bold), Color.FromArgb(31, 41, 55));
            chart.Titles.Add(title);

            int count = 0;
            foreach (HieuSuatNhanVien nv in dsNV)
            {
                if (count >= 8) break;
                string name = nv.HoTen;
                if (name.Length > 12) name = name.Substring(0, 12) + "…";
                sDT.Points.AddXY(name, nv.TongDoanhThu);
                sDon.Points.AddXY(name, nv.SoDonTao * 100000); // scale for visibility
                count++;
            }

            pnlChartArea.Controls.Add(chart);
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
