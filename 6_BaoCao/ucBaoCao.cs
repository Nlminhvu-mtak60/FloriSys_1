using System;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.Shared;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCao : BaseUserControl
    {
        public ucBaoCao()
        {
            InitializeComponent();
        }

        public override void LoadData() { }

        private void ucBaoCao_Load(object sender, EventArgs e)
        {
            // Load default report (Daily)
            ShowReport(new ucBaoCaoNgay(), btnBCNgay);
        }

        private void btnBC_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            UserControl uc = null;

            if (btn == btnBCNgay) uc = new ucBaoCaoNgay();
            else if (btn == btnBCThang) uc = new ucBaoCaoThang();
            else if (btn == btnBCSanPham) uc = new ucBaoCaoSanPham();
            else if (btn == btnBCTonKho) uc = new ucBaoCaoTonKho();
            else if (btn == btnBCNhanVien) uc = new ucBaoCaoNhanVien();

            if (uc != null)
            {
                ShowReport(uc, btn);
            }
        }

        private void ShowReport(UserControl uc, Button activeBtn)
        {
            pnlContainer.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            pnlContainer.Controls.Add(uc);

            // Update UI for active button
            foreach (Control ctrl in pnlSubNav.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.ForeColor = Color.FromArgb(75, 85, 99);
                    btn.BackColor = Color.White;
                }
            }

            activeBtn.ForeColor = Color.FromArgb(232, 57, 77);
            activeBtn.BackColor = Color.FromArgb(254, 242, 244);
        }
    }
}
