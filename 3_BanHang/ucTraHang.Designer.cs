namespace FloriSys._3_BanHang
{
    partial class ucTraHang
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.cardMain = new System.Windows.Forms.GroupBox();
            this.btnDuyet = new System.Windows.Forms.Button();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboHoanTien = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvSanPhamTra = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLyDo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaDon = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cardPending = new System.Windows.Forms.GroupBox();
            this.dgvChoTra = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.cardMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamTra)).BeginInit();
            this.cardPending.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChoTra)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTitle.Location = new System.Drawing.Point(20, 45);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(320, 20);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Xử lý hoàn trả hàng và hoàn tiền cho khách hàng";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(211, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Xử lý trả hàng";
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.Controls.Add(this.tlpMain);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 80);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(20);
            this.pnlContent.Size = new System.Drawing.Size(1100, 720);
            this.pnlContent.TabIndex = 1;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 550F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.cardMain, 0, 0);
            this.tlpMain.Controls.Add(this.cardPending, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(20, 20);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(1060, 680);
            this.tlpMain.TabIndex = 0;
            // 
            // cardMain
            // 
            this.cardMain.BackColor = System.Drawing.Color.White;
            this.cardMain.Controls.Add(this.btnDuyet);
            this.cardMain.Controls.Add(this.txtGhiChu);
            this.cardMain.Controls.Add(this.label5);
            this.cardMain.Controls.Add(this.cboHoanTien);
            this.cardMain.Controls.Add(this.label4);
            this.cardMain.Controls.Add(this.dgvSanPhamTra);
            this.cardMain.Controls.Add(this.label3);
            this.cardMain.Controls.Add(this.cboLyDo);
            this.cardMain.Controls.Add(this.label2);
            this.cardMain.Controls.Add(this.txtMaDon);
            this.cardMain.Controls.Add(this.label1);
            this.cardMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardMain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cardMain.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cardMain.Location = new System.Drawing.Point(3, 3);
            this.cardMain.Name = "cardMain";
            this.cardMain.Size = new System.Drawing.Size(544, 674);
            this.cardMain.TabIndex = 0;
            this.cardMain.TabStop = false;
            this.cardMain.Text = "↩️ Tạo đơn trả hàng";
            // 
            // btnDuyet
            // 
            this.btnDuyet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnDuyet.FlatAppearance.BorderSize = 0;
            this.btnDuyet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDuyet.ForeColor = System.Drawing.Color.White;
            this.btnDuyet.Location = new System.Drawing.Point(20, 615);
            this.btnDuyet.Name = "btnDuyet";
            this.btnDuyet.Size = new System.Drawing.Size(200, 40);
            this.btnDuyet.TabIndex = 10;
            this.btnDuyet.Text = "✅ Duyệt trả hàng";
            this.btnDuyet.UseVisualStyleBackColor = false;
            this.btnDuyet.Click += new System.EventHandler(this.btnDuyet_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(20, 525);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(500, 70);
            this.txtGhiChu.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label5.Location = new System.Drawing.Point(20, 500);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ghi chú nội bộ:";
            // 
            // cboHoanTien
            // 
            this.cboHoanTien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHoanTien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHoanTien.FormattingEnabled = true;
            this.cboHoanTien.Location = new System.Drawing.Point(20, 455);
            this.cboHoanTien.Name = "cboHoanTien";
            this.cboHoanTien.Size = new System.Drawing.Size(300, 31);
            this.cboHoanTien.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(20, 430);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Hình thức hoàn tiền:";
            // 
            // dgvSanPhamTra
            // 
            this.dgvSanPhamTra.AllowUserToAddRows = false;
            this.dgvSanPhamTra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSanPhamTra.BackgroundColor = System.Drawing.Color.White;
            this.dgvSanPhamTra.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSanPhamTra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSanPhamTra.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSanPhamTra.Location = new System.Drawing.Point(20, 215);
            this.dgvSanPhamTra.Name = "dgvSanPhamTra";
            this.dgvSanPhamTra.RowHeadersVisible = false;
            this.dgvSanPhamTra.RowHeadersWidth = 51;
            this.dgvSanPhamTra.RowTemplate.Height = 35;
            this.dgvSanPhamTra.Size = new System.Drawing.Size(500, 200);
            this.dgvSanPhamTra.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(20, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sản phẩm trả lại:";
            // 
            // cboLyDo
            // 
            this.cboLyDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLyDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLyDo.FormattingEnabled = true;
            this.cboLyDo.Location = new System.Drawing.Point(20, 145);
            this.cboLyDo.Name = "cboLyDo";
            this.cboLyDo.Size = new System.Drawing.Size(400, 31);
            this.cboLyDo.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(20, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Lý do trả hàng:";
            // 
            // txtMaDon
            // 
            this.txtMaDon.BackColor = System.Drawing.Color.White;
            this.txtMaDon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaDon.Location = new System.Drawing.Point(20, 75);
            this.txtMaDon.Name = "txtMaDon";
            this.txtMaDon.Size = new System.Drawing.Size(400, 30);
            this.txtMaDon.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(20, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã đơn hàng (Nhập hoặc chọn bên):";
            // 
            // cardPending
            // 
            this.cardPending.BackColor = System.Drawing.Color.White;
            this.cardPending.Controls.Add(this.dgvChoTra);
            this.cardPending.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardPending.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cardPending.Location = new System.Drawing.Point(553, 3);
            this.cardPending.Name = "cardPending";
            this.cardPending.Padding = new System.Windows.Forms.Padding(10);
            this.cardPending.Size = new System.Drawing.Size(504, 674);
            this.cardPending.TabIndex = 1;
            this.cardPending.TabStop = false;
            this.cardPending.Text = "🚚 Đơn Shipper vừa mang về (Chờ xử lý)";
            // 
            // dgvChoTra
            // 
            this.dgvChoTra.AllowUserToAddRows = false;
            this.dgvChoTra.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChoTra.BackgroundColor = System.Drawing.Color.White;
            this.dgvChoTra.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChoTra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChoTra.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvChoTra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChoTra.Location = new System.Drawing.Point(10, 33);
            this.dgvChoTra.Name = "dgvChoTra";
            this.dgvChoTra.ReadOnly = true;
            this.dgvChoTra.RowHeadersVisible = false;
            this.dgvChoTra.RowHeadersWidth = 51;
            this.dgvChoTra.RowTemplate.Height = 40;
            this.dgvChoTra.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvChoTra.Size = new System.Drawing.Size(484, 631);
            this.dgvChoTra.TabIndex = 0;
            // 
            // ucTraHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucTraHang";
            this.Size = new System.Drawing.Size(1100, 800);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.tlpMain.ResumeLayout(false);
            this.cardMain.ResumeLayout(false);
            this.cardMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSanPhamTra)).EndInit();
            this.cardPending.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChoTra)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.GroupBox cardMain;
        private System.Windows.Forms.TextBox txtMaDon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboLyDo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvSanPhamTra;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboHoanTien;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDuyet;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox cardPending;
        private System.Windows.Forms.DataGridView dgvChoTra;
    }
}
