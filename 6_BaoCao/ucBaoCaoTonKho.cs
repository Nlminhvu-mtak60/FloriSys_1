using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._6_BaoCao
{
    public partial class ucBaoCaoTonKho : UserControl
    {
        public ucBaoCaoTonKho()
        {
            InitializeComponent();
        }

        private void ucBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                DataTable dt = BaoCaoDAO.BaoCaoTonKho();
                dgvTonKho.DataSource = dt;

                if (dgvTonKho.Columns.Count > 0)
                {
                    dgvTonKho.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    dgvTonKho.Columns["SoLuongTon"].HeaderText = "Tồn kho";
                    dgvTonKho.Columns["MucTonToiThieu"].HeaderText = "Mức tối thiểu";
                    
                    // Add Status column logic if needed or just display raw data
                }

                // Update KPIs
                lblTongSPSapHet.Text = dt.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu tồn kho: " + ex.Message);
            }
        }
    }
}
