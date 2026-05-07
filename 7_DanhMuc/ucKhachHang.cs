using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._7_DanhMuc
{
    public partial class ucKhachHang : BaseUserControl
    {
        private readonly KhachHangRepository _khRepo = new KhachHangRepository();
        private string editingMaKH = null; // null = add mode, not null = edit mode

        public ucKhachHang()
        {
            InitializeComponent();
        }

        private void ucKhachHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                string key = txtSearch.Text.Trim();
                List<KhachHang> dsKH = _khRepo.LayDanhSach(key);
                dgvKhachHang.DataSource = dsKH;
                FormatGrid();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvKhachHang.Columns.Count == 0) return;

            var visibleCols = new List<string> { "MaKH", "HoTen", "SoDienThoai", "DiaChi", "Email", "NgayTao", "TongDon" };
            foreach (DataGridViewColumn col in dgvKhachHang.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

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
            using (Form frm = new Form())
            {
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
                    ShowWarning("Vui lòng nhập Họ tên và SĐT.");
                    return;
                }

                try
                {
                    KhachHang kh = new KhachHang
                    {
                        MaKH = editingMaKH,
                        HoTen = txtHoTen.Text.Trim(),
                        SoDienThoai = txtSDT.Text.Trim(),
                        DiaChi = txtDiaChi.Text.Trim(),
                        Email = txtEmail.Text.Trim()
                    };

                    if (editingMaKH == null)
                    {
                        _khRepo.ThemKhachHang(kh);
                        ShowSuccess("Thêm khách hàng thành công!");
                    }
                    else
                    {
                        _khRepo.CapNhatKhachHang(kh);
                        ShowSuccess("Cập nhật thành công!");
                    }
                    frm.DialogResult = DialogResult.OK;
                    frm.Close();
                }
                catch (Exception ex)
                {
                    ShowError("Lỗi: " + ex.Message);
                }
            };

            btnCancel.Click += (s, ev) => { frm.Close(); };

            frm.Controls.Add(btnSave);
            frm.Controls.Add(btnCancel);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
            } // end using
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
            KhachHang kh = dgvKhachHang.CurrentRow.DataBoundItem as KhachHang;
            if (kh == null) return;

            editingMaKH = kh.MaKH;
            ShowEditDialog("Sửa khách hàng – " + editingMaKH, kh.HoTen, kh.SoDienThoai, kh.DiaChi ?? "", kh.Email ?? "");
        }

        private void DeleteSelected()
        {
            if (dgvKhachHang.CurrentRow == null) return;
            KhachHang kh = dgvKhachHang.CurrentRow.DataBoundItem as KhachHang;
            if (kh == null) return;

            if (Confirm(string.Format("Bạn có chắc muốn xóa khách hàng \"{0}\" ({1})?", kh.HoTen, kh.MaKH)))
            {
                try
                {
                    _khRepo.XoaKhachHang(kh.MaKH);
                    ShowSuccess("Đã xóa thành công.");
                    LoadData();
                }
                catch (Exception ex)
                {
                    ShowWarning(ex.Message);
                }
            }
        }
    }
}
