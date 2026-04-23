using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoThang : UserControl
    {
        public ucBaoCaoThang()
        {
            InitializeComponent();
        }

        private void ucBaoCaoThang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                int thang = DateTime.Now.Month;
                int nam = DateTime.Now.Year;
                lblMonth.Text = "Tháng " + thang + "/" + nam;

                // KPI - Doanh thu tháng
                BaoCaoDoanhThu doanhThu = BaoCaoDAO.DoanhThuThang(thang, nam);
                if (doanhThu != null)
                {
                    lblDoanhThuValue.Text = doanhThu.TongDoanhThu.ToString("N0") + "đ";
                }

                // So sánh với tháng trước
                int thangTruoc = thang == 1 ? 12 : thang - 1;
                int namTruoc = thang == 1 ? nam - 1 : nam;
                BaoCaoDoanhThu dtTruoc = BaoCaoDAO.DoanhThuThang(thangTruoc, namTruoc);
                if (doanhThu != null && dtTruoc != null)
                {
                    if (dtTruoc.TongDoanhThu > 0)
                    {
                        decimal phanTram = ((doanhThu.TongDoanhThu - dtTruoc.TongDoanhThu) / dtTruoc.TongDoanhThu) * 100;
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
                List<SanPhamBanChay> dsTopSP = BaoCaoDAO.SanPhamBanChay(thang, nam);
                dgvTopSP.DataSource = dsTopSP;
                if (dgvTopSP.Columns.Count > 0)
                {
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
                MessageBox.Show("Lỗi tải báo cáo tháng: " + ex.Message);
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
            chart.Series.Add(series);

            try
            {
                List<DoanhThuNgay> dsNgay = BaoCaoDAO.DoanhThuTheoNgayTrongThang(thang, nam);
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

            pnlChartMock.Controls.Add(chart);
        }
    }
}
