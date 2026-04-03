using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using System.Windows.Forms.DataVisualization.Charting;

namespace FloriSys._2_QuanLy
{
    public partial class ucDashboard : UserControl
    {
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
                LoadStats();
                LoadDonHangGanDay();
                LoadChart();
            }
        }

        private void LoadStats()
        {
            try
            {
                DataTable dt = BaoCaoDAO.ThongKeDashboard();
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    lblStat1Value.Text = dr["DonHomNay"].ToString();
                    
                    decimal doanhThu = Convert.ToDecimal(dr["DoanhThuHomNay"]);
                    if (doanhThu >= 1000000)
                        lblStat2Value.Text = (doanhThu / 1000000).ToString("N1") + "M";
                    else
                        lblStat2Value.Text = doanhThu.ToString("N0") + "đ";

                    lblStat3Value.Text = dr["DonDangGiao"].ToString();
                    lblStat4Value.Text = dr["SPSapHet"].ToString();
                    
                    int spSapHet = Convert.ToInt32(dr["SPSapHet"]);
                    if (spSapHet > 0)
                    {
                        pnlCanhBao.Visible = true;
                        lblCanhBao.Text = $"⚠️  Có {spSapHet} sản phẩm sắp hết hàng cần nhập ngay!";
                        btnCanhBao.Text = $"🔔  Cảnh báo ({spSapHet})";
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
                // Bỏ qua lỗi trong lúc design
            }
        }

        private void LoadDonHangGanDay()
        {
            try
            {
                dgvDonHang.DataSource = BaoCaoDAO.DonHangGanDay(5);
                if (dgvDonHang.Columns.Count > 0)
                {
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
            catch (Exception ex)
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
                
                DataTable dt = BaoCaoDAO.DoanhThu7Ngay();
                decimal total = 0;
                foreach (DataRow row in dt.Rows)
                {
                    DateTime date = Convert.ToDateTime(row["Ngay"]);
                    decimal val = Convert.ToDecimal(row["DoanhThu"]);
                    total += val;
                    series.Points.AddXY(date.ToString("dd/MM"), val);
                }

                // Tiêu đề biểu đồ
                Title title = new Title("HOẠT ĐỘNG 7 NGÀY GẦN NHẤT", Docking.Top, new Font("Segoe UI", 10f, FontStyle.Bold), Color.FromArgb(64, 64, 64));
                chart.Titles.Add(title);
                
                pnlBieuDo.Controls.Add(chart);
            }
            catch (Exception) { }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
