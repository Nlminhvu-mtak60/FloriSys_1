namespace FloriSys._3_BanHang
{
    partial class ucDashboardBanHang
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
            this.pnlDonToi = new System.Windows.Forms.Panel();
            this.lblValDonToi = new System.Windows.Forms.Label();
            this.lblTitleDonToi = new System.Windows.Forms.Label();
            this.pnlDoanhThu = new System.Windows.Forms.Panel();
            this.lblValDoanhThu = new System.Windows.Forms.Label();
            this.lblTitleDoanhThu = new System.Windows.Forms.Label();
            this.pnlDangXuLy = new System.Windows.Forms.Panel();
            this.lblValDangXuLy = new System.Windows.Forms.Label();
            this.lblTitleDangXuLy = new System.Windows.Forms.Label();
            this.pnlHoanThanh = new System.Windows.Forms.Panel();
            this.lblValHoanThanh = new System.Windows.Forms.Label();
            this.lblTitleHoanThanh = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblTableTitle = new System.Windows.Forms.Label();
            this.dgvDonGanDay = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlKhaNang = new System.Windows.Forms.Panel();
            this.lblRank = new System.Windows.Forms.Label();
            this.lblTargetDoanhThu = new System.Windows.Forms.Label();
            this.pbDoanhThu = new System.Windows.Forms.ProgressBar();
            this.lblTargetDon = new System.Windows.Forms.Label();
            this.pbDonHang = new System.Windows.Forms.ProgressBar();
            this.lblHieuSuatTitle = new System.Windows.Forms.Label();
            this.pnlLookup = new System.Windows.Forms.Panel();
            this.dgvLookup = new System.Windows.Forms.DataGridView();
            this.btnTimLookup = new System.Windows.Forms.Button();
            this.txtLookup = new System.Windows.Forms.TextBox();
            this.lblLookupTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlKPI.SuspendLayout();
            this.pnlDonToi.SuspendLayout();
            this.pnlDoanhThu.SuspendLayout();
            this.pnlDangXuLy.SuspendLayout();
            this.pnlHoanThanh.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonGanDay)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.pnlKhaNang.SuspendLayout();
            this.pnlLookup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLookup)).BeginInit();
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
            this.lblSubTitle.Size = new System.Drawing.Size(184, 20);
            this.lblSubTitle.TabIndex = 1;
            this.lblSubTitle.Text = "Chào mừng trở lại, Cashier";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(290, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dashboard Bán hàng ";
            // 
            // pnlKPI
            // 
            this.pnlKPI.ColumnCount = 4;
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.pnlKPI.Controls.Add(this.pnlDonToi, 0, 0);
            this.pnlKPI.Controls.Add(this.pnlDoanhThu, 1, 0);
            this.pnlKPI.Controls.Add(this.pnlDangXuLy, 2, 0);
            this.pnlKPI.Controls.Add(this.pnlHoanThanh, 3, 0);
            this.pnlKPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKPI.Location = new System.Drawing.Point(0, 80);
            this.pnlKPI.Name = "pnlKPI";
            this.pnlKPI.Padding = new System.Windows.Forms.Padding(10);
            this.pnlKPI.RowCount = 1;
            this.pnlKPI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKPI.Size = new System.Drawing.Size(1000, 120);
            this.pnlKPI.TabIndex = 1;
            // 
            // pnlDonToi
            // 
            this.pnlDonToi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.pnlDonToi.Controls.Add(this.lblValDonToi);
            this.pnlDonToi.Controls.Add(this.lblTitleDonToi);
            this.pnlDonToi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDonToi.Location = new System.Drawing.Point(15, 15);
            this.pnlDonToi.Margin = new System.Windows.Forms.Padding(5);
            this.pnlDonToi.Name = "pnlDonToi";
            this.pnlDonToi.Size = new System.Drawing.Size(235, 90);
            this.pnlDonToi.TabIndex = 0;
            // 
            // lblValDonToi
            // 
            this.lblValDonToi.AutoSize = true;
            this.lblValDonToi.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValDonToi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblValDonToi.Location = new System.Drawing.Point(10, 35);
            this.lblValDonToi.Name = "lblValDonToi";
            this.lblValDonToi.Size = new System.Drawing.Size(60, 46);
            this.lblValDonToi.TabIndex = 1;
            this.lblValDonToi.Text = "12";
            // 
            // lblTitleDonToi
            // 
            this.lblTitleDonToi.AutoSize = true;
            this.lblTitleDonToi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleDonToi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.lblTitleDonToi.Location = new System.Drawing.Point(10, 10);
            this.lblTitleDonToi.Name = "lblTitleDonToi";
            this.lblTitleDonToi.Size = new System.Drawing.Size(126, 20);
            this.lblTitleDonToi.TabIndex = 0;
            this.lblTitleDonToi.Text = "Đơn tôi hôm nay";
            // 
            // pnlDoanhThu
            // 
            this.pnlDoanhThu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.pnlDoanhThu.Controls.Add(this.lblValDoanhThu);
            this.pnlDoanhThu.Controls.Add(this.lblTitleDoanhThu);
            this.pnlDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDoanhThu.Location = new System.Drawing.Point(260, 15);
            this.pnlDoanhThu.Margin = new System.Windows.Forms.Padding(5);
            this.pnlDoanhThu.Name = "pnlDoanhThu";
            this.pnlDoanhThu.Size = new System.Drawing.Size(235, 90);
            this.pnlDoanhThu.TabIndex = 1;
            // 
            // lblValDoanhThu
            // 
            this.lblValDoanhThu.AutoSize = true;
            this.lblValDoanhThu.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblValDoanhThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.lblValDoanhThu.Location = new System.Drawing.Point(10, 35);
            this.lblValDoanhThu.Name = "lblValDoanhThu";
            this.lblValDoanhThu.Size = new System.Drawing.Size(89, 41);
            this.lblValDoanhThu.TabIndex = 1;
            this.lblValDoanhThu.Text = "2.8M";
            // 
            // lblTitleDoanhThu
            // 
            this.lblTitleDoanhThu.AutoSize = true;
            this.lblTitleDoanhThu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleDoanhThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(128)))), ((int)(((byte)(61)))));
            this.lblTitleDoanhThu.Location = new System.Drawing.Point(10, 10);
            this.lblTitleDoanhThu.Name = "lblTitleDoanhThu";
            this.lblTitleDoanhThu.Size = new System.Drawing.Size(126, 20);
            this.lblTitleDoanhThu.TabIndex = 0;
            this.lblTitleDoanhThu.Text = "Doanh số của tôi";
            // 
            // pnlDangXuLy
            // 
            this.pnlDangXuLy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.pnlDangXuLy.Controls.Add(this.lblValDangXuLy);
            this.pnlDangXuLy.Controls.Add(this.lblTitleDangXuLy);
            this.pnlDangXuLy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDangXuLy.Location = new System.Drawing.Point(505, 15);
            this.pnlDangXuLy.Margin = new System.Windows.Forms.Padding(5);
            this.pnlDangXuLy.Name = "pnlDangXuLy";
            this.pnlDangXuLy.Size = new System.Drawing.Size(235, 90);
            this.pnlDangXuLy.TabIndex = 2;
            // 
            // lblValDangXuLy
            // 
            this.lblValDangXuLy.AutoSize = true;
            this.lblValDangXuLy.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValDangXuLy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.lblValDangXuLy.Location = new System.Drawing.Point(10, 35);
            this.lblValDangXuLy.Name = "lblValDangXuLy";
            this.lblValDangXuLy.Size = new System.Drawing.Size(40, 46);
            this.lblValDangXuLy.TabIndex = 1;
            this.lblValDangXuLy.Text = "4";
            // 
            // lblTitleDangXuLy
            // 
            this.lblTitleDangXuLy.AutoSize = true;
            this.lblTitleDangXuLy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleDangXuLy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(64)))), ((int)(((byte)(14)))));
            this.lblTitleDangXuLy.Location = new System.Drawing.Point(10, 10);
            this.lblTitleDangXuLy.Name = "lblTitleDangXuLy";
            this.lblTitleDangXuLy.Size = new System.Drawing.Size(115, 20);
            this.lblTitleDangXuLy.TabIndex = 0;
            this.lblTitleDangXuLy.Text = "Đơn đang xử lý";
            // 
            // pnlHoanThanh
            // 
            this.pnlHoanThanh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.pnlHoanThanh.Controls.Add(this.lblValHoanThanh);
            this.pnlHoanThanh.Controls.Add(this.lblTitleHoanThanh);
            this.pnlHoanThanh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHoanThanh.Location = new System.Drawing.Point(750, 15);
            this.pnlHoanThanh.Margin = new System.Windows.Forms.Padding(5);
            this.pnlHoanThanh.Name = "pnlHoanThanh";
            this.pnlHoanThanh.Size = new System.Drawing.Size(235, 90);
            this.pnlHoanThanh.TabIndex = 3;
            // 
            // lblValHoanThanh
            // 
            this.lblValHoanThanh.AutoSize = true;
            this.lblValHoanThanh.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValHoanThanh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.lblValHoanThanh.Location = new System.Drawing.Point(10, 35);
            this.lblValHoanThanh.Name = "lblValHoanThanh";
            this.lblValHoanThanh.Size = new System.Drawing.Size(40, 46);
            this.lblValHoanThanh.TabIndex = 1;
            this.lblValHoanThanh.Text = "8";
            // 
            // lblTitleHoanThanh
            // 
            this.lblTitleHoanThanh.AutoSize = true;
            this.lblTitleHoanThanh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTitleHoanThanh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(64)))), ((int)(((byte)(175)))));
            this.lblTitleHoanThanh.Location = new System.Drawing.Point(10, 10);
            this.lblTitleHoanThanh.Name = "lblTitleHoanThanh";
            this.lblTitleHoanThanh.Size = new System.Drawing.Size(122, 20);
            this.lblTitleHoanThanh.TabIndex = 0;
            this.lblTitleHoanThanh.Text = "Đơn hoàn thành";
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
            this.pnlLeft.Controls.Add(this.dgvDonGanDay);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(13, 13);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Size = new System.Drawing.Size(582, 494);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblTableTitle
            // 
            this.lblTableTitle.AutoSize = true;
            this.lblTableTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTableTitle.Location = new System.Drawing.Point(13, 10);
            this.lblTableTitle.Name = "lblTableTitle";
            this.lblTableTitle.Size = new System.Drawing.Size(250, 23);
            this.lblTableTitle.TabIndex = 1;
            this.lblTableTitle.Text = "📋 Đơn hàng tôi tạo hôm nay";
            // 
            // dgvDonGanDay
            // 
            this.dgvDonGanDay.AllowUserToAddRows = false;
            this.dgvDonGanDay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDonGanDay.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDonGanDay.BackgroundColor = System.Drawing.Color.White;
            this.dgvDonGanDay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDonGanDay.Location = new System.Drawing.Point(10, 40);
            this.dgvDonGanDay.Name = "dgvDonGanDay";
            this.dgvDonGanDay.RowHeadersVisible = false;
            this.dgvDonGanDay.RowHeadersWidth = 51;
            this.dgvDonGanDay.Size = new System.Drawing.Size(562, 444);
            this.dgvDonGanDay.TabIndex = 2;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.pnlKhaNang);
            this.pnlRight.Controls.Add(this.pnlLookup);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(601, 13);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(386, 494);
            this.pnlRight.TabIndex = 1;
            // 
            // pnlKhaNang
            // 
            this.pnlKhaNang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlKhaNang.Controls.Add(this.lblRank);
            this.pnlKhaNang.Controls.Add(this.lblTargetDoanhThu);
            this.pnlKhaNang.Controls.Add(this.pbDoanhThu);
            this.pnlKhaNang.Controls.Add(this.lblTargetDon);
            this.pnlKhaNang.Controls.Add(this.pbDonHang);
            this.pnlKhaNang.Controls.Add(this.lblHieuSuatTitle);
            this.pnlKhaNang.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKhaNang.Location = new System.Drawing.Point(0, 0);
            this.pnlKhaNang.Name = "pnlKhaNang";
            this.pnlKhaNang.Padding = new System.Windows.Forms.Padding(10);
            this.pnlKhaNang.Size = new System.Drawing.Size(386, 180);
            this.pnlKhaNang.TabIndex = 0;
            // 
            // lblRank
            // 
            this.lblRank.AutoSize = true;
            this.lblRank.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblRank.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblRank.Location = new System.Drawing.Point(10, 140);
            this.lblRank.Name = "lblRank";
            this.lblRank.Size = new System.Drawing.Size(197, 23);
            this.lblRank.TabIndex = 5;
            this.lblRank.Text = "🥇 Xếp hạng: #1 / 3 nv";
            // 
            // lblTargetDoanhThu
            // 
            this.lblTargetDoanhThu.AutoSize = true;
            this.lblTargetDoanhThu.Location = new System.Drawing.Point(10, 90);
            this.lblTargetDoanhThu.Name = "lblTargetDoanhThu";
            this.lblTargetDoanhThu.Size = new System.Drawing.Size(148, 16);
            this.lblTargetDoanhThu.TabIndex = 4;
            this.lblTargetDoanhThu.Text = "Doanh thu (48.6M / 50M)";
            // 
            // pbDoanhThu
            // 
            this.pbDoanhThu.Location = new System.Drawing.Point(10, 110);
            this.pbDoanhThu.Name = "pbDoanhThu";
            this.pbDoanhThu.Size = new System.Drawing.Size(350, 12);
            this.pbDoanhThu.TabIndex = 3;
            this.pbDoanhThu.Value = 97;
            // 
            // lblTargetDon
            // 
            this.lblTargetDon.AutoSize = true;
            this.lblTargetDon.Location = new System.Drawing.Point(10, 40);
            this.lblTargetDon.Name = "lblTargetDon";
            this.lblTargetDon.Size = new System.Drawing.Size(146, 16);
            this.lblTargetDon.TabIndex = 2;
            this.lblTargetDon.Text = "Số đơn hàng (187 / 200)";
            // 
            // pbDonHang
            // 
            this.pbDonHang.Location = new System.Drawing.Point(10, 60);
            this.pbDonHang.Name = "pbDonHang";
            this.pbDonHang.Size = new System.Drawing.Size(350, 12);
            this.pbDonHang.TabIndex = 1;
            this.pbDonHang.Value = 94;
            // 
            // lblHieuSuatTitle
            // 
            this.lblHieuSuatTitle.AutoSize = true;
            this.lblHieuSuatTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHieuSuatTitle.Location = new System.Drawing.Point(10, 10);
            this.lblHieuSuatTitle.Name = "lblHieuSuatTitle";
            this.lblHieuSuatTitle.Size = new System.Drawing.Size(229, 23);
            this.lblHieuSuatTitle.TabIndex = 0;
            this.lblHieuSuatTitle.Text = "📈 Hiệu suất tháng 3/2026";
            // 
            // pnlLookup
            // 
            this.pnlLookup.Controls.Add(this.dgvLookup);
            this.pnlLookup.Controls.Add(this.btnTimLookup);
            this.pnlLookup.Controls.Add(this.txtLookup);
            this.pnlLookup.Controls.Add(this.lblLookupTitle);
            this.pnlLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLookup.Location = new System.Drawing.Point(0, 0);
            this.pnlLookup.Name = "pnlLookup";
            this.pnlLookup.Padding = new System.Windows.Forms.Padding(10, 200, 10, 10);
            this.pnlLookup.Size = new System.Drawing.Size(386, 494);
            this.pnlLookup.TabIndex = 1;
            // 
            // dgvLookup
            // 
            this.dgvLookup.AllowUserToAddRows = false;
            this.dgvLookup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLookup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLookup.BackgroundColor = System.Drawing.Color.White;
            this.dgvLookup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLookup.Location = new System.Drawing.Point(10, 260);
            this.dgvLookup.Name = "dgvLookup";
            this.dgvLookup.RowHeadersVisible = false;
            this.dgvLookup.RowHeadersWidth = 51;
            this.dgvLookup.Size = new System.Drawing.Size(362, 224);
            this.dgvLookup.TabIndex = 3;
            // 
            // btnTimLookup
            // 
            this.btnTimLookup.Location = new System.Drawing.Point(290, 225);
            this.btnTimLookup.Name = "btnTimLookup";
            this.btnTimLookup.Size = new System.Drawing.Size(80, 28);
            this.btnTimLookup.TabIndex = 2;
            this.btnTimLookup.Text = "Tìm";
            this.btnTimLookup.UseVisualStyleBackColor = true;
            this.btnTimLookup.Click += new System.EventHandler(this.btnTimLookup_Click);
            // 
            // txtLookup
            // 
            this.txtLookup.Location = new System.Drawing.Point(10, 228);
            this.txtLookup.Name = "txtLookup";
            this.txtLookup.Size = new System.Drawing.Size(270, 22);
            this.txtLookup.TabIndex = 1;
            // 
            // lblLookupTitle
            // 
            this.lblLookupTitle.AutoSize = true;
            this.lblLookupTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLookupTitle.Location = new System.Drawing.Point(10, 195);
            this.lblLookupTitle.Name = "lblLookupTitle";
            this.lblLookupTitle.Size = new System.Drawing.Size(219, 23);
            this.lblLookupTitle.TabIndex = 0;
            this.lblLookupTitle.Text = "🌸 Tra cứu nhanh tồn kho";
            // 
            // ucDashboardBanHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlKPI);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucDashboardBanHang";
            this.Size = new System.Drawing.Size(1000, 720);
            this.Load += new System.EventHandler(this.ucDashboardBanHang_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlKPI.ResumeLayout(false);
            this.pnlDonToi.ResumeLayout(false);
            this.pnlDonToi.PerformLayout();
            this.pnlDoanhThu.ResumeLayout(false);
            this.pnlDoanhThu.PerformLayout();
            this.pnlDangXuLy.ResumeLayout(false);
            this.pnlDangXuLy.PerformLayout();
            this.pnlHoanThanh.ResumeLayout(false);
            this.pnlHoanThanh.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDonGanDay)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.pnlKhaNang.ResumeLayout(false);
            this.pnlKhaNang.PerformLayout();
            this.pnlLookup.ResumeLayout(false);
            this.pnlLookup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLookup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblSubTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel pnlKPI;
        private System.Windows.Forms.Panel pnlDonToi;
        private System.Windows.Forms.Label lblValDonToi;
        private System.Windows.Forms.Label lblTitleDonToi;
        private System.Windows.Forms.Panel pnlDoanhThu;
        private System.Windows.Forms.Label lblValDoanhThu;
        private System.Windows.Forms.Label lblTitleDoanhThu;
        private System.Windows.Forms.Panel pnlDangXuLy;
        private System.Windows.Forms.Label lblValDangXuLy;
        private System.Windows.Forms.Label lblTitleDangXuLy;
        private System.Windows.Forms.Panel pnlHoanThanh;
        private System.Windows.Forms.Label lblValHoanThanh;
        private System.Windows.Forms.Label lblTitleHoanThanh;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Label lblTableTitle;
        private System.Windows.Forms.DataGridView dgvDonGanDay;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlKhaNang;
        private System.Windows.Forms.Label lblHieuSuatTitle;
        private System.Windows.Forms.Label lblTargetDon;
        private System.Windows.Forms.ProgressBar pbDonHang;
        private System.Windows.Forms.Label lblRank;
        private System.Windows.Forms.Label lblTargetDoanhThu;
        private System.Windows.Forms.ProgressBar pbDoanhThu;
        private System.Windows.Forms.Panel pnlLookup;
        private System.Windows.Forms.Label lblLookupTitle;
        private System.Windows.Forms.TextBox txtLookup;
        private System.Windows.Forms.Button btnTimLookup;
        private System.Windows.Forms.DataGridView dgvLookup;
    }
}
