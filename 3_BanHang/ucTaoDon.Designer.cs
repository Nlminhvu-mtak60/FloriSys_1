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
            this.pnlSanPham = new System.Windows.Forms.GroupBox();
            this.dgvSanPham = new System.Windows.Forms.DataGridView();
            this.pnlTimSP = new System.Windows.Forms.Panel();
            this.txtTimSP = new System.Windows.Forms.TextBox();
            this.btnTimSP = new System.Windows.Forms.Button();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.pnlKhachHang = new System.Windows.Forms.GroupBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.cboHinhThuc = new System.Windows.Forms.ComboBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
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
            this.pnlSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).BeginInit();
            this.pnlTimSP.SuspendLayout();
            this.pnlKhachHang.SuspendLayout();
            this.pnlGioHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(15, 42);
            this.splitMain.Margin = new System.Windows.Forms.Padding(2);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlSanPham);
            this.splitMain.Panel1.Controls.Add(this.pnlKhachHang);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlGioHang);
            this.splitMain.Size = new System.Drawing.Size(600, 397);
            this.splitMain.SplitterDistance = 330;
            this.splitMain.SplitterWidth = 3;
            this.splitMain.TabIndex = 0;
            // 
            // pnlSanPham
            // 
            this.pnlSanPham.Controls.Add(this.dgvSanPham);
            this.pnlSanPham.Controls.Add(this.pnlTimSP);
            this.pnlSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSanPham.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlSanPham.ForeColor = System.Drawing.Color.Black;
            this.pnlSanPham.Location = new System.Drawing.Point(0, 162);
            this.pnlSanPham.Margin = new System.Windows.Forms.Padding(2);
            this.pnlSanPham.Name = "pnlSanPham";
            this.pnlSanPham.Padding = new System.Windows.Forms.Padding(8);
            this.pnlSanPham.Size = new System.Drawing.Size(330, 235);
            this.pnlSanPham.TabIndex = 0;
            this.pnlSanPham.TabStop = false;
            this.pnlSanPham.Text = "🌸 Chọn sản phẩm";
            // 
            // dgvSanPham
            // 
            this.dgvSanPham.AllowUserToAddRows = false;
            this.dgvSanPham.BackgroundColor = System.Drawing.Color.White;
            this.dgvSanPham.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSanPham.ColumnHeadersHeight = 29;
            this.dgvSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSanPham.Location = new System.Drawing.Point(8, 55);
            this.dgvSanPham.Margin = new System.Windows.Forms.Padding(2);
            this.dgvSanPham.Name = "dgvSanPham";
            this.dgvSanPham.ReadOnly = true;
            this.dgvSanPham.RowHeadersVisible = false;
            this.dgvSanPham.RowHeadersWidth = 51;
            this.dgvSanPham.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSanPham.Size = new System.Drawing.Size(314, 172);
            this.dgvSanPham.TabIndex = 0;
            // 
            // pnlTimSP
            // 
            this.pnlTimSP.Controls.Add(this.txtTimSP);
            this.pnlTimSP.Controls.Add(this.btnTimSP);
            this.pnlTimSP.Controls.Add(this.btnThemSP);
            this.pnlTimSP.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimSP.Location = new System.Drawing.Point(8, 26);
            this.pnlTimSP.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTimSP.Name = "pnlTimSP";
            this.pnlTimSP.Size = new System.Drawing.Size(314, 29);
            this.pnlTimSP.TabIndex = 1;
            // 
            // txtTimSP
            // 
            this.txtTimSP.Location = new System.Drawing.Point(2, 2);
            this.txtTimSP.Margin = new System.Windows.Forms.Padding(2);
            this.txtTimSP.Name = "txtTimSP";
            this.txtTimSP.Size = new System.Drawing.Size(114, 25);
            this.txtTimSP.TabIndex = 0;
            // 
            // btnTimSP
            // 
            this.btnTimSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimSP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnTimSP.Location = new System.Drawing.Point(128, 0);
            this.btnTimSP.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimSP.Name = "btnTimSP";
            this.btnTimSP.Size = new System.Drawing.Size(55, 27);
            this.btnTimSP.TabIndex = 1;
            this.btnTimSP.Text = "Tìm";
            this.btnTimSP.Click += new System.EventHandler(this.btnTimSP_Click);
            // 
            // btnThemSP
            // 
            this.btnThemSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnThemSP.FlatAppearance.BorderSize = 0;
            this.btnThemSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemSP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnThemSP.ForeColor = System.Drawing.Color.White;
            this.btnThemSP.Location = new System.Drawing.Point(188, 0);
            this.btnThemSP.Margin = new System.Windows.Forms.Padding(2);
            this.btnThemSP.Name = "btnThemSP";
            this.btnThemSP.Size = new System.Drawing.Size(60, 27);
            this.btnThemSP.TabIndex = 2;
            this.btnThemSP.Text = "+ Thêm";
            this.btnThemSP.UseVisualStyleBackColor = false;
            this.btnThemSP.Click += new System.EventHandler(this.btnThemSP_Click);
            // 
            // pnlKhachHang
            // 
            this.pnlKhachHang.Controls.Add(this.lblTenKH);
            this.pnlKhachHang.Controls.Add(this.txtTenKH);
            this.pnlKhachHang.Controls.Add(this.lblSDT);
            this.pnlKhachHang.Controls.Add(this.txtSDT);
            this.pnlKhachHang.Controls.Add(this.lblHinhThuc);
            this.pnlKhachHang.Controls.Add(this.cboHinhThuc);
            this.pnlKhachHang.Controls.Add(this.lblDiaChi);
            this.pnlKhachHang.Controls.Add(this.txtDiaChi);
            this.pnlKhachHang.Controls.Add(this.lblGhiChu);
            this.pnlKhachHang.Controls.Add(this.txtGhiChu);
            this.pnlKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlKhachHang.Location = new System.Drawing.Point(0, 0);
            this.pnlKhachHang.Margin = new System.Windows.Forms.Padding(2);
            this.pnlKhachHang.Name = "pnlKhachHang";
            this.pnlKhachHang.Padding = new System.Windows.Forms.Padding(8);
            this.pnlKhachHang.Size = new System.Drawing.Size(330, 162);
            this.pnlKhachHang.TabIndex = 1;
            this.pnlKhachHang.TabStop = false;
            this.pnlKhachHang.Text = "👤 Thông tin khách hàng";
            // 
            // lblTenKH
            // 
            this.lblTenKH.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenKH.Location = new System.Drawing.Point(10, 24);
            this.lblTenKH.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(60, 18);
            this.lblTenKH.TabIndex = 0;
            this.lblTenKH.Text = "Họ tên:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenKH.Location = new System.Drawing.Point(75, 23);
            this.txtTenKH.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(234, 25);
            this.txtTenKH.TabIndex = 1;
            // 
            // lblSDT
            // 
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSDT.Location = new System.Drawing.Point(10, 49);
            this.lblSDT.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(60, 18);
            this.lblSDT.TabIndex = 2;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtSDT
            // 
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSDT.Location = new System.Drawing.Point(75, 47);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(2);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(234, 25);
            this.txtSDT.TabIndex = 3;
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHinhThuc.Location = new System.Drawing.Point(10, 73);
            this.lblHinhThuc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(60, 18);
            this.lblHinhThuc.TabIndex = 4;
            this.lblHinhThuc.Text = "Hình thức:";
            // 
            // cboHinhThuc
            // 
            this.cboHinhThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHinhThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHinhThuc.Location = new System.Drawing.Point(75, 72);
            this.cboHinhThuc.Margin = new System.Windows.Forms.Padding(2);
            this.cboHinhThuc.Name = "cboHinhThuc";
            this.cboHinhThuc.Size = new System.Drawing.Size(234, 25);
            this.cboHinhThuc.TabIndex = 5;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiaChi.Location = new System.Drawing.Point(10, 98);
            this.lblDiaChi.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(60, 18);
            this.lblDiaChi.TabIndex = 6;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiaChi.Location = new System.Drawing.Point(75, 96);
            this.txtDiaChi.Margin = new System.Windows.Forms.Padding(2);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(234, 25);
            this.txtDiaChi.TabIndex = 7;
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGhiChu.Location = new System.Drawing.Point(10, 122);
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(60, 18);
            this.lblGhiChu.TabIndex = 8;
            this.lblGhiChu.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(75, 120);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(2);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(234, 33);
            this.txtGhiChu.TabIndex = 9;
            // 
            // pnlGioHang
            // 
            this.pnlGioHang.Controls.Add(this.dgvGioHang);
            this.pnlGioHang.Controls.Add(this.btnXoaSP);
            this.pnlGioHang.Controls.Add(this.lblTongTien);
            this.pnlGioHang.Controls.Add(this.btnXacNhan);
            this.pnlGioHang.Controls.Add(this.btnHuy);
            this.pnlGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGioHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlGioHang.Location = new System.Drawing.Point(0, 0);
            this.pnlGioHang.Margin = new System.Windows.Forms.Padding(2);
            this.pnlGioHang.Name = "pnlGioHang";
            this.pnlGioHang.Padding = new System.Windows.Forms.Padding(8);
            this.pnlGioHang.Size = new System.Drawing.Size(267, 397);
            this.pnlGioHang.TabIndex = 0;
            this.pnlGioHang.TabStop = false;
            this.pnlGioHang.Text = "🛒 Giỏ hàng";
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvGioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvGioHang.ColumnHeadersHeight = 29;
            this.dgvGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGioHang.Location = new System.Drawing.Point(8, 26);
            this.dgvGioHang.Margin = new System.Windows.Forms.Padding(2);
            this.dgvGioHang.Name = "dgvGioHang";
            this.dgvGioHang.RowHeadersVisible = false;
            this.dgvGioHang.RowHeadersWidth = 51;
            this.dgvGioHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGioHang.Size = new System.Drawing.Size(251, 250);
            this.dgvGioHang.TabIndex = 0;
            // 
            // btnXoaSP
            // 
            this.btnXoaSP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnXoaSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoaSP.Location = new System.Drawing.Point(8, 276);
            this.btnXoaSP.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoaSP.Name = "btnXoaSP";
            this.btnXoaSP.Size = new System.Drawing.Size(251, 24);
            this.btnXoaSP.TabIndex = 1;
            this.btnXoaSP.Text = "✕ Xóa SP chọn";
            this.btnXoaSP.Click += new System.EventHandler(this.btnXoaSP_Click);
            // 
            // lblTongTien
            // 
            this.lblTongTien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTongTien.Font = new System.Drawing.Font("Georgia", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblTongTien.Location = new System.Drawing.Point(8, 300);
            this.lblTongTien.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(251, 29);
            this.lblTongTien.TabIndex = 2;
            this.lblTongTien.Text = "Tổng cộng: 0đ";
            this.lblTongTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnXacNhan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXacNhan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnXacNhan.FlatAppearance.BorderSize = 0;
            this.btnXacNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(8, 329);
            this.btnXacNhan.Margin = new System.Windows.Forms.Padding(2);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(251, 32);
            this.btnXacNhan.TabIndex = 3;
            this.btnXacNhan.Text = "✅ Xác nhận tạo đơn";
            this.btnXacNhan.UseVisualStyleBackColor = false;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHuy.Location = new System.Drawing.Point(8, 361);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(251, 28);
            this.btnHuy.TabIndex = 4;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(15, 13);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(600, 29);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Tạo đơn hàng mới";
            // 
            // ucTaoDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ucTaoDon";
            this.Padding = new System.Windows.Forms.Padding(15, 13, 15, 16);
            this.Size = new System.Drawing.Size(630, 455);
            this.Load += new System.EventHandler(this.ucTaoDon_Load);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlSanPham.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPham)).EndInit();
            this.pnlTimSP.ResumeLayout(false);
            this.pnlTimSP.PerformLayout();
            this.pnlKhachHang.ResumeLayout(false);
            this.pnlKhachHang.PerformLayout();
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
