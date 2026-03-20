using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Services;

namespace FloriSys._1_DangNhap
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            try
            {
                string matKhauHash = SessionManager.HashSHA256(password);
                DataTable dt = NhanVienDAO.DangNhap(username, matKhauHash);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    SessionManager.MaNV = row["MaNV"].ToString();
                    SessionManager.HoTen = row["HoTen"].ToString();
                    SessionManager.ChucVu = row["ChucVu"].ToString();
                    SessionManager.TaiKhoan = row["TaiKhoan"].ToString();
                    SessionManager.SoDienThoai = row["SoDienThoai"].ToString();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.\nHoặc tài khoản đã bị khóa.",
                        "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể kết nối cơ sở dữ liệu!\n\n" + ex.Message,
                    "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
