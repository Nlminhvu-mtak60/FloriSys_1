using System;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.Services;
using FloriSys.Shared;
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

        private void OnMenuClicked(string menuName)
        {
            UserControl uc = null;
            switch (menuName)
            {
                case "Dashboard":
                    uc = new ucDashboard();
                    break;
                case "DanhSachDon":
                    var ucDS = new _3_BanHang.ucDanhSachDon();
                    ucDS.TaoDonMoi += () => OnMenuClicked("TaoDon");
                    uc = ucDS;
                    break;
                case "TaoDon":
                    var ucTD = new _3_BanHang.ucTaoDon();
                    ucTD.DonDaTao += () => OnMenuClicked("DanhSachDon");
                    uc = ucTD;
                    break;
                case "PhanHoi":
                    // placeholder
                    uc = CreatePlaceholder("Phản hồi khiếu nại");
                    break;
                case "TraHang":
                    uc = CreatePlaceholder("Xử lý trả hàng");
                    break;
                case "TonKho":
                    uc = new _4_KhoHang.ucTonKho();
                    break;
                case "NhapKho":
                    uc = new _4_KhoHang.ucNhapKho();
                    break;
                case "XuatKho":
                    uc = CreatePlaceholder("Xác nhận xuất kho");
                    break;
                case "HangHu":
                    uc = CreatePlaceholder("Ghi nhận hàng hư hỏng");
                    break;
                case "LichSuNhapKho":
                    uc = CreatePlaceholder("Lịch sử phiếu nhập kho");
                    break;
                case "DanhSachGiao":
                    uc = new _5_GiaoHang.ucGiaoHang();
                    break;
                case "PhanCong":
                    uc = new _5_GiaoHang.ucPhanCong();
                    break;
                case "CapNhatGH":
                    uc = new _5_GiaoHang.ucCapNhatGH();
                    break;
                case "NhanVien":
                    uc = CreatePlaceholder("Quản lý nhân viên");
                    break;
                case "SanPham":
                    uc = CreatePlaceholder("Danh mục sản phẩm");
                    break;
                case "KhachHang":
                    uc = CreatePlaceholder("Danh sách khách hàng");
                    break;
                case "BaoCao":
                    uc = new ucBaoCao();
                    break;
                case "DoiMatKhau":
                    uc = CreatePlaceholder("Đổi mật khẩu");
                    break;
                case "DangXuat":
                    if (MessageBox.Show("Bạn có muốn đăng xuất?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SessionManager.Clear();
                        this.Close();
                    }
                    return;
            }

            if (uc != null) LoadUC(uc);
        }

        private void LoadUC(UserControl uc)
        {
            panel1.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panel1.Controls.Add(uc);
        }

        private UserControl CreatePlaceholder(string title)
        {
            UserControl uc = new UserControl();
            uc.BackColor = Color.FromArgb(250, 245, 246);
            Label lbl = new Label();
            lbl.Text = title;
            lbl.Font = new Font("Georgia", 18F, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(17, 24, 39);
            lbl.AutoSize = true;
            lbl.Location = new Point(30, 30);
            Label lblSub = new Label();
            lblSub.Text = "Màn hình này sẽ được hoàn thiện trong bước tiếp theo.";
            lblSub.Font = new Font("Segoe UI", 10F);
            lblSub.ForeColor = Color.FromArgb(156, 163, 175);
            lblSub.AutoSize = true;
            lblSub.Location = new Point(30, 70);
            uc.Controls.Add(lbl);
            uc.Controls.Add(lblSub);
            return uc;
        }

        private void ucThanhMenu1_Load(object sender, EventArgs e) { }
        private void ucThanhMenu1_Load_1(object sender, EventArgs e) { }
    }
}
