using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;
using FloriSys.Services;

namespace FloriSys.Shared
{
    public partial class ucPhanQuyen : BaseUserControl
    {
        private readonly PhanQuyenRepository _pqRepo = new PhanQuyenRepository();
        private string selectedRole = "Admin";

        public ucPhanQuyen()
        {
            InitializeComponent();
        }

        public override void LoadData() { LoadQuyen(); }

        private void ucPhanQuyen_Load(object sender, EventArgs e)
        {
            dgvQuyen.CurrentCellDirtyStateChanged += dgvQuyen_CurrentCellDirtyStateChanged;
            CreateRoleButtons();
            
            if (!this.DesignMode && System.ComponentModel.LicenseManager.UsageMode != System.ComponentModel.LicenseUsageMode.Designtime)
            {
                LoadQuyen();
            }
        }

        private void dgvQuyen_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvQuyen.IsCurrentCellDirty)
            {
                dgvQuyen.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void CreateRoleButtons()
{
    var roles = new System.Collections.Generic.Dictionary<string, string> 
    { 
        { "Admin", "Quản Lý" }, 
        { "Cashier", "Thu Ngân" }, 
        { "Warehouse", "Kho" }, 
        { "Shipper", "Shipper" } 
    };
    foreach (var role in roles)
    {
        System.Windows.Forms.Button btn = new System.Windows.Forms.Button
        {
            Text = role.Value, Width = 120, Height = 35,
            FlatStyle = System.Windows.Forms.FlatStyle.Flat, Tag = role.Key,
            BackColor = (role.Key == selectedRole) ? System.Drawing.Color.FromArgb(232, 57, 77) : System.Drawing.Color.White,
            ForeColor = (role.Key == selectedRole) ? System.Drawing.Color.White : System.Drawing.Color.Black
        };
        btn.Click += RoleButton_Click;
        pnlRoles.Controls.Add(btn);
    }
}

        private void RoleButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            selectedRole = btn.Tag.ToString();
            foreach (Control ctrl in pnlRoles.Controls)
            {
                if (ctrl is Button b)
                {
                    b.BackColor = (b.Tag.ToString() == selectedRole) ? Color.FromArgb(232, 57, 77) : Color.White;
                    b.ForeColor = (b.Tag.ToString() == selectedRole) ? Color.White : Color.Black;
                }
            }
            LoadQuyen();
        }

        private void LoadQuyen()
        {
           
            List<PhanQuyen> dsPQ = _pqRepo.LayPhanQuyen(selectedRole);
            
            // Ẩn dòng PhanQuyen trên UI theo yêu cầu
            dsPQ.RemoveAll(x => x.Module.Equals("PhanQuyen", StringComparison.OrdinalIgnoreCase));
            
            // Đảm bảo luôn hiển thị đầy đủ 10 phân hệ chuẩn, kể cả khi trong DB chưa có dòng nào
            string[] allModules = { "Dashboard", "DonHang", "KhoHang", "GiaoHang", "NhanVien", "KhachHang", "SanPham", "BaoCao", "TraHang", "PhanHoi" };
            foreach (string module in allModules)
            {
                if (!dsPQ.Exists(x => x.Module.Equals(module, StringComparison.OrdinalIgnoreCase)))
                {
                    dsPQ.Add(new PhanQuyen { ChucVu = selectedRole, Module = module, Xem = false, Them = false, Sua = false, Xoa = false, Export = false });
                }
            }
            
            // Sắp xếp lại theo thứ tự chuẩn
            dsPQ.Sort((a, b) => Array.IndexOf(allModules, a.Module).CompareTo(Array.IndexOf(allModules, b.Module)));
            
            dgvQuyen.DataSource = null; // Reset binding
            dgvQuyen.DataSource = dsPQ;
            if (dgvQuyen.Columns.Count > 0)
            {
                var visibleCols = new List<string> { "Module", "Xem", "Them", "Sua", "Xoa", "Export" };
                foreach (DataGridViewColumn col in dgvQuyen.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                dgvQuyen.Columns["Module"].HeaderText = "Phân hệ";
                dgvQuyen.Columns["Module"].ReadOnly = true;
                dgvQuyen.Columns["Xem"].HeaderText = "Xem";
                dgvQuyen.Columns["Them"].HeaderText = "Thêm";
                dgvQuyen.Columns["Sua"].HeaderText = "Sửa";
                dgvQuyen.Columns["Xoa"].HeaderText = "Xóa";
                dgvQuyen.Columns["Export"].HeaderText = "Xuất BC";
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            dgvQuyen.EndEdit();
            List<PhanQuyen> updatedList = new List<PhanQuyen>();
            foreach (DataGridViewRow row in dgvQuyen.Rows)
            {
                PhanQuyen pq = row.DataBoundItem as PhanQuyen;
                if (pq != null)
                {
                    pq.ChucVu = selectedRole;
                    _pqRepo.CapNhatQuyen(pq);
                    updatedList.Add(pq);
                }
            }

            // Nếu vừa cập nhật quyền cho chính vai trò hiện tại của mình -> Cập nhật Session
            if (selectedRole == SessionManager.ChucVu)
            {
                // Lấy lại danh sách quyền từ DB để đảm bảo không mất các quyền bị ẩn trên UI (như PhanQuyen)
                var freshPermissions = _pqRepo.LayPhanQuyen(selectedRole);
                SessionManager.Instance.UpdatePermissions(freshPermissions);
                
                // Tìm frmMain để yêu cầu refresh menu
                Form parent = this.FindForm();
                if (parent is _2_QuanLy.frmMain main)
                {
                    main.RefreshPermissions();
                }
            }

            ShowSuccess("Đã cập nhật phân quyền thành công cho vai trò " + selectedRole);
            LoadQuyen();
        }
    }
}
