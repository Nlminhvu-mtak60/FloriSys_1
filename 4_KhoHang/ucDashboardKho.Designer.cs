namespace FloriSys._4_KhoHang
{
    partial class ucDashboardKho
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlKPI = new System.Windows.Forms.TableLayoutPanel();
            this.pnlDonChoXuat = new System.Windows.Forms.Panel();
            this.lblValChoXuat = new System.Windows.Forms.Label();
            this.lblTitleChoXuat = new System.Windows.Forms.Label();
            this.pnlSapHet = new System.Windows.Forms.Panel();
            this.lblValSapHet = new System.Windows.Forms.Label();
            this.lblTitleSapHet = new System.Windows.Forms.Label();
            this.pnlDaXuat = new System.Windows.Forms.Panel();
            this.lblValDaXuat = new System.Windows.Forms.Label();
            this.lblTitleDaXuat = new System.Windows.Forms.Label();
            this.pnlPhieuNhap = new System.Windows.Forms.Panel();
            this.lblValPhieuNhap = new System.Windows.Forms.Label();
            this.lblTitlePhieuNhap = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.dgvDonChoXuat = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.lblCanhBaoTitle = new System.Windows.Forms.Label();
            this.flpCanhBao = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader.SuspendLayout();
            this.pnlKPI.SuspendLayout();
            this.pnlDonChoXuat.SuspendLayout();
            this.pnlSapHet.SuspendLayout();
            this.pnlDaXuat.SuspendLayout();
            this.pnlPhieuNhap.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonChoXuat)).BeginInit();
            this.pnlRight.SuspendLayout();
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
            this.pnlHeader.Size = new System.Drawing.Size(1000, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTitle.Location = new System.Drawing.Point(20, 45);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(206, 20);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Chào mừng trở lại, Minh Khoa";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(296, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard Nhân viên ";
            // 
            // pnlKPI
            // 
            this.pnlKPI.ColumnCount = 4;
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.Controls.Add(this.pnlDonChoXuat, 0, 0);
            this.pnlKPI.Controls.Add(this.pnlSapHet, 1, 0);
            this.pnlKPI.Controls.Add(this.pnlDaXuat, 2, 0);
            this.pnlKPI.Controls.Add(this.pnlPhieuNhap, 3, 0);
            this.pnlKPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKPI.Location = new System.Drawing.Point(0, 80);
            this.pnlKPI.Name = "pnlKPI";
            this.pnlKPI.Padding = new System.Windows.Forms.Padding(10);
            this.pnlKPI.RowCount = 1;
            this.pnlKPI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKPI.Size = new System.Drawing.Size(1000, 120);
            this.pnlKPI.TabIndex = 1;
            // 
            // pnlDonChoXuat
            // 
            this.pnlDonChoXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlDonChoXuat.Controls.Add(this.lblValChoXuat);
            this.pnlDonChoXuat.Controls.Add(this.lblTitleChoXuat);
            this.pnlDonChoXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDonChoXuat.Location = new System.Drawing.Point(15, 15);
            this.pnlDonChoXuat.Margin = new System.Windows.Forms.Padding(5);
            this.pnlDonChoXuat.Name = "pnlDonChoXuat";
            this.pnlDonChoXuat.Size = new System.Drawing.Size(235, 90);
            this.pnlDonChoXuat.TabIndex = 0;
            // 
            // lblValChoXuat
            // 
            this.lblValChoXuat.AutoSize = true;
            this.lblValChoXuat.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValChoXuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblValChoXuat.Location = new System.Drawing.Point(10, 35);
            this.lblValChoXuat.Name = "lblValChoXuat";
            this.lblValChoXuat.Size = new System.Drawing.Size(40, 46);
            this.lblValChoXuat.TabIndex = 1;
            this.lblValChoXuat.Text = "7";
            // 
            // lblTitleChoXuat
            // 
            this.lblTitleChoXuat.AutoSize = true;
            this.lblTitleChoXuat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleChoXuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblTitleChoXuat.Location = new System.Drawing.Point(10, 10);
            this.lblTitleChoXuat.Name = "lblTitleChoXuat";
            this.lblTitleChoXuat.Size = new System.Drawing.Size(130, 20);
            this.lblTitleChoXuat.TabIndex = 0;
            this.lblTitleChoXuat.Text = "Đơn chờ xuất kho";
            // 
            // pnlSapHet
            // 
            this.pnlSapHet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.pnlSapHet.Controls.Add(this.lblValSapHet);
            this.pnlSapHet.Controls.Add(this.lblTitleSapHet);
            this.pnlSapHet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSapHet.Location = new System.Drawing.Point(260, 15);
            this.pnlSapHet.Margin = new System.Windows.Forms.Padding(5);
            this.pnlSapHet.Name = "pnlSapHet";
            this.pnlSapHet.Size = new System.Drawing.Size(235, 90);
            this.pnlSapHet.TabIndex = 1;
            // 
            // lblValSapHet
            // 
            this.lblValSapHet.AutoSize = true;
            this.lblValSapHet.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValSapHet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblValSapHet.Location = new System.Drawing.Point(10, 35);
            this.lblValSapHet.Name = "lblValSapHet";
            this.lblValSapHet.Size = new System.Drawing.Size(40, 46);
            this.lblValSapHet.TabIndex = 1;
            this.lblValSapHet.Text = "3";
            // 
            // lblTitleSapHet
            // 
            this.lblTitleSapHet.AutoSize = true;
            this.lblTitleSapHet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleSapHet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblTitleSapHet.Location = new System.Drawing.Point(10, 10);
            this.lblTitleSapHet.Name = "lblTitleSapHet";
            this.lblTitleSapHet.Size = new System.Drawing.Size(126, 20);
            this.lblTitleSapHet.TabIndex = 0;
            this.lblTitleSapHet.Text = "Sản phẩm sắp hết";
            // 
            // pnlDaXuat
            // 
            this.pnlDaXuat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlDaXuat.Controls.Add(this.lblValDaXuat);
            this.pnlDaXuat.Controls.Add(this.lblTitleDaXuat);
            this.pnlDaXuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDaXuat.Location = new System.Drawing.Point(505, 15);
            this.pnlDaXuat.Margin = new System.Windows.Forms.Padding(5);
            this.pnlDaXuat.Name = "pnlDaXuat";
            this.pnlDaXuat.Size = new System.Drawing.Size(235, 90);
            this.pnlDaXuat.TabIndex = 2;
            // 
            // lblValDaXuat
            // 
            this.lblValDaXuat.AutoSize = true;
            this.lblValDaXuat.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValDaXuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblValDaXuat.Location = new System.Drawing.Point(10, 35);
            this.lblValDaXuat.Name = "lblValDaXuat";
            this.lblValDaXuat.Size = new System.Drawing.Size(60, 46);
            this.lblValDaXuat.TabIndex = 1;
            this.lblValDaXuat.Text = "14";
            // 
            // lblTitleDaXuat
            // 
            this.lblTitleDaXuat.AutoSize = true;
            this.lblTitleDaXuat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleDaXuat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblTitleDaXuat.Location = new System.Drawing.Point(10, 10);
            this.lblTitleDaXuat.Name = "lblTitleDaXuat";
            this.lblTitleDaXuat.Size = new System.Drawing.Size(155, 20);
            this.lblTitleDaXuat.TabIndex = 0;
            this.lblTitleDaXuat.Text = "Đang chờ giao";
            // 
            // pnlPhieuNhap
            // 
            this.pnlPhieuNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlPhieuNhap.Controls.Add(this.lblValPhieuNhap);
            this.pnlPhieuNhap.Controls.Add(this.lblTitlePhieuNhap);
            this.pnlPhieuNhap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPhieuNhap.Location = new System.Drawing.Point(750, 15);
            this.pnlPhieuNhap.Margin = new System.Windows.Forms.Padding(5);
            this.pnlPhieuNhap.Name = "pnlPhieuNhap";
            this.pnlPhieuNhap.Size = new System.Drawing.Size(235, 90);
            this.pnlPhieuNhap.TabIndex = 3;
            // 
            // lblValPhieuNhap
            // 
            this.lblValPhieuNhap.AutoSize = true;
            this.lblValPhieuNhap.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValPhieuNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblValPhieuNhap.Location = new System.Drawing.Point(10, 35);
            this.lblValPhieuNhap.Name = "lblValPhieuNhap";
            this.lblValPhieuNhap.Size = new System.Drawing.Size(60, 46);
            this.lblValPhieuNhap.TabIndex = 1;
            this.lblValPhieuNhap.Text = "18";
            // 
            // lblTitlePhieuNhap
            // 
            this.lblTitlePhieuNhap.AutoSize = true;
            this.lblTitlePhieuNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitlePhieuNhap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTitlePhieuNhap.Location = new System.Drawing.Point(10, 10);
            this.lblTitlePhieuNhap.Name = "lblTitlePhieuNhap";
            this.lblTitlePhieuNhap.Size = new System.Drawing.Size(157, 20);
            this.lblTitlePhieuNhap.TabIndex = 0;
            this.lblTitlePhieuNhap.Text = "Phiếu nhập tháng này";
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnlMain.Controls.Add(this.pnlLeft, 0, 0);
            this.pnlMain.Controls.Add(this.pnlRight, 1, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 200);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Size = new System.Drawing.Size(1000, 520);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.White;
            this.pnlLeft.Controls.Add(this.lblTableTitle);
            this.pnlLeft.Controls.Add(this.dgvDonChoXuat);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(15, 15);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Size = new System.Drawing.Size(582, 490);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.Location = new System.Drawing.Point(13, 10);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(207, 23);
            this.lblTableTitle.TabIndex = 1;
            this.lblTableTitle.Text = "📋 Đơn hàng chờ xuất kho";
            // 
            // dgvDonChoXuat
            // 
            this.dgvDonChoXuat.AllowUserToAddRows = false;
            this.dgvDonChoXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDonChoXuat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonChoXuat.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonChoXuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonChoXuat.Location = new System.Drawing.Point(10, 40);
            this.dgvDonChoXuat.Name = "dgvDonChoXuat";
            this.dgvDonChoXuat.RowHeadersVisible = false;
            this.dgvDonChoXuat.RowHeadersWidth = 51;
            this.dgvDonChoXuat.Size = new System.Drawing.Size(562, 440);
            this.dgvDonChoXuat.TabIndex = 2;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.lblCanhBaoTitle);
            this.pnlRight.Controls.Add(this.flpCanhBao);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(603, 15);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10, 30, 10, 10);
            this.pnlRight.Size = new System.Drawing.Size(384, 490);
            this.pnlRight.TabIndex = 1;
            // 
            // lblCanhBaoTitle
            // 
            this.lblCanhBaoTitle.AutoSize = true;
            this.lblCanhBaoTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCanhBaoTitle.Location = new System.Drawing.Point(13, 10);
            this.lblCanhBaoTitle.Name = "lblCanhBaoTitle";
            this.lblCanhBaoTitle.Size = new System.Drawing.Size(206, 23);
            this.lblCanhBaoTitle.TabIndex = 1;
            this.lblCanhBaoTitle.Text = "🚨 Cảnh báo tồn kho thấp";
            // 
            // flpCanhBao
            // 
            this.flpCanhBao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpCanhBao.Location = new System.Drawing.Point(10, 30);
            this.flpCanhBao.Name = "flpCanhBao";
            this.flpCanhBao.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.flpCanhBao.Size = new System.Drawing.Size(364, 450);
            this.flpCanhBao.TabIndex = 2;
            // 
            // ucDashboardKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlKPI);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucDashboardKho";
            this.Size = new System.Drawing.Size(1000, 720);
            this.Load += new System.EventHandler(this.ucDashboardKho_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlKPI.ResumeLayout(false);
            this.pnlDonChoXuat.ResumeLayout(false);
            this.pnlDonChoXuat.PerformLayout();
            this.pnlSapHet.ResumeLayout(false);
            this.pnlSapHet.PerformLayout();
            this.pnlDaXuat.ResumeLayout(false);
            this.pnlDaXuat.PerformLayout();
            this.pnlPhieuNhap.ResumeLayout(false);
            this.pnlPhieuNhap.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonChoXuat)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel pnlKPI;
        private System.Windows.Forms.Panel pnlDonChoXuat;
        private System.Windows.Forms.Label lblValChoXuat;
        private System.Windows.Forms.Label lblTitleChoXuat;
        private System.Windows.Forms.Panel pnlSapHet;
        private System.Windows.Forms.Label lblValSapHet;
        private System.Windows.Forms.Label lblTitleSapHet;
        private System.Windows.Forms.Panel pnlDaXuat;
        private System.Windows.Forms.Label lblValDaXuat;
        private System.Windows.Forms.Label lblTitleDaXuat;
        private System.Windows.Forms.Panel pnlPhieuNhap;
        private System.Windows.Forms.Label lblValPhieuNhap;
        private System.Windows.Forms.Label lblTitlePhieuNhap;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvDonChoXuat;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Label lblCanhBaoTitle;
        private System.Windows.Forms.FlowLayoutPanel flpCanhBao;
    }
}
