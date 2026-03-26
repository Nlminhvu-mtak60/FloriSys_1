using FloriSys._1_DangNhap;
using FloriSys._2_QuanLy;
using FloriSys.Services;
using System;
using System.Windows.Forms;

namespace FloriSys
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmDangNhap loginForm = new frmDangNhap();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new frmMain());
            }
        }
    }
}
