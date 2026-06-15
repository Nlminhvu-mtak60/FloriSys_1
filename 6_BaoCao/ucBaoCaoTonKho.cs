using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Shared;
using FloriSys.Models;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoTonKho : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();
        public ucBaoCaoTonKho()
        {
            InitializeComponent();
        }

        private void ucBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                List<SanPham> dsSP = _bcRepo.BaoCaoTonKho();
                dgvTonKho.DataSource = dsSP;

                if (dgvTonKho.Columns.Count > 0)
                {
                    // Chỉ hiển thị các cột cần thiết
                    var visibleCols = new List<string> { "TenSP", "SoLuongTon", "MucTonToiThieu", "TinhTrang" };
                    foreach (DataGridViewColumn col in dgvTonKho.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

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

                // Update static KPI cards
                lblTotalValue.Text = totalSP.ToString();
                lblLowValue.Text = sapHet.ToString();
                lblEmptyValue.Text = hetHang.ToString();

                // Color code rows
                dgvTonKho.CellFormatting -= DgvTonKho_CellFormatting;
                dgvTonKho.CellFormatting += DgvTonKho_CellFormatting;
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải dữ liệu tồn kho: " + ex.Message);
            }
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

