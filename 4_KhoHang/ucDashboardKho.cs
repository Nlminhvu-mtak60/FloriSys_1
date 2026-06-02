using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;
using FloriSys._2_QuanLy;


namespace FloriSys._4_KhoHang
{
    public partial class ucDashboardKho : BaseUserControl
    {
        private readonly BaoCaoRepository _bcRepo = new BaoCaoRepository();
        private readonly SanPhamRepository _spRepo = new SanPhamRepository();
        private Button btnXuLyNgay;
        
        private Panel pnlAlert;
        private Label lblAlert;

        public ucDashboardKho()
        {
            InitializeComponent();
            
            pnlAlert = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(255, 243, 205),
                Visible = false
            };
            lblAlert = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(133, 100, 4)
            };
            pnlAlert.Controls.Add(lblAlert);
            this.Controls.Add(pnlAlert);
        }

        private void ucDashboardKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void LoadData()
        {
            LoadStats();
            LoadDonChoXuat();
            LoadCanhBao();
        }

        private void LoadStats()
        {
            ThongKeKho stats = _bcRepo.ThongKeKho();
            if (stats != null)
            {
                lblValChoXuat.Text = stats.DonChoXuat.ToString();
                lblValSapHet.Text = stats.SPSapHet.ToString();
                lblValDaXuat.Text = stats.DaXuatHomNay.ToString();
                lblValPhieuNhap.Text = stats.PhieuNhapThang.ToString();

                int donChoXuat = stats.DonChoXuat;
                if (donChoXuat > 0)
                {
                    // Đổi màu cảnh báo
                    pnlDonChoXuat.BackColor = Color.FromArgb(254, 226, 226);
                    lblTitleChoXuat.ForeColor = Color.FromArgb(185, 28, 28);
                    
                    // Tạo button nếu chưa có
                    if (btnXuLyNgay == null)
                    {
                        btnXuLyNgay = new Button
                        {
                            Text = "⚡ Xử lý ngay",
                            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                            BackColor = Color.FromArgb(185, 28, 28),
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat,
                            Size = new Size(130, 30),
                            Cursor = Cursors.Hand
                        };
                        btnXuLyNgay.FlatAppearance.BorderSize = 0;
                        
                        // Đặt vị trí sang bên phải để không đè lên số lượng
                        btnXuLyNgay.Location = new Point(pnlDonChoXuat.Width - 140, pnlDonChoXuat.Height - 40);
                        
                        btnXuLyNgay.Click += (s, ev) => 
                        {
                            var mainForm = this.FindForm() as frmMain;
                            if (mainForm != null)
                                mainForm.OnMenuClicked("XuatKho");
                        };
                        pnlDonChoXuat.Controls.Add(btnXuLyNgay);
                    }
                    btnXuLyNgay.Visible = true;
                    btnXuLyNgay.Text = $"⚡ Xử lý {donChoXuat} đơn";
                }
                else
                {
                    // Đổi về màu bình thường
                    pnlDonChoXuat.BackColor = Color.FromArgb(240, 253, 244);
                    lblTitleChoXuat.ForeColor = Color.FromArgb(22, 101, 52);
                    
                    if (btnXuLyNgay != null)
                        btnXuLyNgay.Visible = false;
                }
            }
        }

        private void LoadDonChoXuat()
        {
            List<DonHangGanDay> dsDH = _bcRepo.DonHangChoXuat();
            dgvDonChoXuat.DataSource = dsDH;
            if (dgvDonChoXuat.Columns.Count > 0)
            {
                var visibleCols = new List<string> { "MaDon", "TenKH", "NgayTao", "TrangThai" };
                foreach (DataGridViewColumn col in dgvDonChoXuat.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                dgvDonChoXuat.Columns["MaDon"].HeaderText = "Mã đơn";
                dgvDonChoXuat.Columns["TenKH"].HeaderText = "Khách hàng";
                dgvDonChoXuat.Columns["NgayTao"].HeaderText = "Ngày đặt";
                dgvDonChoXuat.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvDonChoXuat.Columns["TrangThai"].HeaderText = "Trạng thái";
            }

            // Kiểm tra và hiển thị pop-up cảnh báo nếu có đơn hàng chờ quá 30 phút
            int donTre = 0;
            foreach (var dh in dsDH)
            {
                if ((DateTime.Now - dh.NgayTao).TotalMinutes >= 30)
                {
                    donTre++;
                }
            }

            if (donTre > 0)
            {
                lblAlert.Text = $"🚨 CẢNH BÁO TỒN ĐỌNG! Hiện đang có {donTre} đơn hàng mới chờ xuất kho quá 30 phút! Vui lòng ưu tiên xử lý.";
                pnlAlert.Visible = true;
                dgvDonChoXuat.BringToFront();
            }
            else
            {
                pnlAlert.Visible = false;
            }
        }

        private void LoadCanhBao()
        {
            flpCanhBao.Controls.Clear();
            List<SanPham> dsSP = _spRepo.LayCanhBaoTonKho();

            foreach (SanPham sp in dsSP)
            {
                Panel pnl = CreateItemCanhBao(sp.TenSP, sp.SoLuongTon, sp.MucTonToiThieu);
                flpCanhBao.Controls.Add(pnl);
            }
        }

        private Panel CreateItemCanhBao(string tenSP, int ton, int nguong)
        {
            Panel pnl = new Panel { Size = new Size(340, 60), Margin = new Padding(0, 5, 0, 5) };
            pnl.BackColor = (ton == 0) ? Color.MistyRose : Color.FromArgb(255, 251, 235);
            
            Label lblName = new Label { 
                Text = tenSP, 
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            
            Label lblDetail = new Label { 
                Text = string.Format("Còn {0} / tối thiểu {1}", ton, nguong), 
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.DarkRed,
                Location = new Point(10, 30),
                AutoSize = true
            };

            pnl.Controls.Add(lblName);
            pnl.Controls.Add(lblDetail);

            Panel bar = new Panel { 
                Size = new Size(100, 10), 
                Location = new Point(220, 25),
                BackColor = Color.Gainsboro
            };
            Panel fill = new Panel {
                Size = new Size(0, 10),
                BackColor = (ton == 0) ? Color.Red : Color.Orange
            };
            
            float percent = (float)ton / (nguong == 0 ? 1 : nguong);
            if (percent > 1) percent = 1;
            fill.Width = (int)(100 * percent);
            
            bar.Controls.Add(fill);
            pnl.Controls.Add(bar);

            return pnl;
        }
    }
}
