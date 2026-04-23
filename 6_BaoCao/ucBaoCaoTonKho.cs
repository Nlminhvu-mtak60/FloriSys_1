using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoTonKho : UserControl
    {
        public ucBaoCaoTonKho()
        {
            InitializeComponent();
        }

        private void ucBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<SanPham> dsSP = BaoCaoDAO.BaoCaoTonKho();
                dgvTonKho.DataSource = dsSP;

                if (dgvTonKho.Columns.Count > 0)
                {
                    dgvTonKho.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    dgvTonKho.Columns["SoLuongTon"].HeaderText = "Tồn kho";
                    dgvTonKho.Columns["MucTonToiThieu"].HeaderText = "Mức tối thiểu";

                    if (dgvTonKho.Columns.Contains("TinhTrang"))
                        dgvTonKho.Columns["TinhTrang"].HeaderText = "Trạng thái";
                }

                // Count KPIs
                int totalSP = dsSP.Count;
                int sapHet = 0;
                int hetHang = 0;

                foreach (SanPham sp in dsSP)
                {
                    if (sp.SoLuongTon == 0) hetHang++;
                    else if (sp.SoLuongTon <= sp.MucTonToiThieu) sapHet++;
                }

                lblTongSPSapHet.Text = (sapHet + hetHang).ToString();

                // Add additional KPI cards dynamically
                AddKPICards(totalSP, sapHet, hetHang);

                // Color code rows
                dgvTonKho.CellFormatting -= DgvTonKho_CellFormatting;
                dgvTonKho.CellFormatting += DgvTonKho_CellFormatting;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tồn kho: " + ex.Message);
            }
        }

        private void AddKPICards(int totalSP, int sapHet, int hetHang)
        {
            // Remove existing extra cards
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.Controls[i];
                if (c.Tag != null && c.Tag.ToString() == "kpi_extra")
                    this.Controls.Remove(c);
            }

            int startX = 350;
            int y = 80;

            // Card: Tổng SP theo dõi
            Panel pnl1 = CreateKPICard("TỔNG SP THEO DÕI", totalSP.ToString(),
                Color.FromArgb(239, 246, 255), Color.FromArgb(30, 64, 175));
            pnl1.Location = new Point(startX, y);
            pnl1.Tag = "kpi_extra";
            this.Controls.Add(pnl1);
            pnl1.BringToFront();

            // Card: Sắp hết
            Panel pnl2 = CreateKPICard("SẮP HẾT HÀNG", sapHet.ToString(),
                Color.FromArgb(255, 251, 235), Color.FromArgb(146, 64, 14));
            pnl2.Location = new Point(startX + 210, y);
            pnl2.Tag = "kpi_extra";
            this.Controls.Add(pnl2);
            pnl2.BringToFront();

            // Card: Hết hàng
            Panel pnl3 = CreateKPICard("HẾT HÀNG", hetHang.ToString(),
                Color.FromArgb(254, 242, 242), Color.FromArgb(185, 28, 28));
            pnl3.Location = new Point(startX + 420, y);
            pnl3.Tag = "kpi_extra";
            this.Controls.Add(pnl3);
            pnl3.BringToFront();
        }

        private Panel CreateKPICard(string title, string value, Color bgColor, Color fgColor)
        {
            Panel pnl = new Panel();
            pnl.Size = new Size(190, 100);
            pnl.BackColor = bgColor;

            Label lblT = new Label();
            lblT.Text = title;
            lblT.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            lblT.ForeColor = Color.FromArgb(156, 163, 175);
            lblT.Location = new Point(15, 15);
            lblT.AutoSize = true;
            pnl.Controls.Add(lblT);

            Label lblV = new Label();
            lblV.Text = value;
            lblV.Font = new Font("Georgia", 22f, FontStyle.Bold);
            lblV.ForeColor = fgColor;
            lblV.Location = new Point(15, 40);
            lblV.AutoSize = true;
            pnl.Controls.Add(lblV);

            return pnl;
        }

        private void DgvTonKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTonKho.Columns.Contains("TinhTrang") && dgvTonKho.Columns[e.ColumnIndex].Name == "TinhTrang" && e.Value != null)
            {
                string val = e.Value.ToString();
                switch (val)
                {
                    case "DuHang":
                        e.Value = "✅ Đủ hàng";
                        e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                        break;
                    case "SapHet":
                        e.Value = "⚠️ Sắp hết";
                        e.CellStyle.ForeColor = Color.FromArgb(146, 64, 14);
                        break;
                    case "HetHang":
                        e.Value = "🔴 Hết hàng";
                        e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                        break;
                }
                e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            }
        }
    }
}
