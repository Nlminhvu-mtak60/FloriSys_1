using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;
using System.Windows.Forms.DataVisualization.Charting;

namespace FloriSys._2_QuanLy
{
    public partial class ucDashboard : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();

        public ucDashboard()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.DesignMode)
            {
                lblDate.Text = "Hôm nay, " + DateTime.Now.ToString("dd/MM/yyyy");
                LoadData();
            }
        }

        public override void LoadData()
        {
            LoadStats();
            LoadDonHangGanDay();
            LoadChart();
        }

        private void LoadStats()
        {
            try
            {
                ThongKeDashboard stats = _bcRepo.ThongKeDashboard();
                if (stats != null)
                {
                    lblStat1Value.Text = stats.DonHomNay.ToString();
                    lblStat1Sub.Text = TinhPhanTram(stats.DonHomNay, stats.DonHomQua, "so với hôm qua");
                    lblStat1Sub.ForeColor = stats.DonHomNay >= stats.DonHomQua
                        ? Color.FromArgb(22, 101, 52)
                        : Color.FromArgb(185, 28, 28);

                    if (stats.DoanhThuHomNay >= 1000000)
                        lblStat2Value.Text = (stats.DoanhThuHomNay / 1000000).ToString("N1") + "M";
                    else
                        lblStat2Value.Text = stats.DoanhThuHomNay.ToString("#,##0") + "đ";
                    lblStat2Sub.Text = TinhPhanTram(stats.DoanhThuHomNay, stats.DoanhThuHomQua, "so với hôm qua");
                    lblStat2Sub.ForeColor = stats.DoanhThuHomNay >= stats.DoanhThuHomQua
                        ? Color.FromArgb(22, 101, 52)
                        : Color.FromArgb(185, 28, 28);

                    lblStat3Value.Text = stats.DonDangGiao.ToString();
                    lblStat3Sub.Text = stats.ShipperDangGiao > 0
                        ? $"{stats.ShipperDangGiao} shipper đang giao"
                        : "Không có đơn đang giao";

                    lblStat4Value.Text = stats.SPSapHet.ToString();
                    lblStat4Sub.Text = stats.SPSapHet > 0 ? "↓ Cần nhập thêm" : "✓ Đủ hàng";
                    lblStat4Sub.ForeColor = stats.SPSapHet > 0
                        ? Color.FromArgb(232, 57, 77)
                        : Color.FromArgb(22, 101, 52);

                    if (stats.SPSapHet > 0)
                    {
                        pnlCanhBao.Visible = true;
                        
                        List<SanPhamSapHet> dsSapHet = _bcRepo.LaySanPhamSapHet();
                        List<string> tenSapHet = new List<string>();
                        foreach (SanPhamSapHet sp in dsSapHet)
                        {
                            tenSapHet.Add($"{sp.TenSP} (còn {sp.SoLuongTon})");
                        }
                        
                        string thongTinSapHet = string.Join(", ", tenSapHet);
                        lblCanhBao.Text = $"⚠️  {stats.SPSapHet} sản phẩm sắp hết hàng: {thongTinSapHet}";
                        btnCanhBao.Text = $"🔔  Cảnh báo ({stats.SPSapHet})";
                    }
                    else
                    {
                        pnlCanhBao.Visible = false;
                        btnCanhBao.Text = "🔔  Cảnh báo (0)";
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi LoadStats: " + ex.Message);
            }
        }

        private string TinhPhanTram(decimal homNay, decimal homQua, string suffix)
        {
            if (homQua == 0 && homNay == 0)
                return "— Chưa có dữ liệu";
            if (homQua == 0)
                return $"↑ Mới hôm nay";

            decimal phanTram = ((homNay - homQua) / homQua) * 100;
            if (phanTram > 0)
                return $"↑ {phanTram:N0}% {suffix}";
            else if (phanTram < 0)
                return $"↓ {Math.Abs(phanTram):N0}% {suffix}";
            else
                return $"— Bằng hôm qua";
        }

        private void LoadDonHangGanDay()
        {
            try
            {
                List<DonHangGanDay> dsDH = _bcRepo.DonHangGanDay(5);
                dgvDonHang.DataSource = dsDH;
                if (dgvDonHang.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "MaDon", "TenKH", "TongTien", "TrangThai" };
                    foreach (DataGridViewColumn col in dgvDonHang.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                    if (dgvDonHang.Columns.Contains("MaDon")) dgvDonHang.Columns["MaDon"].HeaderText = "Mã đơn";
                    if (dgvDonHang.Columns.Contains("TenKH")) dgvDonHang.Columns["TenKH"].HeaderText = "Khách hàng";
                    if (dgvDonHang.Columns.Contains("TongTien")) 
                    {
                        dgvDonHang.Columns["TongTien"].HeaderText = "Tổng tiền";
                        dgvDonHang.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                    }
                    if (dgvDonHang.Columns.Contains("TrangThai")) dgvDonHang.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                
                dgvDonHang.CellFormatting -= DgvDonHang_CellFormatting;
                dgvDonHang.CellFormatting += DgvDonHang_CellFormatting;
            }
            catch (Exception)
            {
                // Bỏ qua lỗi trong lúc design
            }
        }

        private void DgvDonHang_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvDonHang.Columns[e.ColumnIndex].Name == "TrangThai" && e.Value != null)
            {
                string val = e.Value.ToString();
                switch (val)
                {
                    case "Moi":
                        e.Value = "Mới";
                        e.CellStyle.ForeColor = Color.FromArgb(30, 64, 175);
                        break;
                    case "DangXuLy":
                        e.Value = "Đang xử lý";
                        e.CellStyle.ForeColor = Color.FromArgb(146, 64, 14);
                        break;
                    case "HoanThanh":
                        e.Value = "Hoàn thành";
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                        break;
                    case "Huy":
                        e.Value = "Đã hủy";
                        e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                        break;
                    case "DaGiao":
                        e.Value = "Đã giao";
                        e.CellStyle.ForeColor = Color.FromArgb(21, 128, 61);
                        break;
                    case "HoanHang":
                        e.Value = "Hoàn hàng";
                        e.CellStyle.ForeColor = Color.FromArgb(124, 58, 237);
                        break;
                }
                e.CellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            }
        }

        private void LoadChart()
        {
            try
            {
                pnlBieuDo.Controls.Clear();
                Chart chart = new Chart();
                chart.Location = new Point(0, 30);
                chart.Size = new Size(pnlBieuDo.Width - 10, pnlBieuDo.Height - 35);
                chart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                chart.BackColor = Color.White;
                
                ChartArea area = new ChartArea("MainArea");
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisX.LabelStyle.ForeColor = Color.DimGray;
                area.AxisX.LineColor = Color.LightGray;
                
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(240, 240, 240);
                area.AxisY.LabelStyle.Format = "{0:N0}";
                area.AxisY.LabelStyle.ForeColor = Color.DimGray;
                area.AxisY.LineColor = Color.Transparent;
                chart.ChartAreas.Add(area);
                
                Series series = new Series("Doanh Thu");
                series.ChartType = SeriesChartType.Column;
                series.Color = Color.FromArgb(232, 57, 77); // Primary Red
                series.BackGradientStyle = GradientStyle.TopBottom;
                series.BackSecondaryColor = Color.FromArgb(255, 182, 193); // Light Pink
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "{0:N0}";
                series.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
                series["PointWidth"] = "0.6";
                chart.Series.Add(series);
                
                List<DoanhThuNgay> dsDoanhThu = _bcRepo.DoanhThu7Ngay();
                foreach (DoanhThuNgay item in dsDoanhThu)
                {
                    series.Points.AddXY(item.Ngay.ToString("dd/MM"), item.DoanhThu);
                }

                // Tiêu đề biểu đồ
                Title title = new Title("HOẠT ĐỘNG 7 NGÀY GẦN NHẤT", Docking.Top, new Font("Segoe UI", 10f, FontStyle.Bold), Color.FromArgb(64, 64, 64));
                chart.Titles.Add(title);
                
                pnlBieuDo.Controls.Add(chart);
            }
            catch (Exception) { }
        }

    }
}