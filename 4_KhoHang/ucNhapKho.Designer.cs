namespace FloriSys._4_KhoHang
{
    partial class ucNhapKho
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }
        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.numGiaNhap = new System.Windows.Forms.NumericUpDown();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.dgvNhap = new System.Windows.Forms.DataGridView();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.btnLuu = new System.Windows.Forms.Button();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.lblSP = new System.Windows.Forms.Label();
            this.lblSL = new System.Windows.Forms.Label();
            this.lblGia = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhap)).BeginInit();
            this.pnlInput.SuspendLayout();
            this.SuspendLayout();
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top; this.lblTitle.Size = new System.Drawing.Size(800, 36); this.lblTitle.Text = "Nhập kho mới";
            // pnlInput
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top; this.pnlInput.Size = new System.Drawing.Size(800, 80);
            this.lblSP.Text = "Sản phẩm:"; this.lblSP.Location = new System.Drawing.Point(0, 5); this.lblSP.Size = new System.Drawing.Size(70, 20); this.lblSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSanPham.Location = new System.Drawing.Point(75, 3); this.cboSanPham.Size = new System.Drawing.Size(200, 28); this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSL.Text = "SL:"; this.lblSL.Location = new System.Drawing.Point(285, 5); this.lblSL.Size = new System.Drawing.Size(30, 20); this.lblSL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numSoLuong.Location = new System.Drawing.Point(315, 3); this.numSoLuong.Size = new System.Drawing.Size(70, 28); this.numSoLuong.Minimum = 1; this.numSoLuong.Maximum = 9999; this.numSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGia.Text = "Giá nhập:"; this.lblGia.Location = new System.Drawing.Point(395, 5); this.lblGia.Size = new System.Drawing.Size(60, 20); this.lblGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.numGiaNhap.Location = new System.Drawing.Point(460, 3); this.numGiaNhap.Size = new System.Drawing.Size(120, 28); this.numGiaNhap.Maximum = 99999999; this.numGiaNhap.Increment = 1000; this.numGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnThemDong.Location = new System.Drawing.Point(590, 3); this.btnThemDong.Size = new System.Drawing.Size(100, 28); this.btnThemDong.Text = "+ Thêm dòng"; this.btnThemDong.BackColor = System.Drawing.Color.FromArgb(232, 57, 77); this.btnThemDong.ForeColor = System.Drawing.Color.White; this.btnThemDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnThemDong.FlatAppearance.BorderSize = 0; this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 9F); this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            //this.txtGhiChu.Location = new System.Drawing.Point(0, 40); this.txtGhiChu.Size = new System.Drawing.Size(500, 26); this.txtGhiChu.PlaceholderText = "Ghi chú phiếu nhập..."; this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.pnlInput.Controls.AddRange(new System.Windows.Forms.Control[] { this.lblSP, this.cboSanPham, this.lblSL, this.numSoLuong, this.lblGia, this.numGiaNhap, this.btnThemDong, this.txtGhiChu });
            // dgvNhap
            this.dgvNhap.Dock = System.Windows.Forms.DockStyle.Fill; this.dgvNhap.AllowUserToAddRows = false; this.dgvNhap.RowHeadersVisible = false;
            this.dgvNhap.BackgroundColor = System.Drawing.Color.White; this.dgvNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            // btnLuu
            this.btnLuu.Dock = System.Windows.Forms.DockStyle.Bottom; this.btnLuu.Size = new System.Drawing.Size(800, 40); this.btnLuu.Text = "💾 Lưu phiếu nhập"; this.btnLuu.BackColor = System.Drawing.Color.FromArgb(232, 57, 77); this.btnLuu.ForeColor = System.Drawing.Color.White; this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat; this.btnLuu.FlatAppearance.BorderSize = 0; this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold); this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand; this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // ucNhapKho
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F); this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(250, 245, 246); this.Padding = new System.Windows.Forms.Padding(20);
            this.Controls.Add(this.dgvNhap); this.Controls.Add(this.btnLuu); this.Controls.Add(this.pnlInput); this.Controls.Add(this.lblTitle);
            this.Size = new System.Drawing.Size(840, 560);
            this.Load += new System.EventHandler(this.ucNhapKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhap)).EndInit();
            this.pnlInput.ResumeLayout(false); this.pnlInput.PerformLayout();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Label lblTitle; private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.NumericUpDown numSoLuong; private System.Windows.Forms.NumericUpDown numGiaNhap;
        private System.Windows.Forms.Button btnThemDong; private System.Windows.Forms.DataGridView dgvNhap;
        private System.Windows.Forms.TextBox txtGhiChu; private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Label lblSP; private System.Windows.Forms.Label lblSL; private System.Windows.Forms.Label lblGia;
    }
}
