using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Services;

namespace FloriSys._2_QuanLy
{
    public partial class ucNhanVien : UserControl
    {
        private bool isEditMode = false;
        private string placeholderSearch = "🔍 Tìm tên, SDT...";

        public ucNhanVien()
        {
            InitializeComponent();
        }

        private void ucNhanVien_Load(object sender, EventArgs e)
        {
            cboFilterChucVu.SelectedIndex = 0;
            cboFilterTrangThai.SelectedIndex = 0;
            LoadData();
            ResetForm();
        }

        private void LoadData()
        {
            string keyword = txtSearch.Text == placeholderSearch ? "" : txtSearch.Text.Trim();
            string chucVu = cboFilterChucVu.SelectedIndex > 0 ? cboFilterChucVu.SelectedItem.ToString() : "";
            string trangThai = "";
            if (cboFilterTrangThai.SelectedIndex == 1) trangThai = "DangLam";
            else if (cboFilterTrangThai.SelectedIndex == 2) trangThai = "DaNghi";

            dgvNhanVien.DataSource = NhanVienDAO.LayDanhSach(keyword, chucVu, trangThai);

            // Tùy chỉnh cột
            if (dgvNhanVien.Columns.Count > 0)
            {
                dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
                dgvNhanVien.Columns["ChucVu"].HeaderText = "Chức vụ";
                dgvNhanVien.Columns["SoDienThoai"].HeaderText = "SĐT";
                dgvNhanVien.Columns["TaiKhoan"].HeaderText = "Tài khoản";
                dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng thái";
            }
        }

        private void ResetForm()
        {
            isEditMode = false;
            lblFormTitle.Text = "Thêm nhân viên mới";
            txtMaNV.Text = GenerateMaNV();
            txtHoTen.Clear();
            cboChucVu.SelectedIndex = 1; // Default Cashier
            txtSDT.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
            txtTaiKhoan.ReadOnly = false;
            btnUpdateStatus.Visible = false;
            btnSave.Text = "💾 Lưu thông tin";
        }

        private string GenerateMaNV()
        {
            string newCode = "";
            try
            {
                SqlParameter outParam = new SqlParameter("@NewCode", SqlDbType.NVarChar, 20) { Direction = ParameterDirection.Output };
                DatabaseHelper.ExecuteNonQuery("sp_SinhMa", new SqlParameter[]
                {
                    new SqlParameter("@Prefix", "NV"),
                    new SqlParameter("@Table", "NHAN_VIEN"),
                    new SqlParameter("@Column", "MaNV"),
                    outParam
                });
                newCode = outParam.Value.ToString();
            }
            catch { newCode = "NV" + DateTime.Now.ToString("HHmmss"); }
            return newCode;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                isEditMode = true;
                lblFormTitle.Text = "Cập nhật nhân viên";
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                cboChucVu.SelectedItem = row.Cells["ChucVu"].Value.ToString();
                txtSDT.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value.ToString();
                txtTaiKhoan.ReadOnly = true;
                txtMatKhau.Clear(); // Mật khẩu không hiển thị lái

                btnUpdateStatus.Visible = true;
                string status = row.Cells["TrangThai"].Value.ToString();
                btnUpdateStatus.Text = status == "DangLam" ? "Khóa / Nghỉ" : "Mở khóa / Làm lại";
                btnSave.Text = "💾 Cập nhật";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            string hoTen = txtHoTen.Text.Trim();
            string chucVu = cboChucVu.SelectedItem.ToString();
            string sdt = txtSDT.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan))
            {
                MessageBox.Show("Họ tên và Tài khoản không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (isEditMode)
                {
                    NhanVienDAO.CapNhatNhanVien(maNV, hoTen, chucVu, sdt);
                    if (!string.IsNullOrEmpty(matKhau))
                    {
                        // Cập nhật mật khẩu nếu có nhập
                        string hash = SessionManager.HashSHA256(matKhau);
                        string sql = "UPDATE NHAN_VIEN SET MatKhau=@MK WHERE MaNV=@Ma";
                        DatabaseHelper.ExecuteRawNonQuery(sql, new SqlParameter[] {
                            new SqlParameter("@MK", hash),
                            new SqlParameter("@Ma", maNV)
                        });
                    }
                    MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (string.IsNullOrEmpty(matKhau)) matKhau = "123456"; // Mật khẩu mặc định
                    string hash = SessionManager.HashSHA256(matKhau);
                    NhanVienDAO.ThemNhanVien(maNV, hoTen, chucVu, sdt, taiKhoan, hash);
                    MessageBox.Show("Thêm nhân viên mới thành công!\nMật khẩu mặc định là: 123456 (nếu không nhập)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                LoadData();
                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            string currentStatus = btnUpdateStatus.Text.Contains("Khóa") ? "DangLam" : "DaNghi";
            string newStatus = currentStatus == "DangLam" ? "DaNghi" : "DangLam";

            string msg = currentStatus == "DangLam" ? "Bạn có chắc muốn cho nhân viên này nghỉ việc?" : "Bạn có chắc muốn cho nhân viên này đi làm lại?";
            if (MessageBox.Show(msg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                NhanVienDAO.CapNhatTrangThai(maNV, newStatus);
                LoadData();
                ResetForm();
            }
        }

        // Placeholder logic
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == placeholderSearch)
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = placeholderSearch;
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
