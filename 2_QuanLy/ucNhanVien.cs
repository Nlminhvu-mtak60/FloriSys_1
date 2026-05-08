using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Services;
using FloriSys.Shared;

namespace FloriSys._2_QuanLy
{
    public partial class ucNhanVien : BaseUserControl
    {
        private readonly NhanVienRepository _nvRepo = new NhanVienRepository();
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

        public override void LoadData()
        {
            string keyword = txtSearch.Text == placeholderSearch ? "" : txtSearch.Text.Trim();
            string selectedCV = cboFilterChucVu.SelectedIndex > 0 ? cboFilterChucVu.SelectedItem.ToString() : "";
            string chucVu = "";
            if (selectedCV == "Quản lý") chucVu = "Admin";
            else if (selectedCV == "Thu ngân") chucVu = "Cashier";
            else if (selectedCV == "Thủ kho") chucVu = "Warehouse";
            else if (selectedCV == "Giao hàng") chucVu = "Shipper";

            string trangThai = "";
            if (cboFilterTrangThai.SelectedIndex == 1) trangThai = "DangLam";
            else if (cboFilterTrangThai.SelectedIndex == 2) trangThai = "DaNghi";

            List<NhanVien> dsNV = _nvRepo.LayDanhSach(keyword, chucVu, trangThai);
            dgvNhanVien.DataSource = dsNV;

            // Tùy chỉnh cột
            if (dgvNhanVien.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvNhanVien.Columns)
                {
                    col.Visible = false;
                }

                if (dgvNhanVien.Columns.Contains("MaNV"))
                {
                    dgvNhanVien.Columns["MaNV"].Visible = true;
                    dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                }
                if (dgvNhanVien.Columns.Contains("HoTen"))
                {
                    dgvNhanVien.Columns["HoTen"].Visible = true;
                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ tên";
                }
                if (dgvNhanVien.Columns.Contains("ChucVuDisplay"))
                {
                    dgvNhanVien.Columns["ChucVuDisplay"].Visible = true;
                    dgvNhanVien.Columns["ChucVuDisplay"].HeaderText = "Chức vụ";
                }
                if (dgvNhanVien.Columns.Contains("SoDienThoai"))
                {
                    dgvNhanVien.Columns["SoDienThoai"].Visible = true;
                    dgvNhanVien.Columns["SoDienThoai"].HeaderText = "SĐT";
                }
                if (dgvNhanVien.Columns.Contains("TaiKhoan"))
                {
                    dgvNhanVien.Columns["TaiKhoan"].Visible = true;
                    dgvNhanVien.Columns["TaiKhoan"].HeaderText = "Tài khoản";
                }
                if (dgvNhanVien.Columns.Contains("TrangThaiDisplay"))
                {
                    dgvNhanVien.Columns["TrangThaiDisplay"].Visible = true;
                    dgvNhanVien.Columns["TrangThaiDisplay"].HeaderText = "Trạng thái";
                }
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
                NhanVien nv = dgvNhanVien.Rows[e.RowIndex].DataBoundItem as NhanVien;
                if (nv == null) return;

                txtMaNV.Text = nv.MaNV;
                txtHoTen.Text = nv.HoTen;
                
                string cvVN = "";
                if (nv.ChucVu == "Admin") cvVN = "Quản lý";
                else if (nv.ChucVu == "Cashier") cvVN = "Thu ngân";
                else if (nv.ChucVu == "Warehouse") cvVN = "Thủ kho";
                else if (nv.ChucVu == "Shipper") cvVN = "Giao hàng";
                cboChucVu.SelectedItem = cvVN;
                txtSDT.Text = nv.SoDienThoai;
                txtTaiKhoan.Text = nv.TaiKhoan;
                txtTaiKhoan.ReadOnly = true;
                txtMatKhau.Clear(); // Mật khẩu không hiển thị lại

                btnUpdateStatus.Visible = true;
                btnUpdateStatus.Text = nv.TrangThai == "DangLam" ? "Khóa / Nghỉ" : "Mở khóa / Làm lại";
                btnSave.Text = "💾 Cập nhật";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string taiKhoan = txtTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(hoTen) || string.IsNullOrEmpty(taiKhoan))
            {
                ShowWarning("Họ tên và Tài khoản không được để trống.");
                return;
            }

            try
            {
                string selectedCV = cboChucVu.SelectedItem.ToString();
                string chucVu = "Cashier";
                if (selectedCV == "Quản lý") chucVu = "Admin";
                else if (selectedCV == "Thu ngân") chucVu = "Cashier";
                else if (selectedCV == "Thủ kho") chucVu = "Warehouse";
                else if (selectedCV == "Giao hàng") chucVu = "Shipper";

                NhanVien nv = new NhanVien
                {
                    MaNV = txtMaNV.Text,
                    HoTen = hoTen,
                    ChucVu = chucVu,
                    SoDienThoai = txtSDT.Text.Trim(),
                    TaiKhoan = taiKhoan
                };

                if (isEditMode)
                {
                    _nvRepo.CapNhatNhanVien(nv);
                    if (!string.IsNullOrEmpty(matKhau))
                    {
                        // Cập nhật mật khẩu nếu có nhập
                        string hash = SessionManager.HashSHA256(matKhau);
                        _nvRepo.ResetMatKhau(nv.MaNV, hash);
                    }
                    ShowSuccess("Cập nhật thành công!");
                }
                else
                {
                    if (string.IsNullOrEmpty(matKhau)) matKhau = "123456"; // Mật khẩu mặc định
                    nv.MatKhau = SessionManager.HashSHA256(matKhau);
                    _nvRepo.ThemNhanVien(nv);
                    ShowSuccess("Thêm nhân viên mới thành công!\nMật khẩu mặc định là: 123456 (nếu không nhập)");
                }
                LoadData();
                ResetForm();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi: " + ex.Message);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNV.Text;
            string currentStatus = btnUpdateStatus.Text.Contains("Khóa") ? "DangLam" : "DaNghi";
            string newStatus = currentStatus == "DangLam" ? "DaNghi" : "DangLam";

            string msg = currentStatus == "DangLam" ? "Bạn có chắc muốn cho nhân viên này nghỉ việc?" : "Bạn có chắc muốn cho nhân viên này đi làm lại?";
            if (Confirm(msg))
            {
                _nvRepo.CapNhatTrangThai(maNV, newStatus);
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
    }
}
