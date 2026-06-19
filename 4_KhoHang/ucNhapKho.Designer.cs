namespace FloriSys._4_KhoHang
{
    partial class ucNhapKho
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }
        #region Component Designer generated code
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.grpGhiChu = new System.Windows.Forms.GroupBox();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.grpThemSP = new System.Windows.Forms.GroupBox();
            this.tableInput = new System.Windows.Forms.TableLayoutPanel();
            this.lblSP = new System.Windows.Forms.Label();
            this.cboSanPham = new System.Windows.Forms.ComboBox();
            this.lblSL = new System.Windows.Forms.Label();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.lblGia = new System.Windows.Forms.Label();
            this.numGiaNhap = new System.Windows.Forms.NumericUpDown();
            this.btnThemDong = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.grpDanhSach = new System.Windows.Forms.GroupBox();
            this.dgvNhap = new System.Windows.Forms.DataGridView();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGiaNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnXoaDong = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.grpGhiChu.SuspendLayout();
            this.grpThemSP.SuspendLayout();
            this.tableInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.grpDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhap)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(20, 20);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(984, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.lblSubTitle.Location = new System.Drawing.Point(2, 38);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(344, 23);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Thêm sản phẩm và tạo phiếu nhập kho mới";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(225, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Nhập kho mới";
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.grpGhiChu);
            this.pnlInput.Controls.Add(this.grpThemSP);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Location = new System.Drawing.Point(20, 80);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.pnlInput.Size = new System.Drawing.Size(984, 175);
            this.pnlInput.TabIndex = 1;
            // 
            // grpGhiChu
            // 
            this.grpGhiChu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGhiChu.Controls.Add(this.txtGhiChu);
            this.grpGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpGhiChu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.grpGhiChu.Location = new System.Drawing.Point(0, 100);
            this.grpGhiChu.Name = "grpGhiChu";
            this.grpGhiChu.Padding = new System.Windows.Forms.Padding(12, 5, 12, 8);
            this.grpGhiChu.Size = new System.Drawing.Size(984, 65);
            this.grpGhiChu.TabIndex = 1;
            this.grpGhiChu.TabStop = false;
            this.grpGhiChu.Text = "📝 Ghi chú phiếu nhập";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(12, 28);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(960, 30);
            this.txtGhiChu.TabIndex = 0;
            // 
            // grpThemSP
            // 
            this.grpThemSP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThemSP.Controls.Add(this.tableInput);
            this.grpThemSP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpThemSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.grpThemSP.Location = new System.Drawing.Point(0, 5);
            this.grpThemSP.Name = "grpThemSP";
            this.grpThemSP.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.grpThemSP.Size = new System.Drawing.Size(984, 90);
            this.grpThemSP.TabIndex = 0;
            this.grpThemSP.TabStop = false;
            this.grpThemSP.Text = "📦 Thêm sản phẩm";
            // 
            // tableInput
            // 
            this.tableInput.ColumnCount = 7;
            this.tableInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableInput.Controls.Add(this.lblSP, 0, 0);
            this.tableInput.Controls.Add(this.cboSanPham, 1, 0);
            this.tableInput.Controls.Add(this.lblSL, 2, 0);
            this.tableInput.Controls.Add(this.numSoLuong, 3, 0);
            this.tableInput.Controls.Add(this.lblGia, 4, 0);
            this.tableInput.Controls.Add(this.numGiaNhap, 5, 0);
            this.tableInput.Controls.Add(this.btnThemDong, 6, 0);
            this.tableInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableInput.Location = new System.Drawing.Point(12, 31);
            this.tableInput.Name = "tableInput";
            this.tableInput.RowCount = 1;
            this.tableInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableInput.Size = new System.Drawing.Size(960, 51);
            this.tableInput.TabIndex = 0;
            // 
            // lblSP
            // 
            this.lblSP.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSP.AutoSize = true;
            this.lblSP.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblSP.Location = new System.Drawing.Point(3, 14);
            this.lblSP.Name = "lblSP";
            this.lblSP.Size = new System.Drawing.Size(91, 23);
            this.lblSP.TabIndex = 0;
            this.lblSP.Text = "Sản phẩm:";
            // 
            // cboSanPham
            // 
            this.cboSanPham.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboSanPham.Location = new System.Drawing.Point(100, 10);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(302, 31);
            this.cboSanPham.TabIndex = 1;
            this.cboSanPham.SelectedIndexChanged += new System.EventHandler(this.cboSanPham_SelectedIndexChanged);
            // 
            // lblSL
            // 
            this.lblSL.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSL.AutoSize = true;
            this.lblSL.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSL.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblSL.Location = new System.Drawing.Point(408, 14);
            this.lblSL.Name = "lblSL";
            this.lblSL.Size = new System.Drawing.Size(82, 23);
            this.lblSL.TabIndex = 2;
            this.lblSL.Text = "Số lượng:";
            // 
            // numSoLuong
            // 
            this.numSoLuong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numSoLuong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSoLuong.Location = new System.Drawing.Point(496, 10);
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
            this.numSoLuong.Size = new System.Drawing.Size(96, 30);
            this.numSoLuong.TabIndex = 3;
            this.numSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblGia
            // 
            this.lblGia.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblGia.AutoSize = true;
            this.lblGia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.lblGia.Location = new System.Drawing.Point(598, 14);
            this.lblGia.Name = "lblGia";
            this.lblGia.Size = new System.Drawing.Size(83, 23);
            this.lblGia.TabIndex = 4;
            this.lblGia.Text = "Giá nhập:";
            // 
            // numGiaNhap
            // 
            this.numGiaNhap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.numGiaNhap.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numGiaNhap.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numGiaNhap.Location = new System.Drawing.Point(687, 10);
            this.numGiaNhap.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numGiaNhap.Name = "numGiaNhap";
            this.numGiaNhap.Size = new System.Drawing.Size(165, 30);
            this.numGiaNhap.TabIndex = 5;
            this.numGiaNhap.ThousandsSeparator = true;
            // 
            // btnThemDong
            // 
            this.btnThemDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThemDong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnThemDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDong.FlatAppearance.BorderSize = 0;
            this.btnThemDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemDong.ForeColor = System.Drawing.Color.White;
            this.btnThemDong.Location = new System.Drawing.Point(858, 8);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(99, 35);
            this.btnThemDong.TabIndex = 6;
            this.btnThemDong.Text = "+ Thêm";
            this.btnThemDong.UseVisualStyleBackColor = false;
            this.btnThemDong.Click += new System.EventHandler(this.btnThemDong_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblTongCong);
            this.pnlBottom.Controls.Add(this.btnLuu);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(20, 580);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(984, 80);
            this.pnlBottom.TabIndex = 3;
            // 
            // lblTongCong
            // 
            this.lblTongCong.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTongCong.Font = new System.Drawing.Font("Georgia", 13F, System.Drawing.FontStyle.Bold);
            this.lblTongCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblTongCong.Location = new System.Drawing.Point(0, 0);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.lblTongCong.Size = new System.Drawing.Size(984, 35);
            this.lblTongCong.TabIndex = 0;
            this.lblTongCong.Text = "Tổng giá trị: 0 đ";
            this.lblTongCong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(0, 38);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(984, 42);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "💾 Lưu phiếu nhập";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // grpDanhSach
            // 
            this.grpDanhSach.Controls.Add(this.dgvNhap);
            this.grpDanhSach.Controls.Add(this.btnXoaDong);
            this.grpDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.grpDanhSach.Location = new System.Drawing.Point(20, 255);
            this.grpDanhSach.Name = "grpDanhSach";
            this.grpDanhSach.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.grpDanhSach.Size = new System.Drawing.Size(984, 325);
            this.grpDanhSach.TabIndex = 2;
            this.grpDanhSach.TabStop = false;
            this.grpDanhSach.Text = "📋 Danh sách sản phẩm nhập";
            // 
            // dgvNhap
            // 
            this.dgvNhap.AllowUserToAddRows = false;
            this.dgvNhap.AllowUserToDeleteRows = false;
            this.dgvNhap.BackgroundColor = System.Drawing.Color.White;
            this.dgvNhap.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvNhap.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvNhap.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.dgvNhap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNhap.ColumnHeadersHeight = 40;
            this.dgvNhap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvNhap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSP,
            this.colTenSP,
            this.colSoLuong,
            this.colGiaNhap});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNhap.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNhap.EnableHeadersVisualStyles = false;
            this.dgvNhap.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.dgvNhap.Location = new System.Drawing.Point(12, 31);
            this.dgvNhap.Name = "dgvNhap";
            this.dgvNhap.ReadOnly = true;
            this.dgvNhap.RowHeadersVisible = false;
            this.dgvNhap.RowHeadersWidth = 51;
            this.dgvNhap.RowTemplate.Height = 38;
            this.dgvNhap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNhap.Size = new System.Drawing.Size(960, 251);
            this.dgvNhap.TabIndex = 0;
            // 
            // colMaSP
            // 
            this.colMaSP.DataPropertyName = "MaSP";
            this.colMaSP.HeaderText = "Mã sản phẩm";
            this.colMaSP.MinimumWidth = 6;
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.ReadOnly = true;
            this.colMaSP.Width = 140;
            // 
            // colTenSP
            // 
            this.colTenSP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTenSP.DataPropertyName = "TenSP";
            this.colTenSP.HeaderText = "Tên sản phẩm";
            this.colTenSP.MinimumWidth = 6;
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.ReadOnly = true;
            // 
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "SoLuong";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle2;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.MinimumWidth = 6;
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            this.colSoLuong.Width = 125;
            // 
            // colGiaNhap
            // 
            this.colGiaNhap.DataPropertyName = "GiaNhap";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N0";
            this.colGiaNhap.DefaultCellStyle = dataGridViewCellStyle3;
            this.colGiaNhap.HeaderText = "Giá nhập (đ)";
            this.colGiaNhap.MinimumWidth = 6;
            this.colGiaNhap.Name = "colGiaNhap";
            this.colGiaNhap.ReadOnly = true;
            this.colGiaNhap.Width = 150;
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.BackColor = System.Drawing.Color.White;
            this.btnXoaDong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnXoaDong.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(231)))), ((int)(((byte)(235)))));
            this.btnXoaDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoaDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnXoaDong.Location = new System.Drawing.Point(12, 282);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(960, 35);
            this.btnXoaDong.TabIndex = 1;
            this.btnXoaDong.Text = "✕ Xóa dòng đã chọn";
            this.btnXoaDong.UseVisualStyleBackColor = false;
            this.btnXoaDong.Click += new System.EventHandler(this.btnXoaDong_Click);
            // 
            // ucNhapKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.grpDanhSach);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlBottom);
            this.Name = "ucNhapKho";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(1024, 680);
            this.Load += new System.EventHandler(this.ucNhapKho_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.grpGhiChu.ResumeLayout(false);
            this.grpGhiChu.PerformLayout();
            this.grpThemSP.ResumeLayout(false);
            this.tableInput.ResumeLayout(false);
            this.tableInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGiaNhap)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.grpDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNhap)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.GroupBox grpThemSP;
        private System.Windows.Forms.TableLayoutPanel tableInput;
        private System.Windows.Forms.Label lblSP;
        private System.Windows.Forms.ComboBox cboSanPham;
        private System.Windows.Forms.Label lblSL;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.Label lblGia;
        private System.Windows.Forms.NumericUpDown numGiaNhap;
        private System.Windows.Forms.Button btnThemDong;
        private System.Windows.Forms.GroupBox grpGhiChu;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.GroupBox grpDanhSach;
        private System.Windows.Forms.DataGridView dgvNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGiaNhap;
        private System.Windows.Forms.Button btnXoaDong;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Button btnLuu;
    }
}