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
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 100);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(800, 36);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Nhập kho mới";
            // 
            // cboSanPham
            // 
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSanPham.Location = new System.Drawing.Point(75, 3);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(200, 31);
            this.cboSanPham.TabIndex = 1;
            // 
            // numSoLuong
            // 
            this.numSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSoLuong.Location = new System.Drawing.Point(315, 3);
            this.numSoLuong.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(70, 30);
            this.numSoLuong.TabIndex = 3;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numGiaNhap
            // 
            this.numGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numGiaNhap.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numGiaNhap.Location = new System.Drawing.Point(460, 3);
            this.numGiaNhap.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numGiaNhap.Name = "numGiaNhap";
            this.numGiaNhap.Size = new System.Drawing.Size(120, 30);
            this.numGiaNhap.TabIndex = 5;
            // 
            // btnThemDong
            // 
            this.btnThemDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnThemDong.FlatAppearance.BorderSize = 0;
            this.btnThemDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemDong.ForeColor = System.Drawing.Color.White;
            this.btnThemDong.Location = new System.Drawing.Point(590, 3);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(100, 28);
            this.btnThemDong.TabIndex = 6;
            this.btnThemDong.Text = "+ Thêm dòng";
            this.btnThemDong.UseVisualStyleBackColor = false;
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            // 
            // dgvNhap
            // 
            this.dgvNhap.AllowUserToAddRows = false;
            this.dgvNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhap.ColumnHeadersHeight = 29;
            this.dgvNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhap.Location = new System.Drawing.Point(20, 20);
            this.dgvNhap.Name = "dgvNhap";
            this.dgvNhap.RowHeadersVisible = false;
            this.dgvNhap.RowHeadersWidth = 51;
            this.dgvNhap.Size = new System.Drawing.Size(800, 520);
            this.dgvNhap.TabIndex = 3;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(0, 40);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(500, 30);
            this.txtGhiChu.TabIndex = 7;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(20, 500);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(800, 40);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "💾 Lưu phiếu nhập";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.lblSP);
            this.pnlInput.Controls.Add(this.cboSanPham);
            this.pnlInput.Controls.Add(this.lblSL);
            this.pnlInput.Controls.Add(this.numSoLuong);
            this.pnlInput.Controls.Add(this.lblGia);
            this.pnlInput.Controls.Add(this.numGiaNhap);
            this.pnlInput.Controls.Add(this.btnThemDong);
            this.pnlInput.Controls.Add(this.txtGhiChu);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(20, 20);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(800, 80);
            this.pnlInput.TabIndex = 1;
            // 
            // lblSP
            // 
            this.lblSP.AutoSize = true;
            this.lblSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSP.Location = new System.Drawing.Point(0, 5);
            this.lblSP.Name = "lblSP";
            this.lblSP.Size = new System.Drawing.Size(78, 20);
            this.lblSP.TabIndex = 0;
            this.lblSP.Text = "Sản phẩm:";
            // 
            // lblSL
            // 
            this.lblSL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSL.Location = new System.Drawing.Point(285, 5);
            this.lblSL.Name = "lblSL";
            this.lblSL.Size = new System.Drawing.Size(30, 20);
            this.lblSL.TabIndex = 2;
            this.lblSL.Text = "SL:";
            // 
            // lblGia
            // 
            this.lblGia.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGia.Location = new System.Drawing.Point(395, 5);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(60, 20);
            this.lblGia.TabIndex = 4;
            this.lblGia.Text = "Giá nhập:";
            // 
            // ucNhapKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.dgvNhap);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnLuu);

            this.Name = "ucNhapKho";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(840, 560);
            this.Load += new System.EventHandler(this.ucNhapKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhap)).EndInit();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
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
