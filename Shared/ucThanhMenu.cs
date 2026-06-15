using System;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.Services;

namespace FloriSys.Shared
{
    public partial class ucThanhMenu : UserControl
    {
        public event Action<string> MenuClicked;
        private Button _activeButton;
        
        private Timer badgeTimer;

        public ucThanhMenu()
        {
            InitializeComponent();
            SetupLayout();
        }

        private void SetupLayout()
        {


            // Cấu hình thứ tự Dock (Z-Order)
            pnlNguoiDung.Dock = DockStyle.Bottom;
            pnlHeader.SendToBack(); // Tính Dock=Top ưu tiên
            pnlNguoiDung.SendToBack(); // Tính Dock=Bottom ưu tiên kế tiếp

            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.BringToFront(); // Chiếm khoảng trống còn lại ở giữa
            
        }

        private void ucThanhMenu_Load(object sender, EventArgs e)
        {
            // Wire up all button clicks
            btnDashboard.Click += (s, ev) => Navigate("Dashboard", btnDashboard);
            btnDanhSachDon.Click += (s, ev) => Navigate("DanhSachDon", btnDanhSachDon);
            btnTaoDon.Click += (s, ev) => Navigate("TaoDon", btnTaoDon);
            btnPhanHoi.Click += (s, ev) => Navigate("PhanHoi", btnPhanHoi);
            btnTraHang.Click += (s, ev) => Navigate("TraHang", btnTraHang);
            btnTonKho.Click += (s, ev) => Navigate("TonKho", btnTonKho);
            btnNhapKho.Click += (s, ev) => Navigate("NhapKho", btnNhapKho);
            btnXuatKho.Click += (s, ev) => Navigate("XuatKho", btnXuatKho);
            btnHangHu.Click += (s, ev) => Navigate("HangHu", btnHangHu);
            btnLichSuNhapKho.Click += (s, ev) => Navigate("LichSuNhapKho", btnLichSuNhapKho);
            btnDanhSachGiao.Click += (s, ev) => Navigate("DanhSachGiao", btnDanhSachGiao);
            btnPhanCong.Click += (s, ev) => Navigate("PhanCong", btnPhanCong);
            btnNhanVien.Click += (s, ev) => Navigate("NhanVien", btnNhanVien);
            btnPhanQuyen.Click += (s, ev) => Navigate("PhanQuyen", btnPhanQuyen);
            btnSanPham.Click += (s, ev) => Navigate("SanPham", btnSanPham);
            btnKhachHang.Click += (s, ev) => Navigate("KhachHang", btnKhachHang);
            btnBaoCao.Click += (s, ev) => Navigate("BaoCao", btnBaoCao);
            btnDoiMatKhau.Click += (s, ev) => Navigate("DoiMatKhau", btnDoiMatKhau);
            btnDangXuat.Click += (s, ev) => MenuClicked?.Invoke("DangXuat");

            // Set Dashboard active by default
            SetActive(btnDashboard);

            // Cài đặt Badge Xuất Kho
            lblBadgeXuatKho.BringToFront();
            
            if (!this.DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                badgeTimer = new Timer { Interval = 3000 };
                badgeTimer.Tick += (s, ev) => RefreshBadge();
                badgeTimer.Start();
                RefreshBadge();
            }
        }

        private void Navigate(string menuName, Button btn)
        {
            SetActive(btn);
            MenuClicked?.Invoke(menuName);
        }

        private void SetActive(Button btn)
        {
            // Reset previous
            if (_activeButton != null)
            {
                _activeButton.BackColor = Color.White;
                _activeButton.ForeColor = Color.FromArgb(55, 65, 81);
            }
            // Set new active
            _activeButton = btn;
            btn.BackColor = Color.FromArgb(254, 242, 244);
            btn.ForeColor = Color.FromArgb(232, 57, 77);
        }

        public void CapNhatNguoiDung(string hoTen, string chucVu, string avatarChar)
        {
            lblTenNguoiDung.Text = hoTen;
            lblChucVu.Text = chucVu;
            lblAvatar.Text = avatarChar;
        }

        public void RefreshMenu()
        {
            PhanQuyen();
        }

