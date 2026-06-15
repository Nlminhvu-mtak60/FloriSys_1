using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FloriSys.Models;

namespace FloriSys.Services
{
    public static class ChartHelper
    {
        /// <summary>
        /// Vẽ biểu đồ cột (Column Chart) cho doanh thu theo ngày trong tháng.
        /// </summary>
        public static void DrawRevenueColumnChart(Panel parentPanel, List<DoanhThuNgay> dsNgay, int thang, int nam)
        {
            if (parentPanel == null) return;
            parentPanel.Controls.Clear();

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

            if (dsNgay != null)
            {
                foreach (DoanhThuNgay item in dsNgay)
                {
                    int idx = series.Points.AddXY(item.Ngay.Day, item.DoanhThu);
                    if (item.DoanhThu == 0) series.Points[idx].Color = Color.FromArgb(229, 231, 235);
                }
            }
            else
            {
                // Vẽ placeholder nếu chưa có dữ liệu
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

            parentPanel.Controls.Add(chart);
        }

        /// <summary>
        /// Vẽ biểu đồ tròn (Pie Chart 3D) thể hiện tỷ trọng doanh thu sản phẩm bán chạy.
        /// </summary>
        public static void DrawProductPieChart(Panel parentPanel, List<SanPhamBanChay> dsSP)
        {
            if (parentPanel == null) return;
            parentPanel.Controls.Clear();

            if (dsSP == null || dsSP.Count == 0) return;

            // Sắp xếp theo doanh thu giảm dần để vẽ top 5 dễ theo dõi
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

            parentPanel.Controls.Add(chart);
        }
    }
}
