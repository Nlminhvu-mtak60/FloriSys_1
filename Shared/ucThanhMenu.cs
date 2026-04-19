using System;
using System.Drawing;
using System.Windows.Forms;

namespace FloriSys.Shared
{
    public partial class ucThanhMenu : UserControl
    {
        public event Action<string> MenuClicked;
        private Button _activeButton;

        public ucThanhMenu()
        {
            InitializeComponent();
            SetupLayout();
        }

        private void SetupLayout()
        {
            // Tái cấu trúc layout để tránh lỗi overlap và lỗi scroll không hết cỡ
            Panel pnlHeader = new Panel();
            pnlHeader.Height = 82;
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.BackColor = Color.White;

            // Chuyển logo và phụ đề vào pnlHeader
            pnlHeader.Controls.Add(lblLogo);
            lblLogo.Location = new Point(10, 20);
            pnlHeader.Controls.Add(lblPhuDe);
            lblPhuDe.Location = new Point(10, 52);
            pnlHeader.Controls.Add(lblDuongKe);
            lblDuongKe.Location = new Point(0, 76);

            this.Controls.Add(pnlHeader);

            // Cấu hình thứ tự Dock (Z-Order)
            pnlNguoiDung.Dock = DockStyle.Bottom;
            pnlHeader.SendToBack(); // Tính Dock=Top ưu tiên
            pnlNguoiDung.SendToBack(); // Tính Dock=Bottom ưu tiên kế tiếp

            pnlMenu.Dock = DockStyle.Fill;
            pnlMenu.BringToFront(); // Chiếm khoảng trống còn lại ở giữa
            
            // Padding cuối để cuộn đủ thấy các nút cuối cùng
            pnlMenu.AutoScrollMargin = new Size(0, 30);
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

        public void PhanQuyen(string chucVu)
        {
            // Admin: hiển thị tất cả
            // Cashier: đơn hàng + danh mục + báo cáo (read-only)
            // Warehouse: kho hàng
            // Shipper: giao hàng

            // Hide all first, then show based on role
            bool isAdmin = chucVu == "Admin";

            // Nhóm đơn hàng - Admin + Cashier
            bool showDonHang = isAdmin || chucVu == "Cashier";
            lblNhomDonHang.Visible = showDonHang;
            btnDanhSachDon.Visible = showDonHang;
            btnTaoDon.Visible = showDonHang;
            btnPhanHoi.Visible = showDonHang;
            btnTraHang.Visible = isAdmin; // Chỉ Admin mới có quyền trả hàng (DH-06)

            // Nhóm kho hàng - Admin + Warehouse
            bool showKhoHang = isAdmin || chucVu == "Warehouse";
            lblNhomKhoHang.Visible = showKhoHang;
            btnTonKho.Visible = showKhoHang;
            btnNhapKho.Visible = showKhoHang;
            btnXuatKho.Visible = showKhoHang;
            btnHangHu.Visible = showKhoHang;
            btnLichSuNhapKho.Visible = showKhoHang;

            // Nhóm giao hàng - Admin + Shipper
            bool showGiaoHang = isAdmin || chucVu == "Shipper";
            lblNhomGiaoHang.Visible = showGiaoHang;
            btnDanhSachGiao.Visible = showGiaoHang;
            btnPhanCong.Visible = isAdmin; // Chỉ Admin phân công
            
            // Nhóm quản lý
            lblNhomQuanLy.Visible = isAdmin || chucVu == "Cashier";
            btnNhanVien.Visible = isAdmin;
            btnPhanQuyen.Visible = isAdmin;
            btnSanPham.Visible = isAdmin || chucVu == "Cashier";
            btnKhachHang.Visible = isAdmin || chucVu == "Cashier";

            // Báo cáo - Admin only
            lblNhomBaoCao.Visible = isAdmin;
            btnBaoCao.Visible = isAdmin;
        }

        // Empty event handlers kept for Designer compatibility
        private void pnlMenu_Paint(object sender, PaintEventArgs e) { }
        private void lblNhomTongQuan_Click(object sender, EventArgs e) { }
        private void lblNhomDonHang_Click(object sender, EventArgs e) { }
        private void lblNhomKhoHang_Click(object sender, EventArgs e) { }
        private void lblNhomGiaoHang_Click(object sender, EventArgs e) { }
        private void lblNhomQuanLy_Click(object sender, EventArgs e) { }
        private void lblNhomTaiKhoan_Click(object sender, EventArgs e) { }
        private void btnDoiMatKhau_Click(object sender, EventArgs e) { Navigate("DoiMatKhau", btnDoiMatKhau); }
    }
}
