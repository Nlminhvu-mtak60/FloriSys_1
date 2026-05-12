using System;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.Models;
using FloriSys.Services;

namespace FloriSys._1_DangNhap
{
    public partial class frmDangNhap : Form
    {
        private readonly AuthService _authService;

        public frmDangNhap()
        {
            InitializeComponent();
            _authService = new AuthService();
            picBackground.SendToBack();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // ENCAPSULATION: Validation is in AuthService, not here
            NhanVien nv;
            string error;
            if (_authService.DangNhap(username, password, out nv, out error))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(error, "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}
