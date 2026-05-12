using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._4_KhoHang
{
    public partial class ucTonKho : BaseUserControl
    {
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();

        public ucTonKho() { InitializeComponent(); }
        private void ucTonKho_Load(object sender, EventArgs e) 
        { 
            LoadData(); 
            AddConfigButton();
        }

        private void AddConfigButton()
        {
            Button btnConfig = new Button();
            btnConfig.Text = "⚙ Cấu hình định mức";
            btnConfig.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnConfig.BackColor = System.Drawing.Color.FromArgb(232, 57, 77);
            btnConfig.ForeColor = System.Drawing.Color.White;
            btnConfig.FlatStyle = FlatStyle.Flat;
            btnConfig.FlatAppearance.BorderSize = 0;
            btnConfig.Size = new System.Drawing.Size(180, 36);
            btnConfig.Location = new System.Drawing.Point(txtTimKiem.Right - 180, 3);
            btnConfig.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnConfig.Cursor = Cursors.Hand;
            btnConfig.Click += (s, ev) => 
            {
                var parentForm = this.FindForm() as FloriSys._2_QuanLy.frmMain;
                if (parentForm != null)
                {
                    // Reflection or direct call if we can't access OnMenuClicked
                    // The easiest way is to find the Panel1 and add it
                    var pnl = parentForm.Controls.Find("panel1", true)[0] as Panel;
                    if (pnl != null)
                    {
                        while (pnl.Controls.Count > 0) { var old = pnl.Controls[0]; pnl.Controls.RemoveAt(0); old.Dispose(); }
                        var uc = new _4_KhoHang.ucCauHinhTonKho();
                        uc.Dock = DockStyle.Fill;
                        pnl.Controls.Add(uc);
                    }
                }
            };
            this.Controls.Add(btnConfig);
            btnConfig.BringToFront();
        }

        public override void LoadData()
        {
            try
            {
                string key = txtTimKiem.Text.Trim();
                if (key == "🔍 Tìm tên sản phẩm...") key = "";
                List<SanPham> dsSP = _spRepo.LayDanhSach(key, "", "DangBan");
                dgvTonKho.DataSource = dsSP;
                if (dgvTonKho.Columns.Count > 0)
                {
                    var visibleCols = new List<string> { "MaSP", "TenSP", "LoaiHoa", "SoLuongTon", "MucTonToiThieu", "GiaBan", "GiaNhap" };
                    foreach (DataGridViewColumn col in dgvTonKho.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                    if (dgvTonKho.Columns.Contains("MaSP")) dgvTonKho.Columns["MaSP"].HeaderText = "Mã SP";
                    if (dgvTonKho.Columns.Contains("TenSP")) dgvTonKho.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    if (dgvTonKho.Columns.Contains("LoaiHoa")) dgvTonKho.Columns["LoaiHoa"].HeaderText = "Loại";
                    if (dgvTonKho.Columns.Contains("SoLuongTon")) dgvTonKho.Columns["SoLuongTon"].HeaderText = "Tồn kho";
                    if (dgvTonKho.Columns.Contains("MucTonToiThieu")) dgvTonKho.Columns["MucTonToiThieu"].HeaderText = "Tối thiểu";
                    if (dgvTonKho.Columns.Contains("GiaBan")) dgvTonKho.Columns["GiaBan"].HeaderText = "Giá bán";
                    if (dgvTonKho.Columns.Contains("GiaNhap")) dgvTonKho.Columns["GiaNhap"].HeaderText = "Giá nhập";
                    if (dgvTonKho.Columns.Contains("TrangThai")) dgvTonKho.Columns["TrangThai"].Visible = false;
                    dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex) { ShowError(ex.Message); }
        }

        private void txtTimKiem_Enter(object sender, EventArgs e)
        {
            if (txtTimKiem.Text == "🔍 Tìm tên sản phẩm...")
            {
                txtTimKiem.Text = "";
                txtTimKiem.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void txtTimKiem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                txtTimKiem.Text = "🔍 Tìm tên sản phẩm...";
                txtTimKiem.ForeColor = System.Drawing.Color.Gray;
            }
        }
    }
}
