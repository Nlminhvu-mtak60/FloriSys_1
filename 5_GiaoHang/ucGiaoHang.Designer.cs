namespace FloriSys._5_GiaoHang
{
    partial class ucGiaoHang
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.btnPhanCong = new System.Windows.Forms.Button();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.pnlS1 = new System.Windows.Forms.Panel();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.lblS1Top = new System.Windows.Forms.Label();
            this.lblS1Val = new System.Windows.Forms.Label();
            this.pnlS2 = new System.Windows.Forms.Panel();
            this.lblS2Top = new System.Windows.Forms.Label();
            this.lblS2Val = new System.Windows.Forms.Label();
            this.pnlS3 = new System.Windows.Forms.Panel();
            this.lblS3Top = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlS4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblS4Top = new System.Windows.Forms.Label();
            this.pnlTable = new System.Windows.Forms.Panel();
            this.dgvGiaoHang = new System.Windows.Forms.DataGridView();
            this.colMaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKhach = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDiaChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colShipper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlS1.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.pnlS2.SuspendLayout();
            this.pnlS3.SuspendLayout();
            this.pnlS4.SuspendLayout();
            this.pnlTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoHang)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlHeader.Controls.Add(this.btnPhanCong);
            this.pnlHeader.Controls.Add(this.lblSub);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1024, 72);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblTitle.Location = new System.Drawing.Point(28, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(291, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Theo dõi giao hàng";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblSub.Location = new System.Drawing.Point(28, 46);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(231, 20);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "Tất cả chuyến giao hàng hôm nay";
            // 
            // btnPhanCong
            // 
            this.btnPhanCong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPhanCong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnPhanCong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPhanCong.FlatAppearance.BorderSize = 0;
            this.btnPhanCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhanCong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btnPhanCong.ForeColor = System.Drawing.Color.White;
            this.btnPhanCong.Location = new System.Drawing.Point(810, 18);
            this.btnPhanCong.Name = "btnPhanCong";
            this.btnPhanCong.Size = new System.Drawing.Size(170, 34);
            this.btnPhanCong.TabIndex = 2;
            this.btnPhanCong.Text = "👤  Phân công Shipper";
            this.btnPhanCong.UseVisualStyleBackColor = false;
            // 
            // pnlStats
            // 
            this.pnlStats.BackColor = System.Drawing.Color.Transparent;
            this.pnlStats.Controls.Add(this.tlpStats);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Location = new System.Drawing.Point(0, 72);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(28, 8, 28, 0);
            this.pnlStats.Size = new System.Drawing.Size(1024, 100);
            this.pnlStats.TabIndex = 1;
            // 
            // pnlS1
            // 
            this.pnlS1.BackColor = System.Drawing.Color.White;
            this.pnlS1.Controls.Add(this.lblS1Val);
            this.pnlS1.Controls.Add(this.lblS1Top);
            this.pnlS1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlS1.Location = new System.Drawing.Point(0, 0);
            this.pnlS1.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.pnlS1.Name = "pnlS1";
            this.pnlS1.Size = new System.Drawing.Size(236, 92);
            this.pnlS1.TabIndex = 0;
            // 
            // tlpStats
            // 
            this.tlpStats.ColumnCount = 4;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.Controls.Add(this.pnlS4, 3, 0);
            this.tlpStats.Controls.Add(this.pnlS1, 0, 0);
            this.tlpStats.Controls.Add(this.pnlS2, 1, 0);
            this.tlpStats.Controls.Add(this.pnlS3, 2, 0);
            this.tlpStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpStats.Location = new System.Drawing.Point(28, 8);
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.RowCount = 1;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStats.Size = new System.Drawing.Size(968, 92);
            this.tlpStats.TabIndex = 0;
            // 
            // lblS1Top
            // 
            this.lblS1Top.AutoSize = true;
            this.lblS1Top.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblS1Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblS1Top.Location = new System.Drawing.Point(18, 13);
            this.lblS1Top.Name = "lblS1Top";
            this.lblS1Top.Size = new System.Drawing.Size(114, 17);
            this.lblS1Top.TabIndex = 0;
            this.lblS1Top.Text = "CHỜ PHÂN CÔNG";
            // 
            // lblS1Val
            // 
            this.lblS1Val.AutoSize = true;
            this.lblS1Val.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblS1Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblS1Val.Location = new System.Drawing.Point(18, 30);
            this.lblS1Val.Name = "lblS1Val";
            this.lblS1Val.Size = new System.Drawing.Size(40, 46);
            this.lblS1Val.TabIndex = 1;
            this.lblS1Val.Text = "5";
            // 
            // pnlS2
            // 
            this.pnlS2.BackColor = System.Drawing.Color.White;
            this.pnlS2.Controls.Add(this.lblS2Val);
            this.pnlS2.Controls.Add(this.lblS2Top);
            this.pnlS2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlS2.Location = new System.Drawing.Point(245, 0);
            this.pnlS2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlS2.Name = "pnlS2";
            this.pnlS2.Size = new System.Drawing.Size(236, 92);
            this.pnlS2.TabIndex = 1;
            // 
            // lblS2Top
            // 
            this.lblS2Top.AutoSize = true;
            this.lblS2Top.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblS2Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblS2Top.Location = new System.Drawing.Point(18, 13);
            this.lblS2Top.Name = "lblS2Top";
            this.lblS2Top.Size = new System.Drawing.Size(78, 17);
            this.lblS2Top.TabIndex = 0;
            this.lblS2Top.Text = "ĐANG GIAO";
            // 
            // lblS2Val
            // 
            this.lblS2Val.AutoSize = true;
            this.lblS2Val.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblS2Val.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.lblS2Val.Location = new System.Drawing.Point(18, 30);
            this.lblS2Val.Name = "lblS2Val";
            this.lblS2Val.Size = new System.Drawing.Size(40, 46);
            this.lblS2Val.TabIndex = 2;
            this.lblS2Val.Text = "9";
            // 
            // pnlS3
            // 
            this.pnlS3.BackColor = System.Drawing.Color.White;
            this.pnlS3.Controls.Add(this.label1);
            this.pnlS3.Controls.Add(this.lblS3Top);
            this.pnlS3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlS3.Location = new System.Drawing.Point(487, 0);
            this.pnlS3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.pnlS3.Name = "pnlS3";
            this.pnlS3.Size = new System.Drawing.Size(236, 92);
            this.pnlS3.TabIndex = 2;
            // 
            // lblS3Top
            // 
            this.lblS3Top.AutoSize = true;
            this.lblS3Top.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblS3Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblS3Top.Location = new System.Drawing.Point(18, 13);
            this.lblS3Top.Name = "lblS3Top";
            this.lblS3Top.Size = new System.Drawing.Size(126, 17);
            this.lblS3Top.TabIndex = 1;
            this.lblS3Top.Text = "GIAO THÀNH CÔNG";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.label1.Location = new System.Drawing.Point(26, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 46);
            this.label1.TabIndex = 3;
            this.label1.Text = "24";
            // 
            // pnlS4
            // 
            this.pnlS4.BackColor = System.Drawing.Color.White;
            this.pnlS4.Controls.Add(this.label2);
            this.pnlS4.Controls.Add(this.lblS4Top);
            this.pnlS4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlS4.Location = new System.Drawing.Point(732, 0);
            this.pnlS4.Margin = new System.Windows.Forms.Padding(6, 0, 0, 0);
            this.pnlS4.Name = "pnlS4";
            this.pnlS4.Size = new System.Drawing.Size(236, 92);
            this.pnlS4.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.label2.Location = new System.Drawing.Point(26, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 46);
            this.label2.TabIndex = 3;
            this.label2.Text = "2";
            // 
            // lblS4Top
            // 
            this.lblS4Top.AutoSize = true;
            this.lblS4Top.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblS4Top.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.lblS4Top.Location = new System.Drawing.Point(18, 13);
            this.lblS4Top.Name = "lblS4Top";
            this.lblS4Top.Size = new System.Drawing.Size(85, 17);
            this.lblS4Top.TabIndex = 1;
            this.lblS4Top.Text = "HOÀN HÀNG";
            // 
            // pnlTable
            // 
            this.pnlTable.BackColor = System.Drawing.Color.White;
            this.pnlTable.Controls.Add(this.dgvGiaoHang);
            this.pnlTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTable.Location = new System.Drawing.Point(0, 172);
            this.pnlTable.Name = "pnlTable";
            this.pnlTable.Padding = new System.Windows.Forms.Padding(28, 12, 28, 16);
            this.pnlTable.Size = new System.Drawing.Size(1024, 548);
            this.pnlTable.TabIndex = 2;
            // 
            // dgvGiaoHang
            // 
            this.dgvGiaoHang.AllowUserToAddRows = false;
            this.dgvGiaoHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvGiaoHang.ColumnHeadersHeight = 36;
            this.dgvGiaoHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvGiaoHang.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaDon,
            this.colKhach,
            this.colDiaChi,
            this.colGio,
            this.colShipper,
            this.colTT,
            this.colAction});
            this.dgvGiaoHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGiaoHang.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.dgvGiaoHang.Location = new System.Drawing.Point(28, 12);
            this.dgvGiaoHang.Name = "dgvGiaoHang";
            this.dgvGiaoHang.RowHeadersVisible = false;
            this.dgvGiaoHang.RowHeadersWidth = 51;
            this.dgvGiaoHang.RowTemplate.Height = 44;
            this.dgvGiaoHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGiaoHang.Size = new System.Drawing.Size(968, 520);
            this.dgvGiaoHang.TabIndex = 0;
            // 
            // colMaDon
            // 
            this.colMaDon.HeaderText = "MÃ ĐƠN";
            this.colMaDon.MinimumWidth = 6;
            this.colMaDon.Name = "colMaDon";
            // 
            // colKhach
            // 
            this.colKhach.HeaderText = "KHÁCH HÀNG";
            this.colKhach.MinimumWidth = 6;
            this.colKhach.Name = "colKhach";
            this.colKhach.Width = 150;
            // 
            // colDiaChi
            // 
            this.colDiaChi.HeaderText = "ĐỊA CHỈ";
            this.colDiaChi.MinimumWidth = 6;
            this.colDiaChi.Name = "colDiaChi";
            this.colDiaChi.Width = 200;
            // 
            // colGio
            // 
            this.colGio.HeaderText = "GIỜ YÊU CẦU";
            this.colGio.MinimumWidth = 6;
            this.colGio.Name = "colGio";
            this.colGio.Width = 120;
            // 
            // colShipper
            // 
            this.colShipper.HeaderText = "SHIPPER";
            this.colShipper.MinimumWidth = 6;
            this.colShipper.Name = "colShipper";
            this.colShipper.Width = 120;
            // 
            // colTT
            // 
            this.colTT.HeaderText = "TRẠNG THÁI";
            this.colTT.MinimumWidth = 6;
            this.colTT.Name = "colTT";
            this.colTT.Width = 120;
            // 
            // colAction
            // 
            this.colAction.HeaderText = "";
            this.colAction.MinimumWidth = 6;
            this.colAction.Name = "colAction";
            this.colAction.Width = 80;
            // 
            // ucGiaoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlTable);
            this.Controls.Add(this.pnlStats);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucGiaoHang";
            this.Size = new System.Drawing.Size(1024, 720);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlS1.ResumeLayout(false);
            this.pnlS1.PerformLayout();
            this.tlpStats.ResumeLayout(false);
            this.pnlS2.ResumeLayout(false);
            this.pnlS2.PerformLayout();
            this.pnlS3.ResumeLayout(false);
            this.pnlS3.PerformLayout();
            this.pnlS4.ResumeLayout(false);
            this.pnlS4.PerformLayout();
            this.pnlTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGiaoHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnPhanCong;
        private System.Windows.Forms.Label lblSub;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private System.Windows.Forms.Panel pnlS1;
        private System.Windows.Forms.Label lblS1Val;
        private System.Windows.Forms.Label lblS1Top;
        private System.Windows.Forms.Panel pnlS2;
        private System.Windows.Forms.Label lblS2Top;
        private System.Windows.Forms.Panel pnlS4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblS4Top;
        private System.Windows.Forms.Label lblS2Val;
        private System.Windows.Forms.Panel pnlS3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblS3Top;
        private System.Windows.Forms.Panel pnlTable;
        private System.Windows.Forms.DataGridView dgvGiaoHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKhach;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDiaChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colShipper;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTT;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAction;
    }
}
