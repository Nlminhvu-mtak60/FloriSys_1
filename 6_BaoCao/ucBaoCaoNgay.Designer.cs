namespace FloriSys._6_BaoCao
{
    partial class ucBaoCaoNgay
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStat1 = new System.Windows.Forms.Panel();
            this.lblTongDonValue = new System.Windows.Forms.Label();
            this.lblTongDonTitle = new System.Windows.Forms.Label();
            this.pnlStat2 = new System.Windows.Forms.Panel();
            this.lblDoanhThuValue = new System.Windows.Forms.Label();
            this.lblDoanhThuTitle = new System.Windows.Forms.Label();
            this.pnlStat3 = new System.Windows.Forms.Panel();
            this.lblSoLuongSPValue = new System.Windows.Forms.Label();
            this.lblSoLuongSPTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlChartCard = new System.Windows.Forms.Panel();
            this.lblChartTitle = new System.Windows.Forms.Label();
            this.pnlChartMock = new System.Windows.Forms.Panel();
            this.pnlTableCard = new System.Windows.Forms.Panel();
            this.dgvTopSP = new System.Windows.Forms.DataGridView();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.pnlStats.SuspendLayout();
            this.pnlStat1.SuspendLayout();
            this.pnlStat2.SuspendLayout();
            this.pnlStat3.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlChartCard.SuspendLayout();
            this.pnlTableCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSP)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 20.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(350, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Báo cáo doanh thu ngày";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblDate.Location = new System.Drawing.Point(33, 65);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(155, 23);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Thứ Năm, 26/03/2026";
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.Controls.Add(this.pnlStat1);
            this.pnlStats.Controls.Add(this.pnlStat2);
            this.pnlStats.Controls.Add(this.pnlStat3);
            this.pnlStats.Location = new System.Drawing.Point(30, 100);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(940, 120);
            this.pnlStats.TabIndex = 2;
            // 
            // pnlStat1
            // 
            this.pnlStat1.BackColor = System.Drawing.Color.White;
            this.pnlStat1.Controls.Add(this.lblTongDonValue);
            this.pnlStat1.Controls.Add(this.lblTongDonTitle);
            this.pnlStat1.Location = new System.Drawing.Point(3, 3);
            this.pnlStat1.Name = "pnlStat1";
            this.pnlStat1.Size = new System.Drawing.Size(220, 100);
            this.pnlStat1.TabIndex = 0;
            // 
            // lblTongDonValue
            // 
            this.lblTongDonValue.AutoSize = true;
            this.lblTongDonValue.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Bold);
            this.lblTongDonValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTongDonValue.Location = new System.Drawing.Point(15, 40);
            this.lblTongDonValue.Name = "lblTongDonValue";
            this.lblTongDonValue.Size = new System.Drawing.Size(47, 46);
            this.lblTongDonValue.TabIndex = 1;
            this.lblTongDonValue.Text = "0";
            // 
            // lblTongDonTitle
            // 
            this.lblTongDonTitle.AutoSize = true;
            this.lblTongDonTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongDonTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblTongDonTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTongDonTitle.Name = "lblTongDonTitle";
            this.lblTongDonTitle.Size = new System.Drawing.Size(126, 20);
            this.lblTongDonTitle.TabIndex = 0;
            this.lblTongDonTitle.Text = "TỔNG ĐƠN HÀNG";
            // 
            // pnlStat2
            // 
            this.pnlStat2.BackColor = System.Drawing.Color.White;
            this.pnlStat2.Controls.Add(this.lblDoanhThuValue);
            this.pnlStat2.Controls.Add(this.lblDoanhThuTitle);
            this.pnlStat2.Location = new System.Drawing.Point(229, 3);
            this.pnlStat2.Name = "pnlStat2";
            this.pnlStat2.Size = new System.Drawing.Size(300, 100);
            this.pnlStat2.TabIndex = 1;
            // 
            // lblDoanhThuValue
            // 
            this.lblDoanhThuValue.AutoSize = true;
            this.lblDoanhThuValue.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThuValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblDoanhThuValue.Location = new System.Drawing.Point(15, 40);
            this.lblDoanhThuValue.Name = "lblDoanhThuValue";
            this.lblDoanhThuValue.Size = new System.Drawing.Size(117, 46);
            this.lblDoanhThuValue.TabIndex = 1;
            this.lblDoanhThuValue.Text = "0đ";
            // 
            // lblDoanhThuTitle
            // 
            this.lblDoanhThuTitle.AutoSize = true;
            this.lblDoanhThuTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDoanhThuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblDoanhThuTitle.Location = new System.Drawing.Point(15, 15);
            this.lblDoanhThuTitle.Name = "lblDoanhThuTitle";
            this.lblDoanhThuTitle.Size = new System.Drawing.Size(100, 20);
            this.lblDoanhThuTitle.TabIndex = 0;
            this.lblDoanhThuTitle.Text = "DOANH THU";
            // 
            // pnlStat3
            // 
            this.pnlStat3.BackColor = System.Drawing.Color.White;
            this.pnlStat3.Controls.Add(this.lblSoLuongSPValue);
            this.pnlStat3.Controls.Add(this.lblSoLuongSPTitle);
            this.pnlStat3.Location = new System.Drawing.Point(535, 3);
            this.pnlStat3.Name = "pnlStat3";
            this.pnlStat3.Size = new System.Drawing.Size(220, 100);
            this.pnlStat3.TabIndex = 2;
            // 
            // lblSoLuongSPValue
            // 
            this.lblSoLuongSPValue.AutoSize = true;
            this.lblSoLuongSPValue.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Bold);
            this.lblSoLuongSPValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblSoLuongSPValue.Location = new System.Drawing.Point(15, 40);
            this.lblSoLuongSPValue.Name = "lblSoLuongSPValue";
            this.lblSoLuongSPValue.Size = new System.Drawing.Size(47, 46);
            this.lblSoLuongSPValue.TabIndex = 1;
            this.lblSoLuongSPValue.Text = "0";
            // 
            // lblSoLuongSPTitle
            // 
            this.lblSoLuongSPTitle.AutoSize = true;
            this.lblSoLuongSPTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoLuongSPTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblSoLuongSPTitle.Location = new System.Drawing.Point(15, 15);
            this.lblSoLuongSPTitle.Name = "lblSoLuongSPTitle";
            this.lblSoLuongSPTitle.Size = new System.Drawing.Size(130, 20);
            this.lblSoLuongSPTitle.TabIndex = 0;
            this.lblSoLuongSPTitle.Text = "SẢN PHẨM BÁN";
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.pnlMain.Controls.Add(this.pnlChartCard, 0, 0);
            this.pnlMain.Controls.Add(this.pnlTableCard, 1, 0);
            this.pnlMain.Location = new System.Drawing.Point(30, 230);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Size = new System.Drawing.Size(940, 460);
            this.pnlMain.TabIndex = 3;
            // 
            // pnlChartCard
            // 
            this.pnlChartCard.BackColor = System.Drawing.Color.White;
            this.pnlChartCard.Controls.Add(this.lblChartTitle);
            this.pnlChartCard.Controls.Add(this.pnlChartMock);
            this.pnlChartCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChartCard.Location = new System.Drawing.Point(3, 3);
            this.pnlChartCard.Name = "pnlChartCard";
            this.pnlChartCard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlChartCard.Size = new System.Drawing.Size(558, 454);
            this.pnlChartCard.TabIndex = 0;
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblChartTitle.Location = new System.Drawing.Point(20, 20);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.Size = new System.Drawing.Size(265, 28);
            this.lblChartTitle.TabIndex = 0;
            this.lblChartTitle.Text = "📊 Doanh thu theo giờ (đ)";
            // 
            // pnlChartMock
            // 
            this.pnlChartMock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlChartMock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlChartMock.Location = new System.Drawing.Point(20, 60);
            this.pnlChartMock.Name = "pnlChartMock";
            this.pnlChartMock.Size = new System.Drawing.Size(518, 374);
            this.pnlChartMock.TabIndex = 1;
            // 
            // pnlTableCard
            // 
            this.pnlTableCard.BackColor = System.Drawing.Color.White;
            this.pnlTableCard.Controls.Add(this.dgvTopSP);
            this.pnlTableCard.Controls.Add(this.lblTableTitle);
            this.pnlTableCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTableCard.Location = new System.Drawing.Point(567, 3);
            this.pnlTableCard.Name = "pnlTableCard";
            this.pnlTableCard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTableCard.Size = new System.Drawing.Size(370, 454);
            this.pnlTableCard.TabIndex = 1;
            // 
            // dgvTopSP
            // 
            this.dgvTopSP.AllowUserToAddRows = false;
            this.dgvTopSP.AllowUserToDeleteRows = false;
            this.dgvTopSP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopSP.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopSP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopSP.Location = new System.Drawing.Point(20, 60);
            this.dgvTopSP.Name = "dgvTopSP";
            this.dgvTopSP.ReadOnly = true;
            this.dgvTopSP.RowHeadersVisible = false;
            this.dgvTopSP.RowTemplate.Height = 35;
            this.dgvTopSP.Size = new System.Drawing.Size(330, 374);
            this.dgvTopSP.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTableTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(262, 28);
            this.lblTableTitle.TabIndex = 0;
            this.lblTableTitle.Text = "🔥 Top sản phẩm hôm nay";
            // 
            // ucBaoCaoNgay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucBaoCaoNgay";
            this.Size = new System.Drawing.Size(1000, 720);
            this.Load += new System.EventHandler(this.ucBaoCaoNgay_Load);
            this.pnlStats.ResumeLayout(false);
            this.pnlStat1.ResumeLayout(false);
            this.pnlStat1.PerformLayout();
            this.pnlStat2.ResumeLayout(false);
            this.pnlStat2.PerformLayout();
            this.pnlStat3.ResumeLayout(false);
            this.pnlStat3.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlChartCard.ResumeLayout(false);
            this.pnlChartCard.PerformLayout();
            this.pnlTableCard.ResumeLayout(false);
            this.pnlTableCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopSP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.FlowLayoutPanel pnlStats;
        private System.Windows.Forms.Panel pnlStat1;
        private System.Windows.Forms.Label lblTongDonValue;
        private System.Windows.Forms.Label lblTongDonTitle;
        private System.Windows.Forms.Panel pnlStat2;
        private System.Windows.Forms.Label lblDoanhThuValue;
        private System.Windows.Forms.Label lblDoanhThuTitle;
        private System.Windows.Forms.Panel pnlStat3;
        private System.Windows.Forms.Label lblSoLuongSPValue;
        private System.Windows.Forms.Label lblSoLuongSPTitle;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlChartCard;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.Panel pnlChartMock;
        private System.Windows.Forms.Panel pnlTableCard;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvTopSP;
    }
}
