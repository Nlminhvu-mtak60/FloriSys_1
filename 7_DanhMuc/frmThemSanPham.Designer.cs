namespace FloriSys._7_DanhMuc
{
    partial class frmThemSanPham
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

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMaSP = new System.Windows.Forms.Label();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.lblLoai = new System.Windows.Forms.Label();
            this.cboLoai = new System.Windows.Forms.ComboBox();
            this.lblGiaBan = new System.Windows.Forms.Label();
            this.numGiaBan = new System.Windows.Forms.NumericUpDown();
            this.lblGiaNhap = new System.Windows.Forms.Label();
            this.numGiaNhap = new System.Windows.Forms.NumericUpDown();
            this.lblToiThieu = new System.Windows.Forms.Label();
            this.numToiThieu = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBackground = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToiThieu)).BeginInit();
            this.pnlBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Thêm sản phẩm mới";
            // 
            // lblMaSP
            // 
            this.lblMaSP.AutoSize = true;
            this.lblMaSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaSP.Location = new System.Drawing.Point(30, 90);
            this.lblMaSP.Name = "lblMaSP";
            this.lblMaSP.Size = new System.Drawing.Size(101, 20);
            this.lblMaSP.TabIndex = 1;
            this.lblMaSP.Text = "Mã sản phẩm:";
            // 
            // txtMaSP
            // 
            this.txtMaSP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaSP.Location = new System.Drawing.Point(150, 85);
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.ReadOnly = true;
            this.txtMaSP.Size = new System.Drawing.Size(250, 30);
            this.txtMaSP.TabIndex = 2;
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenSP.Location = new System.Drawing.Point(30, 140);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(103, 20);
            this.lblTenSP.TabIndex = 3;
            this.lblTenSP.Text = "Tên sản phẩm:";
            // 
            // txtTenSP
            // 
            this.txtTenSP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenSP.Location = new System.Drawing.Point(150, 135);
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(250, 30);
            this.txtTenSP.TabIndex = 4;
            // 
            // lblLoai
            // 
            this.lblLoai.AutoSize = true;
            this.lblLoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoai.Location = new System.Drawing.Point(30, 190);
            this.lblLoai.Name = "lblLoai";
            this.lblLoai.Size = new System.Drawing.Size(68, 20);
            this.lblLoai.TabIndex = 5;
            this.lblLoai.Text = "Loại hoa:";
            // 
            // cboLoai
            // 
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoai.FormattingEnabled = true;
            this.cboLoai.Items.AddRange(new object[] {
            "Hoa tươi",
            "Bó hoa",
            "Lẵng hoa",
            "Phụ kiện",
            "Chậu cây"});
            this.cboLoai.Location = new System.Drawing.Point(150, 185);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(250, 31);
            this.cboLoai.TabIndex = 6;
            // 
            // lblGiaBan
            // 
            this.lblGiaBan.AutoSize = true;
            this.lblGiaBan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiaBan.Location = new System.Drawing.Point(30, 240);
            this.lblGiaBan.Name = "lblGiaBan";
            this.lblGiaBan.Size = new System.Drawing.Size(63, 20);
            this.lblGiaBan.TabIndex = 7;
            this.lblGiaBan.Text = "Giá bán:";
            // 
            // numGiaBan
            // 
            this.numGiaBan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numGiaBan.Location = new System.Drawing.Point(150, 235);
            this.numGiaBan.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiaBan.Name = "numGiaBan";
            this.numGiaBan.Size = new System.Drawing.Size(250, 30);
            this.numGiaBan.TabIndex = 8;
            // 
            // lblGiaNhap
            // 
            this.lblGiaNhap.AutoSize = true;
            this.lblGiaNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGiaNhap.Location = new System.Drawing.Point(30, 290);
            this.lblGiaNhap.Name = "lblGiaNhap";
            this.lblGiaNhap.Size = new System.Drawing.Size(71, 20);
            this.lblGiaNhap.TabIndex = 9;
            this.lblGiaNhap.Text = "Giá nhập:";
            // 
            // numGiaNhap
            // 
            this.numGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numGiaNhap.Location = new System.Drawing.Point(150, 285);
            this.numGiaNhap.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.numGiaNhap.Name = "numGiaNhap";
            this.numGiaNhap.Size = new System.Drawing.Size(250, 30);
            this.numGiaNhap.TabIndex = 10;
            // 
            // lblToiThieu
            // 
            this.lblToiThieu.AutoSize = true;
            this.lblToiThieu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblToiThieu.Location = new System.Drawing.Point(30, 340);
            this.lblToiThieu.Name = "lblToiThieu";
            this.lblToiThieu.Size = new System.Drawing.Size(100, 20);
            this.lblToiThieu.TabIndex = 11;
            this.lblToiThieu.Text = "Tồn tối thiểu:";
            // 
            // numToiThieu
            // 
            this.numToiThieu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numToiThieu.Location = new System.Drawing.Point(150, 335);
            this.numToiThieu.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numToiThieu.Name = "numToiThieu";
            this.numToiThieu.Size = new System.Drawing.Size(250, 30);
            this.numToiThieu.TabIndex = 12;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(280, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.Gray;
            this.btnCancel.Location = new System.Drawing.Point(150, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlBackground
            // 
            this.pnlBackground.BackColor = System.Drawing.Color.White;
            this.pnlBackground.Controls.Add(this.btnCancel);
            this.pnlBackground.Controls.Add(this.btnSave);
            this.pnlBackground.Controls.Add(this.numToiThieu);
            this.pnlBackground.Controls.Add(this.lblToiThieu);
            this.pnlBackground.Controls.Add(this.numGiaNhap);
            this.pnlBackground.Controls.Add(this.lblGiaNhap);
            this.pnlBackground.Controls.Add(this.numGiaBan);
            this.pnlBackground.Controls.Add(this.lblGiaBan);
            this.pnlBackground.Controls.Add(this.cboLoai);
            this.pnlBackground.Controls.Add(this.lblLoai);
            this.pnlBackground.Controls.Add(this.txtTenSP);
            this.pnlBackground.Controls.Add(this.lblTenSP);
            this.pnlBackground.Controls.Add(this.txtMaSP);
            this.pnlBackground.Controls.Add(this.lblMaSP);
            this.pnlBackground.Controls.Add(this.lblTitle);
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Location = new System.Drawing.Point(10, 10);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(434, 461);
            this.pnlBackground.TabIndex = 0;
            // 
            // frmThemSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(454, 481);
            this.Controls.Add(this.pnlBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemSanPham";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm sản phẩm";
            this.Load += new System.EventHandler(this.frmThemSanPham_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numGiaBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numToiThieu)).EndInit();
            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.Label lblLoai;
        private System.Windows.Forms.ComboBox cboLoai;
        private System.Windows.Forms.Label lblGiaBan;
        private System.Windows.Forms.NumericUpDown numGiaBan;
        private System.Windows.Forms.Label lblGiaNhap;
        private System.Windows.Forms.NumericUpDown numGiaNhap;
        private System.Windows.Forms.Label lblToiThieu;
        private System.Windows.Forms.NumericUpDown numToiThieu;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlBackground;
    }
}
