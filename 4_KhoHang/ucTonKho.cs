using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Services;

namespace FloriSys._4_KhoHang
{
    public partial class ucTonKho : UserControl
    {
        public ucTonKho() { InitializeComponent(); }
        private void ucTonKho_Load(object sender, EventArgs e) { LoadData(); }
        public void LoadData()
        {
            try
            {
                string key = txtTimKiem.Text.Trim();
                dgvTonKho.DataSource = SanPhamDAO.LayCanhBaoTonKho();
                if (dgvTonKho.Columns.Count > 0)
                {
                    dgvTonKho.Columns["MaSP"].HeaderText = "Mã SP";
                    dgvTonKho.Columns["TenSP"].HeaderText = "Tên sản phẩm";
                    dgvTonKho.Columns["LoaiHoa"].HeaderText = "Loại";
                    dgvTonKho.Columns["SoLuongTon"].HeaderText = "Tồn kho";
                    dgvTonKho.Columns["MucTonToiThieu"].HeaderText = "Tối thiểu";
                    dgvTonKho.Columns["TinhTrang"].HeaderText = "Trạng thái";
                    dgvTonKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
