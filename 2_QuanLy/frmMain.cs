using System;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.Services;
using FloriSys.Shared;
using FloriSys._1_DangNhap;
using FloriSys._3_BanHang;
using FloriSys._4_KhoHang;
using FloriSys._5_GiaoHang;
using FloriSys._6_BaoCao;

namespace FloriSys._2_QuanLy
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Cập nhật thông tin người dùng trên menu
            ucThanhMenu1.CapNhatNguoiDung(SessionManager.HoTen, SessionManager.ChucVuDisplay, SessionManager.AvatarChar);
            ucThanhMenu1.PhanQuyen(SessionManager.ChucVu);

            // Đăng ký sự kiện điều hướng
            ucThanhMenu1.MenuClicked += OnMenuClicked;

            // Hiển thị Dashboard mặc định
            LoadUC(new ucDashboard());
        }

        public void OnMenuClicked(string menuName)
        {
            OnMenuClicked(menuName, null);
        }

        public void OnMenuClicked(string menuName, object data)
        {
            UserControl uc = null;
            switch (menuName)
            {
                case "Dashboard":
                    if (SessionManager.ChucVu == "Warehouse")
                        uc = new _4_KhoHang.ucDashboardKho();
                    else if (SessionManager.ChucVu == "Shipper")
                        uc = new _5_GiaoHang.ucDashboardShipper();
                    else if (SessionManager.ChucVu == "Cashier")
                        uc = new _3_BanHang.ucDashboardBanHang();
                    else
                        uc = new ucDashboard(); // Admin Dashboard
                    break;
                case "DanhSachDon":
                    var ucDS = new _3_BanHang.ucDanhSachDon();
                    ucDS.TaoDonMoi += () => OnMenuClicked("TaoDon");
                    ucDS.XemChiTiet += (maDon) => LoadChiTietDon(maDon);
                    uc = ucDS;
                    break;
                case "TaoDon":
                    var ucTD = new _3_BanHang.ucTaoDon();
                    ucTD.DonDaTao += () => OnMenuClicked("DanhSachDon");
                    uc = ucTD;
                    break;
                case "PhanHoi":
                    var ucPH = new _3_BanHang.ucPhanHoi();
                    if (data != null && data is string maDonParam)
                    {
                        ucPH.SetMaDon(maDonParam);
                    }
                    uc = ucPH;
                    break;
                case "TraHang":
                    uc = new _3_BanHang.ucTraHang();
                    break;
                case "TonKho":
                    uc = new _4_KhoHang.ucTonKho();
                    break;
                case "CauHinhTonKho":
                    uc = new _4_KhoHang.ucCauHinhTonKho();
                    break;
                case "NhapKho":
                    uc = new _4_KhoHang.ucNhapKho();
                    break;
                case "XuatKho":
                    uc = new _4_KhoHang.ucXuatKho();
                    break;
                case "HangHu":
                    uc = new _4_KhoHang.ucHangHu();
                    break;
                case "LichSuNhapKho":
                    uc = new _4_KhoHang.ucLichSuNhapKho();
                    break;
                case "DanhSachGiao":
                    uc = new _5_GiaoHang.ucGiaoHang();
                    break;
                case "PhanCong":
                    uc = new _5_GiaoHang.ucPhanCong();
                    break;
                case "NhanVien":
                    uc = new ucNhanVien();
                    break;
                case "PhanQuyen":
                    uc = new Shared.ucPhanQuyen();
                    break;
                case "SanPham":
                    uc = new _7_DanhMuc.ucSanPham();
                    break;
                case "KhachHang":
                    uc = new _7_DanhMuc.ucKhachHang();
                    break;
                case "BaoCao":
                    uc = new ucBaoCao();
                    break;
                case "DoiMatKhau":
                    uc = new ucDoiMatKhau();
                    break;
                case "Thoat":
                    this.Close();
                    return;
                case "DangXuat":
                    if (MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SessionManager.Clear();
                        this.Close();
                    }
                    return;
            }

            if (uc != null)
            {
                LoadUC(uc);
                // Highlight menu item
                ucThanhMenu1.SetActiveMenu(menuName);
            }
        }

        private void LoadChiTietDon(string maDon)
        {
            var uc = new _3_BanHang.ucChiTietDonHang();
            uc.SetMaDon(maDon);
            LoadUC(uc);
        }

        private void LoadUC(UserControl uc)
        {
            // Dispose tất cả controls cũ để tránh memory leak GDI handles
            while (panel1.Controls.Count > 0)
            {
                var old = panel1.Controls[0];
                panel1.Controls.RemoveAt(0);
                old.Dispose();
            }
            uc.Dock = DockStyle.Fill;
            panel1.Controls.Add(uc);
        }

        private void ucThanhMenu1_Load(object sender, EventArgs e)
        {

        }
    }
}
