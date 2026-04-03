namespace FloriSys.Shared
{
    partial class ucPhanQuyen
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
            this.btnLuu = new System.Windows.Forms.Button();
            this.lblSubTitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlRoles = new System.Windows.Forms.FlowLayoutPanel();
            this.dgvQuyen = new System.Windows.Forms.DataGridView();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuyen)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnLuu);
            this.pnlHeader.Controls.Add(this.lblSubTitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 80);
            this.pnlHeader.TabIndex = 0;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(820, 20);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(150, 40);
            this.btnLuu.TabIndex = 2;
            this.btnLuu.Text = "💾 Lưu thay đổi";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.AutoSize = true;
            this.lblSubTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubTitle.Location = new System.Drawing.Point(20, 45);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.Size = new System.Drawing.Size(435, 20);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Quản lý quyền truy cập hệ thống theo từng vai trò (RBAC matrix)";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(263, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Phân quyền chi tiết";
            // 
            // pnlRoles
            // 
            this.pnlRoles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlRoles.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRoles.Location = new System.Drawing.Point(0, 80);
            this.pnlRoles.Name = "pnlRoles";
            this.pnlRoles.Padding = new System.Windows.Forms.Padding(10, 5, 0, 5);
            this.pnlRoles.Size = new System.Drawing.Size(1000, 50);
            this.pnlRoles.TabIndex = 1;
            // 
            // dgvQuyen
            // 
            this.dgvQuyen.AllowUserToAddRows = false;
            this.dgvQuyen.AllowUserToDeleteRows = false;
            this.dgvQuyen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQuyen.BackgroundColor = System.Drawing.Color.White;
            this.dgvQuyen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvQuyen.Location = new System.Drawing.Point(0, 160);
            this.dgvQuyen.Name = "dgvQuyen";
            this.dgvQuyen.RowHeadersVisible = false;
            this.dgvQuyen.RowHeadersWidth = 51;
            this.dgvQuyen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvQuyen.Size = new System.Drawing.Size(1000, 560);
            this.dgvQuyen.TabIndex = 2;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.BackColor = System.Drawing.Color.White;
            this.lblTableTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.Location = new System.Drawing.Point(0, 130);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Padding = new System.Windows.Forms.Padding(20, 5, 0, 0);
            this.lblTableTitle.Size = new System.Drawing.Size(1000, 30);
            this.lblTableTitle.TabIndex = 3;
            this.lblTableTitle.Text = "Ma trận quyền hạn cho vai trò: ...";
            // 
            // ucPhanQuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvQuyen);
            this.Controls.Add(this.lblTableTitle);
            this.Controls.Add(this.pnlRoles);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucPhanQuyen";
            this.Size = new System.Drawing.Size(1000, 720);
            this.Load += new System.EventHandler(this.ucPhanQuyen_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQuyen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.FlowLayoutPanel pnlRoles;
        private System.Windows.Forms.DataGridView dgvQuyen;
        private System.Windows.Forms.Label lblTableTitle;
    }
}
