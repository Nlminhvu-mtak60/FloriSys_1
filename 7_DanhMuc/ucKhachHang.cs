using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;

namespace FloriSys._7_DanhMuc
{
    public partial class ucKhachHang : UserControl
    {
        private string editingMaKH = null; // null = add mode, not null = edit mode

        public ucKhachHang()
        {
            InitializeComponent();
        }

        private void ucKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                string key = txtSearch.Text.Trim();
                DataTable dt = KhachHangDAO.LayDanhSach(key);
                dgvKhachHang.DataSource = dt;
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            if (dgvKhachHang.Columns.Count == 0) return;

            dgvKhachHang.Columns["MaKH"].HeaderText = "Mã KH";
            dgvKhachHang.Columns["HoTen"].HeaderText = "Họ tên";
            dgvKhachHang.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
            dgvKhachHang.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvKhachHang.Columns["Email"].HeaderText = "Email";
            dgvKhachHang.Columns["NgayTao"].HeaderText = "Ngày tạo";
            dgvKhachHang.Columns["TongDon"].HeaderText = "Tổng đơn";

            dgvKhachHang.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dgvKhachHang.Columns["TongDon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            editingMaKH = null;
            ShowEditDialog("Thêm khách hàng mới", "", "", "", "");
        }

        private void ShowEditDialog(string title, string hoTen, string sdt, string diaChi, string email)
        {
            // Create inline edit form
            Form frm = new Form();
            frm.Text = title;
            frm.Size = new Size(460, 360);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.BackColor = Color.White;
            frm.Font = new Font("Segoe UI", 10F);

            int y = 20;
            int lblW = 110;
            int txtW = 300;

            // Họ tên
            frm.Controls.Add(new Label { Text = "Họ tên *", Location = new Point(20, y), Size = new Size(lblW, 25), ForeColor = Color.FromArgb(75, 85, 99) });
            TextBox txtHoTen = new TextBox { Text = hoTen, Location = new Point(135, y), Size = new Size(txtW, 28) };
            frm.Controls.Add(txtHoTen);
            y += 42;

            // SĐT
            frm.Controls.Add(new Label { Text = "SĐT *", Location = new Point(20, y), Size = new Size(lblW, 25), ForeColor = Color.FromArgb(75, 85, 99) });
            TextBox txtSDT = new TextBox { Text = sdt, Location = new Point(135, y), Size = new Size(txtW, 28) };
            frm.Controls.Add(txtSDT);
            y += 42;

            // Địa chỉ
            frm.Controls.Add(new Label { Text = "Địa chỉ", Location = new Point(20, y), Size = new Size(lblW, 25), ForeColor = Color.FromArgb(75, 85, 99) });
            TextBox txtDiaChi = new TextBox { Text = diaChi, Location = new Point(135, y), Size = new Size(txtW, 28) };
            frm.Controls.Add(txtDiaChi);
            y += 42;

            // Email
            frm.Controls.Add(new Label { Text = "Email", Location = new Point(20, y), Size = new Size(lblW, 25), ForeColor = Color.FromArgb(75, 85, 99) });
            TextBox txtEmail = new TextBox { Text = email, Location = new Point(135, y), Size = new Size(txtW, 28) };
            frm.Controls.Add(txtEmail);
            y += 52;

            // Buttons
            Button btnSave = new Button
            {
                Text = editingMaKH == null ? "✅  Thêm mới" : "💾  Cập nhật",
                Size = new Size(150, 38),
                Location = new Point(135, y),
                BackColor = Color.FromArgb(232, 57, 77),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;

            Button btnCancel = new Button
            {
                Text = "Hủy",
                Size = new Size(100, 38),
                Location = new Point(295, y),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(107, 114, 128)
            };
            btnCancel.FlatAppearance.BorderSize = 1;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);

            btnSave.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập Họ tên và SĐT.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (editingMaKH == null)
                    {
                        KhachHangDAO.ThemKhachHang(txtHoTen.Text.Trim(), txtSDT.Text.Trim(),
                            txtDiaChi.Text.Trim(), txtEmail.Text.Trim());
                        MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        KhachHangDAO.CapNhatKhachHang(editingMaKH, txtHoTen.Text.Trim(), txtSDT.Text.Trim(),
                            txtDiaChi.Text.Trim(), txtEmail.Text.Trim());
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    frm.DialogResult = DialogResult.OK;
                    frm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (s, ev) => { frm.Close(); };

            frm.Controls.Add(btnSave);
            frm.Controls.Add(btnCancel);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Add context menu for Edit/Delete
            ContextMenuStrip ctx = new ContextMenuStrip();
            ctx.Font = new Font("Segoe UI", 9.5F);

            ToolStripMenuItem mnuEdit = new ToolStripMenuItem("✏️  Sửa thông tin");
            mnuEdit.Click += (s, ev) => EditSelected();
            ctx.Items.Add(mnuEdit);

            ToolStripMenuItem mnuDelete = new ToolStripMenuItem("🗑️  Xóa khách hàng");
            mnuDelete.ForeColor = Color.FromArgb(220, 38, 38);
            mnuDelete.Click += (s, ev) => DeleteSelected();
            ctx.Items.Add(mnuDelete);

            dgvKhachHang.ContextMenuStrip = ctx;
            dgvKhachHang.CellDoubleClick += (s, ev) => { if (ev.RowIndex >= 0) EditSelected(); };
        }

        private void EditSelected()
        {
            if (dgvKhachHang.CurrentRow == null) return;
            DataGridViewRow row = dgvKhachHang.CurrentRow;

            editingMaKH = row.Cells["MaKH"].Value.ToString();
            string hoTen = row.Cells["HoTen"].Value?.ToString() ?? "";
            string sdt = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
            string diaChi = row.Cells["DiaChi"].Value?.ToString() ?? "";
            string email = row.Cells["Email"].Value?.ToString() ?? "";

            ShowEditDialog("Sửa khách hàng – " + editingMaKH, hoTen, sdt, diaChi, email);
        }

        private void DeleteSelected()
        {
            if (dgvKhachHang.CurrentRow == null) return;
            string maKH = dgvKhachHang.CurrentRow.Cells["MaKH"].Value.ToString();
            string tenKH = dgvKhachHang.CurrentRow.Cells["HoTen"].Value.ToString();

            if (MessageBox.Show(
                string.Format("Bạn có chắc muốn xóa khách hàng \"{0}\" ({1})?", tenKH, maKH),
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    KhachHangDAO.XoaKhachHang(maKH);
                    MessageBox.Show("Đã xóa thành công.", "Thông báo");
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Không thể xóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
