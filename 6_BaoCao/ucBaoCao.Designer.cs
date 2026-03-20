namespace FloriSys._6_BaoCao
{
    partial class ucBaoCao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlSubNav = new System.Windows.Forms.Panel();
            this.btnBCNhanVien = new System.Windows.Forms.Button();
            this.btnBCTonKho = new System.Windows.Forms.Button();
            this.btnBCSanPham = new System.Windows.Forms.Button();
            this.btnBCThang = new System.Windows.Forms.Button();
            this.btnBCNgay = new System.Windows.Forms.Button();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlSubNav.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSubNav
            // 
            this.pnlSubNav.BackColor = System.Drawing.Color.White;
            this.pnlSubNav.Controls.Add(this.btnBCNhanVien);
            this.pnlSubNav.Controls.Add(this.btnBCTonKho);
            this.pnlSubNav.Controls.Add(this.btnBCSanPham);
            this.pnlSubNav.Controls.Add(this.btnBCThang);
            this.pnlSubNav.Controls.Add(this.btnBCNgay);
            this.pnlSubNav.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubNav.Location = new System.Drawing.Point(0, 0);
            this.pnlSubNav.Name = "pnlSubNav";
            this.pnlSubNav.Size = new System.Drawing.Size(1000, 60);
            this.pnlSubNav.TabIndex = 0;
            // 
            // btnBCNhanVien
            // 
            this.btnBCNhanVien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBCNhanVien.FlatAppearance.BorderSize = 0;
            this.btnBCNhanVien.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBCNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBCNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnBCNhanVien.Location = new System.Drawing.Point(600, 0);
            this.btnBCNhanVien.Name = "btnBCNhanVien";
            this.btnBCNhanVien.Size = new System.Drawing.Size(150, 60);
            this.btnBCNhanVien.TabIndex = 4;
            this.btnBCNhanVien.Text = "👤 Nhân viên";
            this.btnBCNhanVien.UseVisualStyleBackColor = true;
            this.btnBCNhanVien.Click += new System.EventHandler(this.btnBC_Click);
            // 
            // btnBCTonKho
            // 
            this.btnBCTonKho.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBCTonKho.FlatAppearance.BorderSize = 0;
            this.btnBCTonKho.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBCTonKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBCTonKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnBCTonKho.Location = new System.Drawing.Point(450, 0);
            this.btnBCTonKho.Name = "btnBCTonKho";
            this.btnBCTonKho.Size = new System.Drawing.Size(150, 60);
            this.btnBCTonKho.TabIndex = 3;
            this.btnBCTonKho.Text = "📦 Tồn kho";
            this.btnBCTonKho.UseVisualStyleBackColor = true;
            this.btnBCTonKho.Click += new System.EventHandler(this.btnBC_Click);
            // 
            // btnBCSanPham
            // 
            this.btnBCSanPham.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBCSanPham.FlatAppearance.BorderSize = 0;
            this.btnBCSanPham.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBCSanPham.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBCSanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnBCSanPham.Location = new System.Drawing.Point(300, 0);
            this.btnBCSanPham.Name = "btnBCSanPham";
            this.btnBCSanPham.Size = new System.Drawing.Size(150, 60);
            this.btnBCSanPham.TabIndex = 2;
            this.btnBCSanPham.Text = "🏆 Sản phẩm";
            this.btnBCSanPham.UseVisualStyleBackColor = true;
            this.btnBCSanPham.Click += new System.EventHandler(this.btnBC_Click);
            // 
            // btnBCThang
            // 
            this.btnBCThang.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBCThang.FlatAppearance.BorderSize = 0;
            this.btnBCThang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBCThang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBCThang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnBCThang.Location = new System.Drawing.Point(150, 0);
            this.btnBCThang.Name = "btnBCThang";
            this.btnBCThang.Size = new System.Drawing.Size(150, 60);
            this.btnBCThang.TabIndex = 1;
            this.btnBCThang.Text = "📆 Tháng";
            this.btnBCThang.UseVisualStyleBackColor = true;
            this.btnBCThang.Click += new System.EventHandler(this.btnBC_Click);
            // 
            // btnBCNgay
            // 
            this.btnBCNgay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBCNgay.FlatAppearance.BorderSize = 0;
            this.btnBCNgay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBCNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBCNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.btnBCNgay.Location = new System.Drawing.Point(0, 0);
            this.btnBCNgay.Name = "btnBCNgay";
            this.btnBCNgay.Size = new System.Drawing.Size(150, 60);
            this.btnBCNgay.TabIndex = 0;
            this.btnBCNgay.Text = "📅 Ngày";
            this.btnBCNgay.UseVisualStyleBackColor = true;
            this.btnBCNgay.Click += new System.EventHandler(this.btnBC_Click);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(0, 60);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1000, 660);
            this.pnlContainer.TabIndex = 1;
            // 
            // ucBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.pnlSubNav);
            this.Name = "ucBaoCao";
            this.Size = new System.Drawing.Size(1000, 720);
            this.Load += new System.EventHandler(this.ucBaoCao_Load);
            this.pnlSubNav.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSubNav;
        private System.Windows.Forms.Button btnBCNgay;
        private System.Windows.Forms.Button btnBCNhanVien;
        private System.Windows.Forms.Button btnBCTonKho;
        private System.Windows.Forms.Button btnBCSanPham;
        private System.Windows.Forms.Button btnBCThang;
        private System.Windows.Forms.Panel pnlContainer;
    }
}
