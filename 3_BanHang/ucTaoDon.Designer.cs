namespace FloriSys._3_BanHang
{
    partial class ucTaoDon
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }
        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pnlKhachHang = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.cboHinhThuc = new System.Windows.Forms.ComboBox();
            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.pnlSanPham = new System.Windows.Forms.GroupBox();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.pnlTimSP = new System.Windows.Forms.Panel();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.btnTimSP = new System.Windows.Forms.Button();
            this.txtTimSP = new System.Windows.Forms.TextBox();
            this.pnlGioHang = new System.Windows.Forms.GroupBox();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.btnXoaSP = new System.Windows.Forms.Button();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlKhachHang.SuspendLayout();
            this.pnlSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.pnlTimSP.SuspendLayout();
            this.pnlGioHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // lblTitle
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(17, 24, 39);
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Size = new System.Drawing.Size(700, 36);
            this.lblTitle.Text = "Tạo đơn hàng mới";
            // splitMain
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(20, 52);
            this.splitMain.SplitterDistance = 440;
            // Panel1 - Left: KH + SP
            // pnlKhachHang
            this.pnlKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlKhachHang.Text = "👤 Thông tin khách hàng";
            this.pnlKhachHang.Size = new System.Drawing.Size(430, 200);
            this.pnlKhachHang.Padding = new System.Windows.Forms.Padding(10);
            // Labels & Inputs
            this.lblTenKH.Text = "Họ tên:"; this.lblTenKH.Location = new System.Drawing.Point(14, 30); this.lblTenKH.Size = new System.Drawing.Size(80, 22); this.lblTenKH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenKH.Location = new System.Drawing.Point(100, 28); this.txtTenKH.Size = new System.Drawing.Size(310, 26); this.txtTenKH.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSDT.Text = "SĐT:"; this.lblSDT.Location = new System.Drawing.Point(14, 60); this.lblSDT.Size = new System.Drawing.Size(80, 22); this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSDT.Location = new System.Drawing.Point(100, 58); this.txtSDT.Size = new System.Drawing.Size(310, 26); this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHinhThuc.Text = "Hình thức:"; this.lblHinhThuc.Location = new System.Drawing.Point(14, 90); this.lblHinhThuc.Size = new System.Drawing.Size(80, 22); this.lblHinhThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboHinhThuc.Location = new System.Drawing.Point(100, 88); this.cboHinhThuc.Size = new System.Drawing.Size(310, 26); this.cboHinhThuc.Font = new System.Drawing.Font("Segoe UI", 10F); this.cboHinhThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lblDiaChi.Text = "Địa chỉ:"; this.lblDiaChi.Location = new System.Drawing.Point(14, 120); this.lblDiaChi.Size = new System.Drawing.Size(80, 22); this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiaChi.Location = new System.Drawing.Point(100, 118); this.txtDiaChi.Size = new System.Drawing.Size(310, 26); this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChu.Text = "Ghi chú:"; this.lblGhiChu.Location = new System.Drawing.Point(14, 150); this.lblGhiChu.Size = new System.Drawing.Size(80, 22); this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGhiChu.Location = new System.Drawing.Point(100, 148); this.txtGhiChu.Size = new System.Drawing.Size(310, 40); this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F); this.txtGhiChu.Multiline = true;
            this.pnlKhachHang.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblTenKH, this.txtTenKH, this.lblSDT, this.txtSDT, this.lblHinhThuc, this.cboHinhThuc, this.lblDiaChi, this.txtDiaChi, this.lblGhiChu, this.txtGhiChu });
            // pnlSanPham
            this.pnlSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSanPham.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlSanPham.Text = "🌸 Chọn sản phẩm";
            this.pnlSanPham.Padding = new System.Windows.Forms.Padding(10);
            // pnlTimSP
            this.pnlTimSP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimSP.Size = new System.Drawing.Size(410, 36);
            //this.txtTimSP.Location = new System.Drawing.Point(0, 4); this.txtTimSP.Size = new System.Drawing.Size(200, 26); this.txtTimSP.Font = new System.Drawing.Font("Segoe UI", 10F); this.txtTimSP.PlaceholderText = "🔍 Tìm tên hoa...";
            this.btnTimSP.Location = new System.Drawing.Point(206, 4); this.btnTimSP.Size = new System.Drawing.Size(50, 26); this.btnTimSP.Text = "Tìm"; this.btnTimSP.Font = new System.Drawing.Font("Segoe UI", 9F); this.btnTimSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnTimSP.Click += new System.EventHandler(this.btnTimSP_Click);
            this.btnThemSP.Location = new System.Drawing.Point(262, 4); this.btnThemSP.Size = new System.Drawing.Size(80, 26); this.btnThemSP.Text = "+ Thêm"; this.btnThemSP.Font = new System.Drawing.Font("Segoe UI", 9F); this.btnThemSP.BackColor = System.Drawing.Color.FromArgb(232, 57, 77); this.btnThemSP.ForeColor = System.Drawing.Color.White; this.btnThemSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnThemSP.FlatAppearance.BorderSize = 0; this.btnThemSP.Click += new System.EventHandler(this.btnThemSP_Click);
            this.pnlTimSP.Controls.AddRange(new System.Windows.Forms.Control[] { this.txtTimSP, this.btnTimSP, this.btnThemSP });
            // dgvSanPham
            this.dgvSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSanPham.AllowUserToAddRows = false; this.dgvSanPham.ReadOnly = true; this.dgvSanPham.RowHeadersVisible = false;
            this.dgvSanPham.BackgroundColor = System.Drawing.Color.White; this.dgvSanPham.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.pnlSanPham.Controls.Add(this.dgvSanPham);
            this.pnlSanPham.Controls.Add(this.pnlTimSP);
            this.splitMain.Panel1.Controls.Add(this.pnlSanPham);
            this.splitMain.Panel1.Controls.Add(this.pnlKhachHang);
            // Panel2 - Right: Giỏ hàng
            this.pnlGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGioHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlGioHang.Text = "🛒 Giỏ hàng";
            this.pnlGioHang.Padding = new System.Windows.Forms.Padding(10);
            this.dgvGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGioHang.AllowUserToAddRows = false; this.dgvGioHang.RowHeadersVisible = false;
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.White; this.dgvGioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGioHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.btnXoaSP.Dock = System.Windows.Forms.DockStyle.Bottom; this.btnXoaSP.Text = "✕ Xóa SP chọn"; this.btnXoaSP.Size = new System.Drawing.Size(300, 30); this.btnXoaSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnXoaSP.Font = new System.Drawing.Font("Segoe UI", 9F); this.btnXoaSP.Click += new System.EventHandler(this.btnXoaSP_Click);
            this.lblTongTien.Dock = System.Windows.Forms.DockStyle.Bottom; this.lblTongTien.Font = new System.Drawing.Font("Georgia", 14F, System.Drawing.FontStyle.Bold); this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(232, 57, 77); this.lblTongTien.Size = new System.Drawing.Size(300, 36); this.lblTongTien.Text = "Tổng cộng: 0đ"; this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnXacNhan.Dock = System.Windows.Forms.DockStyle.Bottom; this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(232, 57, 77); this.btnXacNhan.ForeColor = System.Drawing.Color.White; this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnXacNhan.FlatAppearance.BorderSize = 0; this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.btnXacNhan.Size = new System.Drawing.Size(300, 40); this.btnXacNhan.Text = "✅ Xác nhận tạo đơn"; this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand; this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            this.btnHuy.Dock = System.Windows.Forms.DockStyle.Bottom; this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F); this.btnHuy.Size = new System.Drawing.Size(300, 34); this.btnHuy.Text = "Hủy"; this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            this.pnlGioHang.Controls.Add(this.dgvGioHang);
            this.pnlGioHang.Controls.Add(this.btnXoaSP);
            this.pnlGioHang.Controls.Add(this.lblTongTien);
            this.pnlGioHang.Controls.Add(this.btnXacNhan);
            this.pnlGioHang.Controls.Add(this.btnHuy);
            this.splitMain.Panel2.Controls.Add(this.pnlGioHang);
            // ucTaoDon
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(250, 245, 246);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.lblTitle);
            this.Padding = new System.Windows.Forms.Padding(20, 16, 20, 20);
            this.Size = new System.Drawing.Size(840, 560);
            this.Load += new System.EventHandler(this.ucTaoDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            this.splitMain.ResumeLayout(false);
            this.pnlKhachHang.ResumeLayout(false);
            this.pnlKhachHang.PerformLayout();
            this.pnlSanPham.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.pnlTimSP.ResumeLayout(false);
            this.pnlTimSP.PerformLayout();
            this.pnlGioHang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.GroupBox pnlKhachHang;
        private System.Windows.Forms.GroupBox pnlSanPham;
        private System.Windows.Forms.GroupBox pnlGioHang;
        private System.Windows.Forms.Label lblTenKH; private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label lblSDT; private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblHinhThuc; private System.Windows.Forms.ComboBox cboHinhThuc;
        private System.Windows.Forms.Label lblDiaChi; private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.Label lblGhiChu; private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.DataGridView dgvSanPham;
        private System.Windows.Forms.Panel pnlTimSP;
        private System.Windows.Forms.TextBox txtTimSP; private System.Windows.Forms.Button btnTimSP; private System.Windows.Forms.Button btnThemSP;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Button btnXoaSP;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Label lblTitle;
    }
}
