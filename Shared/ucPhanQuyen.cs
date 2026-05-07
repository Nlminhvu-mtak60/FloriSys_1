using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

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
            LoadQuyen();
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
            string[] roles = { "Admin", "Cashier", "Warehouse", "Shipper" };
            foreach (string role in roles)
            {
                Button btn = new Button
                {
                    Text = role, Width = 120, Height = 35,
                    FlatStyle = FlatStyle.Flat, Tag = role,
                    BackColor = (role == selectedRole) ? Color.FromArgb(232, 57, 77) : Color.White,
                    ForeColor = (role == selectedRole) ? Color.White : Color.Black
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
            lblTableTitle.Text = "Ma trận quyền hạn cho vai trò: " + selectedRole;
            List<PhanQuyen> dsPQ = _pqRepo.LayPhanQuyen(selectedRole);
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
            foreach (DataGridViewRow row in dgvQuyen.Rows)
            {
                PhanQuyen pq = row.DataBoundItem as PhanQuyen;
                if (pq != null)
                {
                    pq.ChucVu = selectedRole;
                    _pqRepo.CapNhatQuyen(pq);
                }
            }
            ShowSuccess("Đã cập nhật phân quyền thành công cho vai trò " + selectedRole);
            LoadQuyen();
        }
    }
}
