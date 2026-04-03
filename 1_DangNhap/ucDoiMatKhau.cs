using System;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Services;

namespace FloriSys._1_DangNhap
{
    public partial class ucDoiMatKhau : UserControl
    {
        public ucDoiMatKhau()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string oldPass = txtOldPassword.Text.Trim();
            string newPass = txtNewPassword.Text.Trim();
            string confirmPass = txtConfirmPassword.Text.Trim();

            if (string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(confirmPass))
            {
                ShowMessage("Vui lòng nhập đầy đủ thông tin.", Color.Red);
                return;
            }

            if (newPass.Length < 6)
            {
                ShowMessage("Mật khẩu mới phải có ít nhất 6 ký tự.", Color.Red);
                return;
            }

            if (newPass != confirmPass)
            {
                ShowMessage("Xác nhận mật khẩu mới không khớp.", Color.Red);
                return;
            }

            try
            {
                string oldHash = SessionManager.HashSHA256(oldPass);
                string newHash = SessionManager.HashSHA256(newPass);

                bool success = NhanVienDAO.DoiMatKhau(SessionManager.MaNV, oldHash, newHash);

                if (success)
                {
                    ShowMessage("Đổi mật khẩu thành công!", Color.Green);
                    ClearForm();
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    ShowMessage("Mật khẩu hiện tại không đúng.", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Lỗi: " + ex.Message, Color.Red);
            }
        }

        private void ShowMessage(string msg, Color color)
        {
            lblMessage.Text = msg;
            lblMessage.ForeColor = color;
        }

        private void ClearForm()
        {
            txtOldPassword.Clear();
            txtNewPassword.Clear();
            txtConfirmPassword.Clear();
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
