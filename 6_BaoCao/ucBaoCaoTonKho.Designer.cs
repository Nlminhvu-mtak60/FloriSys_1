namespace FloriSys._6_BaoCao
{
    partial class ucBaoCaoTonKho
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
            this.pnlStatCard = new System.Windows.Forms.Panel();
            this.lblTongSPSapHet = new System.Windows.Forms.Label();
            this.lblStatTitle = new System.Windows.Forms.Label();
            this.pnlGridCard = new System.Windows.Forms.Panel();
            this.dgvTonKho = new System.Windows.Forms.DataGridView();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.pnlStatCard.SuspendLayout();
            this.pnlGridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Georgia", 20.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 39);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Báo cáo tồn kho";
            // 
            // pnlStatCard
            // 
            this.pnlStatCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.pnlStatCard.Controls.Add(this.lblTongSPSapHet);
            this.pnlStatCard.Controls.Add(this.lblStatTitle);
            this.pnlStatCard.Location = new System.Drawing.Point(30, 80);
            this.pnlStatCard.Name = "pnlStatCard";
            this.pnlStatCard.Size = new System.Drawing.Size(300, 100);
            this.pnlStatCard.TabIndex = 1;
            // 
            // lblTongSPSapHet
            // 
            this.lblTongSPSapHet.AutoSize = true;
            this.lblTongSPSapHet.Font = new System.Drawing.Font("Georgia", 24F, System.Drawing.FontStyle.Bold);
            this.lblTongSPSapHet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblTongSPSapHet.Location = new System.Drawing.Point(15, 40);
            this.lblTongSPSapHet.Name = "lblTongSPSapHet";
            this.lblTongSPSapHet.Size = new System.Drawing.Size(47, 46);
            this.lblTongSPSapHet.TabIndex = 1;
            this.lblTongSPSapHet.Text = "0";
            // 
            // lblStatTitle
            // 
            this.lblStatTitle.AutoSize = true;
            this.lblStatTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblStatTitle.Location = new System.Drawing.Point(15, 15);
            this.lblStatTitle.Name = "lblStatTitle";
            this.lblStatTitle.Size = new System.Drawing.Size(155, 20);
            this.lblStatTitle.TabIndex = 0;
            this.lblStatTitle.Text = "SẢN PHẨM SẮP HẾT";
            // 
            // pnlGridCard
            // 
            this.pnlGridCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGridCard.BackColor = System.Drawing.Color.White;
            this.pnlGridCard.Controls.Add(this.dgvTonKho);
            this.pnlGridCard.Controls.Add(this.lblGridTitle);
            this.pnlGridCard.Location = new System.Drawing.Point(30, 195);
            this.pnlGridCard.Name = "pnlGridCard";
            this.pnlGridCard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlGridCard.Size = new System.Drawing.Size(940, 495);
            this.pnlGridCard.TabIndex = 2;
            // 
            // dgvTonKho
            // 
            this.dgvTonKho.AllowUserToAddRows = false;
            this.dgvTonKho.AllowUserToDeleteRows = false;
            this.dgvTonKho.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTonKho.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTonKho.BackgroundColor = System.Drawing.Color.White;
            this.dgvTonKho.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTonKho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTonKho.Location = new System.Drawing.Point(20, 60);
            this.dgvTonKho.Name = "dgvTonKho";
            this.dgvTonKho.ReadOnly = true;
            this.dgvTonKho.RowHeadersVisible = false;
            this.dgvTonKho.RowTemplate.Height = 35;
            this.dgvTonKho.Size = new System.Drawing.Size(900, 415);
            this.dgvTonKho.TabIndex = 1;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblGridTitle.Location = new System.Drawing.Point(20, 20);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(306, 28);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "📦 Danh sách hàng cần chú ý";
            // 
            // ucBaoCaoTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlGridCard);
            this.Controls.Add(this.pnlStatCard);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucBaoCaoTonKho";
            this.Size = new System.Drawing.Size(1000, 720);
            this.Load += new System.EventHandler(this.ucBaoCaoTonKho_Load);
            this.pnlStatCard.ResumeLayout(false);
            this.pnlStatCard.PerformLayout();
            this.pnlGridCard.ResumeLayout(false);
            this.pnlGridCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTonKho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlStatCard;
        private System.Windows.Forms.Label lblTongSPSapHet;
        private System.Windows.Forms.Label lblStatTitle;
        private System.Windows.Forms.Panel pnlGridCard;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.DataGridView dgvTonKho;
    }
}
