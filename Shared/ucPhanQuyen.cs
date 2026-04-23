using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Shared
{
    public partial class ucPhanQuyen : UserControl
    {
        private string selectedRole = "Admin";

        public ucPhanQuyen()
        {
            InitializeComponent();
        }

        private void ucPhanQuyen_Load(object sender, EventArgs e)
        {
            CreateRoleButtons();
            LoadQuyen();
        }

        private void CreateRoleButtons()
        {
            string[] roles = { "Admin", "Cashier", "Warehouse", "Shipper" };
            foreach (string role in roles)
            {
                Button btn = new Button
                {
                    Text = role,
                    Width = 120,
                    Height = 35,
                    FlatStyle = FlatStyle.Flat,
                    Tag = role,
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
            
            // Update UI
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
            List<PhanQuyen> dsPQ = PhanQuyenDAO.LayPhanQuyen(selectedRole);
            dgvQuyen.DataSource = dsPQ;
            
            if (dgvQuyen.Columns.Count > 0)
            {
                dgvQuyen.Columns["Module"].HeaderText = "Phân hệ";
                dgvQuyen.Columns["Module"].ReadOnly = true;
                
                dgvQuyen.Columns["Xem"].HeaderText = "Xem";
                dgvQuyen.Columns["Them"].HeaderText = "Thêm";
                dgvQuyen.Columns["Sua"].HeaderText = "Sửa";
                dgvQuyen.Columns["Xoa"].HeaderText = "Xóa";
                dgvQuyen.Columns["Export"].HeaderText = "Xuất BC";

                // Hide ChucVu column
                if (dgvQuyen.Columns.Contains("ChucVu"))
                    dgvQuyen.Columns["ChucVu"].Visible = false;
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
                    PhanQuyenDAO.CapNhatQuyen(pq);
                }
            }

            MessageBox.Show("Đã cập nhật phân quyền thành công cho vai trò " + selectedRole, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadQuyen();
        }
    }
}