        public void PhanQuyen()
        {
            // ABSTRACTION: Hide complex permission logic, just ask SessionManager
            
            bool donHang = SessionManager.Instance.HasPermission("DonHang", "Xem");
            bool khoHang = SessionManager.Instance.HasPermission("KhoHang", "Xem");
            bool giaoHang = SessionManager.Instance.HasPermission("GiaoHang", "Xem");
            bool phanHoi = SessionManager.Instance.HasPermission("PhanHoi", "Xem");
            bool traHang = SessionManager.Instance.HasPermission("TraHang", "Xem");
            bool sanPham = SessionManager.Instance.HasPermission("SanPham", "Xem");
            bool khachHang = SessionManager.Instance.HasPermission("KhachHang", "Xem");
            bool nhanVien = SessionManager.Instance.HasPermission("NhanVien", "Xem");
            bool baoCao = SessionManager.Instance.HasPermission("BaoCao", "Xem");
            bool pq = SessionManager.Instance.HasPermission("PhanQuyen", "Xem");

            // Bật/tắt các module con
            btnDanhSachDon.Visible = donHang;
            btnTaoDon.Visible = donHang;
            btnPhanHoi.Visible = phanHoi;
            btnTraHang.Visible = traHang;

            btnTonKho.Visible = khoHang;
            btnNhapKho.Visible = khoHang;
            btnXuatKho.Visible = khoHang;
            btnHangHu.Visible = khoHang;
            btnLichSuNhapKho.Visible = khoHang;

            btnDanhSachGiao.Visible = giaoHang;
            // Shipper không được phép truy cập menu Phân công giao hàng
            btnPhanCong.Visible = giaoHang && !SessionManager.IsShipper; 

            btnNhanVien.Visible = nhanVien;
            btnPhanQuyen.Visible = pq;
            btnSanPham.Visible = sanPham;
            btnKhachHang.Visible = khachHang;
            btnBaoCao.Visible = baoCao;

            // Cập nhật hiển thị Label nhóm
            lblNhomDonHang.Visible = btnDanhSachDon.Visible || btnTaoDon.Visible || btnPhanHoi.Visible || btnTraHang.Visible;
            lblNhomKhoHang.Visible = btnTonKho.Visible || btnNhapKho.Visible || btnXuatKho.Visible || btnHangHu.Visible || btnLichSuNhapKho.Visible;
            lblNhomGiaoHang.Visible = btnDanhSachGiao.Visible || btnPhanCong.Visible;
            lblNhomQuanLy.Visible = btnNhanVien.Visible || btnPhanQuyen.Visible || btnSanPham.Visible || btnKhachHang.Visible;
            lblNhomBaoCao.Visible = btnBaoCao.Visible;
            ReArrangeMenu(); // Chuẩn hóa menu phân quyền
        }

        private void ReArrangeMenu()
        {
            // Lấy tất cả controls trong pnlMenu, sắp xếp theo vị trí Y hiện tại (thứ tự ban đầu)
            var allControls = new System.Collections.Generic.List<Control>();
            foreach (Control c in pnlMenu.Controls)
                allControls.Add(c);

            allControls.Sort((a, b) => a.Location.Y.CompareTo(b.Location.Y));

            int currentY = 5;        // Y bắt đầu (khoảng cách từ trên xuống)
            int btnSpacing = 2;      // Khoảng cách giữa các button
            int groupSpacing = 8;    // Khoảng cách trước label nhóm

            foreach (Control c in allControls)
            {
                if (!c.Visible) continue;

                // Bỏ qua badge vì nó phải đi theo btnXuatKho
                if (c == lblBadgeXuatKho) continue;

                // Nếu là Label nhóm (lblNhom...) thêm khoảng cách trên
                if (c is Label)
                    currentY += groupSpacing;

                c.Location = new Point(c.Location.X, currentY);

                // Nếu control này là btnXuatKho, cập nhật vị trí của badge theo nó
                if (c == btnXuatKho)
                {
                    lblBadgeXuatKho.Location = new Point(btnXuatKho.Right - 35, currentY + 8);
                }

                currentY += c.Height + btnSpacing;
            }
        }


        public void SetActiveMenu(string menuName)
        {
            // Reset colors of all buttons
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel pnl)
                {
                    foreach (Control c in pnl.Controls)
                    {
                        if (c is Button b)
                        {
                            b.BackColor = Color.White;
                            b.ForeColor = Color.FromArgb(75, 85, 99);
                        }
                    }
                }
            }

            // Find and highlight active button
            string btnName = "btn" + menuName;
            Control[] found = this.Controls.Find(btnName, true);
            if (found.Length > 0 && found[0] is Button activeBtn)
            {
                activeBtn.BackColor = Color.FromArgb(254, 242, 244);
                activeBtn.ForeColor = Color.FromArgb(232, 57, 77);
                _activeButton = activeBtn;
            }
        }

        // Empty event handlers kept for Designer compatibility

        private void btnDoiMatKhau_Click(object sender, EventArgs e) { Navigate("DoiMatKhau", btnDoiMatKhau); }

        public void RefreshBadge()
        {
            var chucVu = SessionManager.CurrentUser?.ChucVu;
            if (chucVu != "Warehouse" && chucVu != "Admin")
            {
                lblBadgeXuatKho.Visible = false;
                return;
            }

            try
            {
                string sql = "SELECT COUNT(1) FROM DON_HANG WHERE TrangThai = 'Moi'";
                int count = Convert.ToInt32(DataAccess.DatabaseHelper.ExecuteRawScalar(sql, null));
                
                if (count > 0)
                {
                    lblBadgeXuatKho.Text = count > 99 ? "99+" : count.ToString();
                    lblBadgeXuatKho.Visible = true;
                }
                else
                {
                    lblBadgeXuatKho.Visible = false;
                }
            }
            catch { }
        }
    }
}
