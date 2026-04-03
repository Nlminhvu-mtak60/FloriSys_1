using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;

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
            dgvQuyen.DataSource = PhanQuyenDAO.LayPhanQuyen(selectedRole);
            
            if (dgvQuyen.Columns.Count > 0)
            {
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
            DataTable dt = dgvQuyen.DataSource as DataTable;
            if (dt == null) return;

            foreach (DataRow dr in dt.Rows)
            {
                string module = dr["Module"].ToString();
                bool xem = Convert.ToBoolean(dr["Xem"]);
                bool them = Convert.ToBoolean(dr["Them"]);
                bool sua = Convert.ToBoolean(dr["Sua"]);
                bool xoa = Convert.ToBoolean(dr["Xoa"]);
                bool export = Convert.ToBoolean(dr["Export"]);

                PhanQuyenDAO.CapNhatQuyen(selectedRole, module, xem, them, sua, xoa, export);
            }

            MessageBox.Show("Đã cập nhật phân quyền thành công cho vai trò " + selectedRole, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadQuyen();
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
