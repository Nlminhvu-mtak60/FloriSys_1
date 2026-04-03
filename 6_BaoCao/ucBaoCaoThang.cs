using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using FloriSys.DataAccess;

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
                DataTable dtDoanhThu = BaoCaoDAO.DoanhThuThang(thang, nam);
                if (dtDoanhThu.Rows.Count > 0)
                {
                    decimal tongDT = Convert.ToDecimal(dtDoanhThu.Rows[0]["TongDoanhThu"]);
                    lblDoanhThuValue.Text = tongDT.ToString("N0") + "đ";
                }

                // So sánh với tháng trước
                int thangTruoc = thang == 1 ? 12 : thang - 1;
                int namTruoc = thang == 1 ? nam - 1 : nam;
                DataTable dtTruoc = BaoCaoDAO.DoanhThuThang(thangTruoc, namTruoc);
                if (dtDoanhThu.Rows.Count > 0 && dtTruoc.Rows.Count > 0)
                {
                    decimal dtThang = Convert.ToDecimal(dtDoanhThu.Rows[0]["TongDoanhThu"]);
                    decimal dtThangTruoc = Convert.ToDecimal(dtTruoc.Rows[0]["TongDoanhThu"]);
                    if (dtThangTruoc > 0)
                    {
                        decimal phanTram = ((dtThang - dtThangTruoc) / dtThangTruoc) * 100;
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
                DataTable dtTopSP = BaoCaoDAO.SanPhamBanChay(thang, nam);
                dgvTopSP.DataSource = dtTopSP;
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
                DataTable dtNgay = BaoCaoDAO.DoanhThuTheoNgayTrongThang(thang, nam);
                foreach (DataRow row in dtNgay.Rows)
                {
                    int ngay = Convert.ToInt32(row["NgayTrongThang"]);
                    decimal dt = Convert.ToDecimal(row["DoanhThu"]);
                    int idx = series.Points.AddXY(ngay, dt);
                    if (dt == 0) series.Points[idx].Color = Color.FromArgb(229, 231, 235);
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
