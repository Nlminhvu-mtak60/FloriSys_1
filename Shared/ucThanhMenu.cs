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
            var repo = new FloriSys.DataAccess.PhanQuyenRepository();
            var qList = repo.LayPhanQuyen(chucVu);
            
            bool HasReadAccess(string moduleCode)
            {
                var q = qList.Find(x => x.Module == moduleCode);
                return q != null && q.Xem;
            }

            bool donHang = HasReadAccess("DonHang");
            bool khoHang = HasReadAccess("KhoHang");
            bool giaoHang = HasReadAccess("GiaoHang");
            bool phanHoi = HasReadAccess("PhanHoi");
            bool traHang = HasReadAccess("TraHang");
            bool sanPham = HasReadAccess("SanPham");
            bool khachHang = HasReadAccess("KhachHang");
            bool nhanVien = HasReadAccess("NhanVien");
            bool baoCao = HasReadAccess("BaoCao");
            bool pq = HasReadAccess("PhanQuyen");

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
            btnPhanCong.Visible = giaoHang && !FloriSys.Services.SessionManager.IsShipper; 

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
        }

        // Empty event handlers kept for Designer compatibility

        private void btnDoiMatKhau_Click(object sender, EventArgs e) { Navigate("DoiMatKhau", btnDoiMatKhau); }
    }
}
