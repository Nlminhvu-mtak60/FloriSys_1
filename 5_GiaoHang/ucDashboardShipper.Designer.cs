namespace FloriSys._5_GiaoHang
{
    partial class ucDashboardShipper
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
            this.pnlTongDon = new System.Windows.Forms.Panel();
            this.lblValTongDon = new System.Windows.Forms.Label();
            this.lblTitleTongDon = new System.Windows.Forms.Label();
            this.pnlDaGiao = new System.Windows.Forms.Panel();
            this.lblValDaGiao = new System.Windows.Forms.Label();
            this.lblTitleDaGiao = new System.Windows.Forms.Label();
            this.pnlDangGiao = new System.Windows.Forms.Panel();
            this.lblValDangGiao = new System.Windows.Forms.Label();
            this.lblTitleDangGiao = new System.Windows.Forms.Label();
            this.pnlChuaGiao = new System.Windows.Forms.Panel();
            this.lblValChuaGiao = new System.Windows.Forms.Label();
            this.lblTitleChuaGiao = new System.Windows.Forms.Label();
            this.pnlCurrent = new System.Windows.Forms.Panel();
            this.btnHoanHang = new System.Windows.Forms.Button();
            this.btnKhachVang = new System.Windows.Forms.Button();
            this.btnThanhCong = new System.Windows.Forms.Button();
            this.lblCurAddress = new System.Windows.Forms.Label();
            this.lblCurPhone = new System.Windows.Forms.Label();
            this.lblCurCustomer = new System.Windows.Forms.Label();
            this.lblCurTitle = new System.Windows.Forms.Label();
            this.dgvAllDon = new System.Windows.Forms.DataGridView();
            this.lblListTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlKPI.SuspendLayout();
            this.pnlTongDon.SuspendLayout();
            this.pnlDaGiao.SuspendLayout();
            this.pnlDangGiao.SuspendLayout();
            this.pnlChuaGiao.SuspendLayout();
            this.pnlCurrent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllDon)).BeginInit();
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
            this.lblSubTitle.Size = new System.Drawing.Size(252, 20);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Danh sách giao hàng trong ngày của bạn";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Xin chào, Shipper ";
            // 
            // pnlKPI
            // 
            this.pnlKPI.ColumnCount = 4;
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.Controls.Add(this.pnlTongDon, 0, 0);
            this.pnlKPI.Controls.Add(this.pnlDaGiao, 1, 0);
            this.pnlKPI.Controls.Add(this.pnlDangGiao, 2, 0);
            this.pnlKPI.Controls.Add(this.pnlChuaGiao, 3, 0);
            this.pnlKPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKPI.Location = new System.Drawing.Point(0, 80);
            this.pnlKPI.Name = "pnlKPI";
            this.pnlKPI.Padding = new System.Windows.Forms.Padding(10);
            this.pnlKPI.RowCount = 1;
            this.pnlKPI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKPI.Size = new System.Drawing.Size(1000, 120);
            this.pnlKPI.TabIndex = 1;
            // 
            // pnlTongDon
            // 
            this.pnlTongDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlTongDon.Controls.Add(this.lblValTongDon);
            this.pnlTongDon.Controls.Add(this.lblTitleTongDon);
            this.pnlTongDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTongDon.Location = new System.Drawing.Point(15, 15);
            this.pnlTongDon.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTongDon.Name = "pnlTongDon";
            this.pnlTongDon.Size = new System.Drawing.Size(235, 90);
            this.pnlTongDon.TabIndex = 0;
            // 
            // lblValTongDon
            // 
            this.lblValTongDon.AutoSize = true;
            this.lblValTongDon.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValTongDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblValTongDon.Location = new System.Drawing.Point(10, 35);
            this.lblValTongDon.Name = "lblValTongDon";
            this.lblValTongDon.Size = new System.Drawing.Size(40, 46);
            this.lblValTongDon.TabIndex = 1;
            this.lblValTongDon.Text = "6";
            // 
            // lblTitleTongDon
            // 
            this.lblTitleTongDon.AutoSize = true;
            this.lblTitleTongDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleTongDon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblTitleTongDon.Location = new System.Drawing.Point(10, 10);
            this.lblTitleTongDon.Name = "lblTitleTongDon";
            this.lblTitleTongDon.Size = new System.Drawing.Size(163, 20);
            this.lblTitleTongDon.TabIndex = 0;
            this.lblTitleTongDon.Text = "Đơn cần giao hôm nay";
            // 
            // pnlDaGiao
            // 
            this.pnlDaGiao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlDaGiao.Controls.Add(this.lblValDaGiao);
            this.pnlDaGiao.Controls.Add(this.lblTitleDaGiao);
            this.pnlDaGiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDaGiao.Location = new System.Drawing.Point(260, 15);
            this.pnlDaGiao.Margin = new System.Windows.Forms.Padding(5);
            this.pnlDaGiao.Name = "pnlDaGiao";
            this.pnlDaGiao.Size = new System.Drawing.Size(235, 90);
            this.pnlDaGiao.TabIndex = 1;
            // 
            // lblValDaGiao
            // 
            this.lblValDaGiao.AutoSize = true;
            this.lblValDaGiao.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValDaGiao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblValDaGiao.Location = new System.Drawing.Point(10, 35);
            this.lblValDaGiao.Name = "lblValDaGiao";
            this.lblValDaGiao.Size = new System.Drawing.Size(40, 46);
            this.lblValDaGiao.TabIndex = 1;
            this.lblValDaGiao.Text = "2";
            // 
            // lblTitleDaGiao
            // 
            this.lblTitleDaGiao.AutoSize = true;
            this.lblTitleDaGiao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleDaGiao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblTitleDaGiao.Location = new System.Drawing.Point(10, 10);
            this.lblTitleDaGiao.Name = "lblTitleDaGiao";
            this.lblTitleDaGiao.Size = new System.Drawing.Size(147, 20);
            this.lblTitleDaGiao.TabIndex = 0;
            this.lblTitleDaGiao.Text = "Đã giao thành công";
            // 
            // pnlDangGiao
            // 
            this.pnlDangGiao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.pnlDangGiao.Controls.Add(this.lblValDangGiao);
            this.pnlDangGiao.Controls.Add(this.lblTitleDangGiao);
            this.pnlDangGiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDangGiao.Location = new System.Drawing.Point(505, 15);
            this.pnlDangGiao.Margin = new System.Windows.Forms.Padding(5);
            this.pnlDangGiao.Name = "pnlDangGiao";
            this.pnlDangGiao.Size = new System.Drawing.Size(235, 90);
            this.pnlDangGiao.TabIndex = 2;
            // 
            // lblValDangGiao
            // 
            this.lblValDangGiao.AutoSize = true;
            this.lblValDangGiao.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValDangGiao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblValDangGiao.Location = new System.Drawing.Point(10, 35);
            this.lblValDangGiao.Name = "lblValDangGiao";
            this.lblValDangGiao.Size = new System.Drawing.Size(40, 46);
            this.lblValDangGiao.TabIndex = 1;
            this.lblValDangGiao.Text = "1";
            // 
            // lblTitleDangGiao
            // 
            this.lblTitleDangGiao.AutoSize = true;
            this.lblTitleDangGiao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleDangGiao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblTitleDangGiao.Location = new System.Drawing.Point(10, 10);
            this.lblTitleDangGiao.Name = "lblTitleDangGiao";
            this.lblTitleDangGiao.Size = new System.Drawing.Size(117, 20);
            this.lblTitleDangGiao.TabIndex = 0;
            this.lblTitleDangGiao.Text = "Đang trên đường";
            // 
            // pnlChuaGiao
            // 
            this.pnlChuaGiao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlChuaGiao.Controls.Add(this.lblValChuaGiao);
            this.pnlChuaGiao.Controls.Add(this.lblTitleChuaGiao);
            this.pnlChuaGiao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChuaGiao.Location = new System.Drawing.Point(750, 15);
            this.pnlChuaGiao.Margin = new System.Windows.Forms.Padding(5);
            this.pnlChuaGiao.Name = "pnlChuaGiao";
            this.pnlChuaGiao.Size = new System.Drawing.Size(235, 90);
            this.pnlChuaGiao.TabIndex = 3;
            // 
            // lblValChuaGiao
            // 
            this.lblValChuaGiao.AutoSize = true;
            this.lblValChuaGiao.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValChuaGiao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblValChuaGiao.Location = new System.Drawing.Point(10, 35);
            this.lblValChuaGiao.Name = "lblValChuaGiao";
            this.lblValChuaGiao.Size = new System.Drawing.Size(40, 46);
            this.lblValChuaGiao.TabIndex = 1;
            this.lblValChuaGiao.Text = "3";
            // 
            // lblTitleChuaGiao
            // 
            this.lblTitleChuaGiao.AutoSize = true;
            this.lblTitleChuaGiao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleChuaGiao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTitleChuaGiao.Location = new System.Drawing.Point(10, 10);
            this.lblTitleChuaGiao.Name = "lblTitleChuaGiao";
            this.lblTitleChuaGiao.Size = new System.Drawing.Size(77, 20);
            this.lblTitleChuaGiao.TabIndex = 0;
            this.lblTitleChuaGiao.Text = "Chưa giao";
            // 
            // pnlCurrent
            // 
            this.pnlCurrent.BackColor = System.Drawing.Color.White;
            this.pnlCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCurrent.Controls.Add(this.btnHoanHang);
            this.pnlCurrent.Controls.Add(this.btnKhachVang);
            this.pnlCurrent.Controls.Add(this.btnThanhCong);
            this.pnlCurrent.Controls.Add(this.lblCurAddress);
            this.pnlCurrent.Controls.Add(this.lblCurPhone);
            this.pnlCurrent.Controls.Add(this.lblCurCustomer);
            this.pnlCurrent.Controls.Add(this.lblCurTitle);
            this.pnlCurrent.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCurrent.Location = new System.Drawing.Point(0, 200);
            this.pnlCurrent.Name = "pnlCurrent";
            this.pnlCurrent.Size = new System.Drawing.Size(1000, 200);
            this.pnlCurrent.TabIndex = 2;
            // 
            // btnHoanHang
            // 
            this.btnHoanHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnHoanHang.FlatAppearance.BorderSize = 0;
            this.btnHoanHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHoanHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHoanHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btnHoanHang.Location = new System.Drawing.Point(365, 140);
            this.btnHoanHang.Name = "btnHoanHang";
            this.btnHoanHang.Size = new System.Drawing.Size(120, 35);
            this.btnHoanHang.TabIndex = 6;
            this.btnHoanHang.Text = "↩️ Hoàn hàng";
            this.btnHoanHang.UseVisualStyleBackColor = false;
            this.btnHoanHang.Click += new System.EventHandler(this.btnHoanHang_Click);
            // 
            // btnKhachVang
            // 
            this.btnKhachVang.BackColor = System.Drawing.Color.Gray;
            this.btnKhachVang.FlatAppearance.BorderSize = 0;
            this.btnKhachVang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachVang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnKhachVang.ForeColor = System.Drawing.Color.White;
            this.btnKhachVang.Location = new System.Drawing.Point(195, 140);
            this.btnKhachVang.Name = "btnKhachVang";
            this.btnKhachVang.Size = new System.Drawing.Size(160, 35);
            this.btnKhachVang.TabIndex = 5;
            this.btnKhachVang.Text = "👤 Khách vắng mặt";
            this.btnKhachVang.UseVisualStyleBackColor = false;
            this.btnKhachVang.Click += new System.EventHandler(this.btnKhachVang_Click);
            // 
            // btnThanhCong
            // 
            this.btnThanhCong.BackColor = System.Drawing.Color.Green;
            this.btnThanhCong.FlatAppearance.BorderSize = 0;
            this.btnThanhCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhCong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThanhCong.ForeColor = System.Drawing.Color.White;
            this.btnThanhCong.Location = new System.Drawing.Point(25, 140);
            this.btnThanhCong.Name = "btnThanhCong";
            this.btnThanhCong.Size = new System.Drawing.Size(160, 35);
            this.btnThanhCong.TabIndex = 4;
            this.btnThanhCong.Text = "✅ Giao thành công";
            this.btnThanhCong.UseVisualStyleBackColor = false;
            this.btnThanhCong.Click += new System.EventHandler(this.btnThanhCong_Click);
            // 
            // lblCurAddress
            // 
            this.lblCurAddress.AutoSize = true;
            this.lblCurAddress.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurAddress.Location = new System.Drawing.Point(25, 100);
            this.lblCurAddress.Name = "lblCurAddress";
            this.lblCurAddress.Size = new System.Drawing.Size(262, 23);
            this.lblCurAddress.TabIndex = 3;
            this.lblCurAddress.Text = "📍 Địa chỉ: 45 Nguyễn Huệ, Q.1";
            // 
            // lblCurPhone
            // 
            this.lblCurPhone.AutoSize = true;
            this.lblCurPhone.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCurPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblCurPhone.Location = new System.Drawing.Point(25, 75);
            this.lblCurPhone.Name = "lblCurPhone";
            this.lblCurPhone.Size = new System.Drawing.Size(189, 23);
            this.lblCurPhone.TabIndex = 2;
            this.lblCurPhone.Text = "📞 SĐT: 0912 345 678";
            // 
            // lblCurCustomer
            // 
            this.lblCurCustomer.AutoSize = true;
            this.lblCurCustomer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCurCustomer.Location = new System.Drawing.Point(20, 45);
            this.lblCurCustomer.Name = "lblCurCustomer";
            this.lblCurCustomer.Size = new System.Drawing.Size(260, 28);
            this.lblCurCustomer.TabIndex = 1;
            this.lblCurCustomer.Text = "Khách hàng: Trần Văn Hùng";
            // 
            // lblCurTitle
            // 
            this.lblCurTitle.AutoSize = true;
            this.lblCurTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCurTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblCurTitle.Location = new System.Drawing.Point(10, 10);
            this.lblCurTitle.Name = "lblCurTitle";
            this.lblCurTitle.Size = new System.Drawing.Size(236, 23);
            this.lblCurTitle.TabIndex = 0;
            this.lblCurTitle.Text = "🔴 Đơn đang giao – DH0046";
            // 
            // dgvAllDon
            // 
            this.dgvAllDon.AllowUserToAddRows = false;
            this.dgvAllDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllDon.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllDon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAllDon.Location = new System.Drawing.Point(0, 440);
            this.dgvAllDon.Name = "dgvAllDon";
            this.dgvAllDon.RowHeadersVisible = false;
            this.dgvAllDon.RowHeadersWidth = 51;
            this.dgvAllDon.Size = new System.Drawing.Size(1000, 280);
            this.dgvAllDon.TabIndex = 3;
            this.dgvAllDon.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAllDon_CellClick);
            // 
            // lblListTitle
            // 
            this.lblListTitle.AutoSize = true;
            this.lblListTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblListTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblListTitle.Location = new System.Drawing.Point(0, 400);
            this.lblListTitle.Name = "lblListTitle";
            this.lblListTitle.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.lblListTitle.Size = new System.Drawing.Size(217, 40);
            this.lblListTitle.TabIndex = 4;
            this.lblListTitle.Text = "📋 Tất cả đơn trong ngày";
            // 
            // ucDashboardShipper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAllDon);
            this.Controls.Add(this.lblListTitle);
            this.Controls.Add(this.pnlCurrent);
            this.Controls.Add(this.pnlKPI);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucDashboardShipper";
            this.Size = new System.Drawing.Size(1000, 720);
            this.Load += new System.EventHandler(this.ucDashboardShipper_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlKPI.ResumeLayout(false);
            this.pnlTongDon.ResumeLayout(false);
            this.pnlTongDon.PerformLayout();
            this.pnlDaGiao.ResumeLayout(false);
            this.pnlDaGiao.PerformLayout();
            this.pnlDangGiao.ResumeLayout(false);
            this.pnlDangGiao.PerformLayout();
            this.pnlChuaGiao.ResumeLayout(false);
            this.pnlChuaGiao.PerformLayout();
            this.pnlCurrent.ResumeLayout(false);
            this.pnlCurrent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel pnlKPI;
        private System.Windows.Forms.Panel pnlTongDon;
        private System.Windows.Forms.Label lblValTongDon;
        private System.Windows.Forms.Label lblTitleTongDon;
        private System.Windows.Forms.Panel pnlDaGiao;
        private System.Windows.Forms.Label lblValDaGiao;
        private System.Windows.Forms.Label lblTitleDaGiao;
        private System.Windows.Forms.Panel pnlDangGiao;
        private System.Windows.Forms.Label lblValDangGiao;
        private System.Windows.Forms.Label lblTitleDangGiao;
        private System.Windows.Forms.Panel pnlChuaGiao;
        private System.Windows.Forms.Label lblValChuaGiao;
        private System.Windows.Forms.Label lblTitleChuaGiao;
        private System.Windows.Forms.Panel pnlCurrent;
        private System.Windows.Forms.Label lblCurTitle;
        private System.Windows.Forms.Label lblCurCustomer;
        private System.Windows.Forms.Label lblCurPhone;
        private System.Windows.Forms.Label lblCurAddress;
        private System.Windows.Forms.Button btnThanhCong;
        private System.Windows.Forms.Button btnHoanHang;
        private System.Windows.Forms.Button btnKhachVang;
        private System.Windows.Forms.DataGridView dgvAllDon;
        private System.Windows.Forms.Label lblListTitle;
    }
}
