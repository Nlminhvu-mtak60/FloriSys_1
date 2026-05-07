namespace FloriSys._3_BanHang
{
    partial class ucPhanHoi
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpNew = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaDon = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpHistory = new System.Windows.Forms.GroupBox();
            this.dgvPhanHoi = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grpNew.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanHoi)).BeginInit();
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
            this.lblSubTitle.Size = new System.Drawing.Size(156, 20);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Đơn hàng DH2026044";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(249, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Ghi nhận phản hồi";
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.81818F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.18182F));
            this.pnlMain.Controls.Add(this.pnlLeft, 0, 0);
            this.pnlMain.Controls.Add(this.pnlRight, 1, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 80);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Size = new System.Drawing.Size(1100, 720);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.grpNew);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(10, 10);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(429, 700);
            this.pnlLeft.TabIndex = 0;
            // 
            // grpNew
            // 
            this.grpNew.Controls.Add(this.btnSave);
            this.grpNew.Controls.Add(this.txtNoiDung);
            this.grpNew.Controls.Add(this.label2);
            this.grpNew.Controls.Add(this.txtMaDon);
            this.grpNew.Controls.Add(this.label1);
            this.grpNew.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpNew.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpNew.Location = new System.Drawing.Point(0, 0);
            this.grpNew.Name = "grpNew";
            this.grpNew.Size = new System.Drawing.Size(429, 450);
            this.grpNew.TabIndex = 0;
            this.grpNew.TabStop = false;
            this.grpNew.Text = "📝 Ghi nhận phản hồi mới";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(435, 40);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Ghi nhận phản hồi";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Location = new System.Drawing.Point(20, 130);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(435, 230);
            this.txtNoiDung.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label2.Location = new System.Drawing.Point(20, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nội dung phản hồi:";
            // 
            // txtMaDon
            // 
            this.txtMaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.txtMaDon.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaDon.Location = new System.Drawing.Point(20, 60);
            this.txtMaDon.Name = "txtMaDon";
            this.txtMaDon.ReadOnly = true;
            this.txtMaDon.Size = new System.Drawing.Size(435, 30);
            this.txtMaDon.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đơn hàng liên quan:";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpHistory);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(459, 10);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(10);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(631, 700);
            this.pnlRight.TabIndex = 1;
            // 
            // grpHistory
            // 
            this.grpHistory.Controls.Add(this.dgvPhanHoi);
            this.grpHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHistory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpHistory.Location = new System.Drawing.Point(0, 0);
            this.grpHistory.Name = "grpHistory";
            this.grpHistory.Size = new System.Drawing.Size(631, 700);
            this.grpHistory.TabIndex = 0;
            this.grpHistory.TabStop = false;
            this.grpHistory.Text = "📋 Lịch sử phản hồi";
            // 
            // dgvPhanHoi
            // 
            this.dgvPhanHoi.AllowUserToAddRows = false;
            this.dgvPhanHoi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPhanHoi.BackgroundColor = System.Drawing.Color.White;
            this.dgvPhanHoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPhanHoi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPhanHoi.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPhanHoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPhanHoi.Location = new System.Drawing.Point(3, 26);
            this.dgvPhanHoi.Name = "dgvPhanHoi";
            this.dgvPhanHoi.RowHeadersVisible = false;
            this.dgvPhanHoi.RowHeadersWidth = 51;
            this.dgvPhanHoi.RowTemplate.Height = 35;
            this.dgvPhanHoi.Size = new System.Drawing.Size(625, 671);
            this.dgvPhanHoi.TabIndex = 0;
            // 
            // ucPhanHoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucPhanHoi";
            this.Size = new System.Drawing.Size(1100, 800);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.grpNew.ResumeLayout(false);
            this.grpNew.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.grpHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhanHoi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaDon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpHistory;
        private System.Windows.Forms.DataGridView dgvPhanHoi;
    }
}
