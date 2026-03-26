using System;
using System.Data;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._3_BanHang
{
    public partial class ucDashboardBanHang : UserControl
    {
        private string currentUserMaNV = FloriSys.Services.SessionManager.MaNV;

        public ucDashboardBanHang()
        {
            InitializeComponent();
        }

        private void ucDashboardBanHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            LoadStats();
            LoadDonHang();
            LoadLookup("");
        }

        private void LoadStats()
        {
            DataTable dt = BaoCaoDAO.ThongKeBanHang(currentUserMaNV);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                lblValDonToi.Text = dr["DonHomNay"].ToString();
                
                decimal doanhThu = Convert.ToDecimal(dr["DoanhThuHomNay"]);
                if (doanhThu >= 1000000)
                    lblValDoanhThu.Text = (doanhThu / 1000000).ToString("N1") + "M";
                else
                    lblValDoanhThu.Text = doanhThu.ToString("N0") + "đ";

                lblValDangXuLy.Text = dr["DonDangXuLy"].ToString();
                lblValHoanThanh.Text = dr["DonHoanThanh"].ToString();
            }
        }

        private void LoadDonHang()
        {
            dgvDonGanDay.DataSource = BaoCaoDAO.DonHangCuaNV(currentUserMaNV);
            if (dgvDonGanDay.Columns.Count > 0)
            {
                dgvDonGanDay.Columns["MaDon"].HeaderText = "Mã đơn";
                dgvDonGanDay.Columns["TenKH"].HeaderText = "Khách hàng";
                dgvDonGanDay.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvDonGanDay.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                dgvDonGanDay.Columns["NgayTao"].HeaderText = "Ngày tạo";
                dgvDonGanDay.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                dgvDonGanDay.Columns["TrangThai"].HeaderText = "Trạng thái";
            }
        }

        private void LoadLookup(string keyword)
        {
            dgvLookup.DataSource = SanPhamDAO.LaySanPhamDangBan(keyword);
            if (dgvLookup.Columns.Count > 0)
            {
                dgvLookup.Columns["MaSP"].Visible = false;
                dgvLookup.Columns["LoaiHoa"].Visible = false;
                dgvLookup.Columns["TenSP"].HeaderText = "Sản phẩm";
                dgvLookup.Columns["GiaBan"].HeaderText = "Giá bán";
                dgvLookup.Columns["GiaBan"].DefaultCellStyle.Format = "N0";
                dgvLookup.Columns["SoLuongTon"].HeaderText = "Tồn";
            }
        }

        private void btnTimLookup_Click(object sender, EventArgs e)
        {
            LoadLookup(txtLookup.Text);
        }
    }
}
