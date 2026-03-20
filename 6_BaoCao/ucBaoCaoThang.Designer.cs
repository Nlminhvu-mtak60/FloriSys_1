namespace FloriSys._6_BaoCao
{
    partial class ucBaoCaoThang
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
            this.lblMonth = new System.Windows.Forms.Label();
            this.pnlStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStat1 = new System.Windows.Forms.Panel();
            this.lblDoanhThuValue = new System.Windows.Forms.Label();
            this.lblDoanhThuTitle = new System.Windows.Forms.Label();
            this.pnlStat2 = new System.Windows.Forms.Panel();
            this.lblCompareValue = new System.Windows.Forms.Label();
            this.lblCompareTitle = new System.Windows.Forms.Label();
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
            this.lblTitle.Size = new System.Drawing.Size(270, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Báo cáo tháng";
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblMonth.Location = new System.Drawing.Point(33, 65);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(121, 23);
            this.lblMonth.TabIndex = 1;
            this.lblMonth.Text = "Tháng 03/2026";
            // 
            // pnlStats
            // 
            this.pnlStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStats.Controls.Add(this.pnlStat1);
            this.pnlStats.Controls.Add(this.pnlStat2);
            this.pnlStats.Location = new System.Drawing.Point(30, 100);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Size = new System.Drawing.Size(940, 120);
            this.pnlStats.TabIndex = 2;
            // 
            // pnlStat1
            // 
            this.pnlStat1.BackColor = System.Drawing.Color.White;
            this.pnlStat1.Controls.Add(this.lblDoanhThuValue);
            this.pnlStat1.Controls.Add(this.lblDoanhThuTitle);
            this.pnlStat1.Location = new System.Drawing.Point(3, 3);
            this.pnlStat1.Name = "pnlStat1";
            this.pnlStat1.Size = new System.Drawing.Size(350, 100);
            this.pnlStat1.TabIndex = 0;
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
            this.lblDoanhThuTitle.Size = new System.Drawing.Size(155, 20);
            this.lblDoanhThuTitle.TabIndex = 0;
            this.lblDoanhThuTitle.Text = "DOANH THU THÁNG";
            // 
            // pnlStat2
            // 
            this.pnlStat2.BackColor = System.Drawing.Color.White;
            this.pnlStat2.Controls.Add(this.lblCompareValue);
            this.pnlStat2.Controls.Add(this.lblCompareTitle);
            this.pnlStat2.Location = new System.Drawing.Point(359, 3);
            this.pnlStat2.Name = "pnlStat2";
            this.pnlStat2.Size = new System.Drawing.Size(250, 100);
            this.pnlStat2.TabIndex = 1;
            // 
            // lblCompareValue
            // 
            this.lblCompareValue.AutoSize = true;
            this.lblCompareValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompareValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(106)))), ((int)(((byte)(79)))));
            this.lblCompareValue.Location = new System.Drawing.Point(15, 50);
            this.lblCompareValue.Name = "lblCompareValue";
            this.lblCompareValue.Size = new System.Drawing.Size(51, 28);
            this.lblCompareValue.TabIndex = 1;
            this.lblCompareValue.Text = "+0%";
            // 
            // lblCompareTitle
            // 
            this.lblCompareTitle.AutoSize = true;
            this.lblCompareTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompareTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblCompareTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCompareTitle.Name = "lblCompareTitle";
            this.lblCompareTitle.Size = new System.Drawing.Size(164, 20);
            this.lblCompareTitle.TabIndex = 0;
            this.lblCompareTitle.Text = "SO VỚI THÁNG TRƯỚC";
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.ColumnCount = 1;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Controls.Add(this.pnlChartCard, 0, 0);
            this.pnlMain.Controls.Add(this.pnlTableCard, 0, 1);
            this.pnlMain.Location = new System.Drawing.Point(30, 230);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 2;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.pnlMain.Size = new System.Drawing.Size(940, 560);
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
            this.pnlChartCard.Size = new System.Drawing.Size(934, 274);
            this.pnlChartCard.TabIndex = 0;
            // 
            // lblChartTitle
            // 
            this.lblChartTitle.AutoSize = true;
            this.lblChartTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblChartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblChartTitle.Location = new System.Drawing.Point(20, 20);
            this.lblChartTitle.Name = "lblChartTitle";
            this.lblChartTitle.Size = new System.Drawing.Size(282, 28);
            this.lblChartTitle.TabIndex = 0;
            this.lblChartTitle.Text = "📊 Doanh thu theo ngày (đ)";
            // 
            // pnlChartMock
            // 
            this.pnlChartMock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlChartMock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlChartMock.Location = new System.Drawing.Point(20, 60);
            this.pnlChartMock.Name = "pnlChartMock";
            this.pnlChartMock.Size = new System.Drawing.Size(894, 194);
            this.pnlChartMock.TabIndex = 1;
            // 
            // pnlTableCard
            // 
            this.pnlTableCard.BackColor = System.Drawing.Color.White;
            this.pnlTableCard.Controls.Add(this.dgvTopSP);
            this.pnlTableCard.Controls.Add(this.lblTableTitle);
            this.pnlTableCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTableCard.Location = new System.Drawing.Point(3, 283);
            this.pnlTableCard.Name = "pnlTableCard";
            this.pnlTableCard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlTableCard.Size = new System.Drawing.Size(934, 274);
            this.pnlTableCard.TabIndex = 1;
            // 
            // dgvTopSP
            // 
            this.dgvTopSP.AllowUserToAddRows = false;
            this.dgvTopSP.AllowUserToDeleteRows = false;
            this.dgvTopSP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopSP.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTopSP.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopSP.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTopSP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopSP.Location = new System.Drawing.Point(20, 60);
            this.dgvTopSP.Name = "dgvTopSP";
            this.dgvTopSP.ReadOnly = true;
            this.dgvTopSP.RowHeadersVisible = false;
            this.dgvTopSP.RowTemplate.Height = 35;
            this.dgvTopSP.Size = new System.Drawing.Size(894, 194);
            this.dgvTopSP.TabIndex = 1;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTableTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(273, 28);
            this.lblTableTitle.TabIndex = 0;
            this.lblTableTitle.Text = "🔥 Top sản phẩm trong tháng";
            // 
            // ucBaoCaoThang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucBaoCaoThang";
            this.Size = new System.Drawing.Size(1000, 820);
            this.Load += new System.EventHandler(this.ucBaoCaoThang_Load);
            this.pnlStats.ResumeLayout(false);
            this.pnlStat1.ResumeLayout(false);
            this.pnlStat1.PerformLayout();
            this.pnlStat2.ResumeLayout(false);
            this.pnlStat2.PerformLayout();
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
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.FlowLayoutPanel pnlStats;
        private System.Windows.Forms.Panel pnlStat1;
        private System.Windows.Forms.Label lblDoanhThuValue;
        private System.Windows.Forms.Label lblDoanhThuTitle;
        private System.Windows.Forms.Panel pnlStat2;
        private System.Windows.Forms.Label lblCompareValue;
        private System.Windows.Forms.Label lblCompareTitle;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlChartCard;
        private System.Windows.Forms.Label lblChartTitle;
        private System.Windows.Forms.Panel pnlChartMock;
        private System.Windows.Forms.Panel pnlTableCard;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvTopSP;
    }
}
