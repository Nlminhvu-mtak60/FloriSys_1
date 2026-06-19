namespace FloriSys._3_BanHang
{
    partial class ucChiTietDonHang
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
            this.btnInHoaDon = new System.Windows.Forms.Button();
            this.btnPhanHoi = new System.Windows.Forms.Button();
            this.lblStatusBadge = new System.Windows.Forms.Label();
            this.lblMaDon = new System.Windows.Forms.Label();
            this.lblBack = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpSanPham = new System.Windows.Forms.GroupBox();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grpKhachHang = new System.Windows.Forms.GroupBox();
            this.flpKhachHang = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblNguoiNhan = new System.Windows.Forms.Label();
            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblGhiChu = new System.Windows.Forms.Label();

            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpAction = new System.Windows.Forms.GroupBox();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.grpTimeline = new System.Windows.Forms.GroupBox();
            this.pnlTimeline = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grpSanPham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.grpKhachHang.SuspendLayout();
            this.flpKhachHang.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.grpAction.SuspendLayout();
            this.grpTimeline.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.btnInHoaDon);
            this.pnlHeader.Controls.Add(this.btnPhanHoi);
            this.pnlHeader.Controls.Add(this.lblStatusBadge);
            this.pnlHeader.Controls.Add(this.lblMaDon);
            this.pnlHeader.Controls.Add(this.lblBack);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1100, 100);
            this.pnlHeader.TabIndex = 0;
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnInHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInHoaDon.FlatAppearance.BorderSize = 0;
            this.btnInHoaDon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInHoaDon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnInHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnInHoaDon.Location = new System.Drawing.Point(760, 30);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(150, 40);
            this.btnInHoaDon.TabIndex = 4;
            this.btnInHoaDon.Text = "🖨️ In hóa đơn";
            this.btnInHoaDon.UseVisualStyleBackColor = false;
            this.btnInHoaDon.Click += new System.EventHandler(this.btnInHoaDon_Click);
            // 
            // btnPhanHoi
            // 
            this.btnPhanHoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPhanHoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnPhanHoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPhanHoi.FlatAppearance.BorderSize = 0;
            this.btnPhanHoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPhanHoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPhanHoi.ForeColor = System.Drawing.Color.White;
            this.btnPhanHoi.Location = new System.Drawing.Point(920, 30);
            this.btnPhanHoi.Name = "btnPhanHoi";
            this.btnPhanHoi.Size = new System.Drawing.Size(150, 40);
            this.btnPhanHoi.TabIndex = 3;
            this.btnPhanHoi.Text = "💬 Phản hồi";
            this.btnPhanHoi.UseVisualStyleBackColor = false;
            this.btnPhanHoi.Click += new System.EventHandler(this.btnPhanHoi_Click);
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.AutoSize = true;
            this.lblStatusBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            this.lblStatusBadge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblStatusBadge.Location = new System.Drawing.Point(20, 65);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.lblStatusBadge.Size = new System.Drawing.Size(104, 26);
            this.lblStatusBadge.TabIndex = 2;
            this.lblStatusBadge.Text = "Đang xử lý";
            // 
            // lblMaDon
            // 
            this.lblMaDon.AutoSize = true;
            this.lblMaDon.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblMaDon.Location = new System.Drawing.Point(15, 25);
            this.lblMaDon.Name = "lblMaDon";
            this.lblMaDon.Size = new System.Drawing.Size(328, 41);
            this.lblMaDon.TabIndex = 1;
            this.lblMaDon.Text = "Đơn hàng DH2026046";
            // 
            // lblBack
            // 
            this.lblBack.AutoSize = true;
            this.lblBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBack.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBack.ForeColor = System.Drawing.Color.Gray;
            this.lblBack.Location = new System.Drawing.Point(20, 5);
            this.lblBack.Name = "lblBack";
            this.lblBack.Size = new System.Drawing.Size(150, 20);
            this.lblBack.TabIndex = 0;
            this.lblBack.Text = "← Quay lại danh sách";
            this.lblBack.Click += new System.EventHandler(this.lblBack_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.ColumnCount = 2;
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.pnlMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.pnlMain.Controls.Add(this.pnlLeft, 0, 0);
            this.pnlMain.Controls.Add(this.pnlRight, 1, 0);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 100);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RowCount = 1;
            this.pnlMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMain.Size = new System.Drawing.Size(1100, 700);
            this.pnlMain.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.grpSanPham);
            this.pnlLeft.Controls.Add(this.grpKhachHang);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(10, 10);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(10);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(695, 680);
            this.pnlLeft.TabIndex = 0;
            // 
            // grpSanPham
            // 
            this.grpSanPham.Controls.Add(this.dgvChiTiet);
            this.grpSanPham.Controls.Add(this.pnlSummary);
            this.grpSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSanPham.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpSanPham.Location = new System.Drawing.Point(0, 220);
            this.grpSanPham.Name = "grpSanPham";
            this.grpSanPham.Size = new System.Drawing.Size(695, 460);
            this.grpSanPham.TabIndex = 1;
            this.grpSanPham.TabStop = false;
            this.grpSanPham.Text = "🌸 Sản phẩm đặt";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AllowUserToAddRows = false;
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.BackgroundColor = System.Drawing.Color.White;
            this.dgvChiTiet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(242)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvChiTiet.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvChiTiet.Location = new System.Drawing.Point(3, 26);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersVisible = false;
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 35;
            this.dgvChiTiet.Size = new System.Drawing.Size(689, 351);
            this.dgvChiTiet.TabIndex = 0;
            // 
            // pnlSummary
            // 
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.pnlSummary.Controls.Add(this.lblTongTien);
            this.pnlSummary.Controls.Add(this.label6);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSummary.Location = new System.Drawing.Point(3, 377);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(689, 80);
            this.pnlSummary.TabIndex = 1;
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblTongTien.Location = new System.Drawing.Point(450, 20);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(137, 37);
            this.lblTongTien.TabIndex = 1;
            this.lblTongTien.Text = "600,000đ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(20, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 28);
            this.label6.TabIndex = 0;
            this.label6.Text = "Tổng cộng";
            // 
            // grpKhachHang
            // 
            this.grpKhachHang.AutoSize = true;
            this.grpKhachHang.Controls.Add(this.flpKhachHang);
            this.grpKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpKhachHang.Location = new System.Drawing.Point(0, 0);
            this.grpKhachHang.Name = "grpKhachHang";
            this.grpKhachHang.Size = new System.Drawing.Size(695, 220);
            this.grpKhachHang.TabIndex = 0;
            this.grpKhachHang.TabStop = false;
            this.grpKhachHang.Text = "👤 Thông tin khách hàng";
            // 
            // flpKhachHang
            // 
            this.flpKhachHang.AutoSize = true;
            this.flpKhachHang.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flpKhachHang.Controls.Add(this.lblTenKH);
            this.flpKhachHang.Controls.Add(this.lblSDT);
            this.flpKhachHang.Controls.Add(this.lblNguoiNhan);
            this.flpKhachHang.Controls.Add(this.lblHinhThuc);
            this.flpKhachHang.Controls.Add(this.lblDiaChi);
            this.flpKhachHang.Controls.Add(this.lblGhiChu);
            this.flpKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpKhachHang.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpKhachHang.Location = new System.Drawing.Point(3, 26);
            this.flpKhachHang.Name = "flpKhachHang";
            this.flpKhachHang.Padding = new System.Windows.Forms.Padding(12, 10, 12, 15);
            this.flpKhachHang.Size = new System.Drawing.Size(689, 191);
            this.flpKhachHang.TabIndex = 0;
            this.flpKhachHang.WrapContents = false;
            // 
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTenKH.Location = new System.Drawing.Point(15, 10);
            this.lblTenKH.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(151, 28);
            this.lblTenKH.TabIndex = 0;
            this.lblTenKH.Text = "Trần Văn Hùng";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSDT.Location = new System.Drawing.Point(15, 43);
            this.lblSDT.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(149, 23);
            this.lblSDT.TabIndex = 1;
            this.lblSDT.Text = "SĐT: 0912 345 678";
            // 
            // lblNguoiNhan
            // 
            this.lblNguoiNhan.AutoSize = true;
            this.lblNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblNguoiNhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.lblNguoiNhan.Location = new System.Drawing.Point(15, 76);
            this.lblNguoiNhan.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblNguoiNhan.Name = "lblNguoiNhan";
            this.lblNguoiNhan.Size = new System.Drawing.Size(117, 23);
            this.lblNguoiNhan.TabIndex = 2;
            this.lblNguoiNhan.Text = "Người nhận: ";
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHinhThuc.Location = new System.Drawing.Point(15, 109);
            this.lblHinhThuc.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(188, 23);
            this.lblHinhThuc.TabIndex = 3;
            this.lblHinhThuc.Text = "Hình thức: Giao tận nơi";
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiaChi.Location = new System.Drawing.Point(15, 142);
            this.lblDiaChi.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(220, 23);
            this.lblDiaChi.TabIndex = 4;
            this.lblDiaChi.Text = "Địa chỉ: 45 Lê Lợi, Q.1, HCM";
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChu.Location = new System.Drawing.Point(15, 175);
            this.lblGhiChu.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(243, 23);
            this.lblGhiChu.TabIndex = 5;
            this.lblGhiChu.Text = "Ghi chú: Gói quà đẹp, có thiệp";
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpAction);
            this.pnlRight.Controls.Add(this.grpTimeline);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(725, 10);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(10);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(365, 680);
            this.pnlRight.TabIndex = 1;
            // 
            // grpAction
            // 
            this.grpAction.Controls.Add(this.btnUpdateStatus);
            this.grpAction.Controls.Add(this.txtNote);
            this.grpAction.Controls.Add(this.label8);
            this.grpAction.Controls.Add(this.cboStatus);
            this.grpAction.Controls.Add(this.label7);
            this.grpAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpAction.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpAction.Location = new System.Drawing.Point(0, 350);
            this.grpAction.Name = "grpAction";
            this.grpAction.Size = new System.Drawing.Size(365, 280);
            this.grpAction.TabIndex = 1;
            this.grpAction.TabStop = false;
            this.grpAction.Text = "🔄 Cập nhật trạng thái";
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(57)))), ((int)(((byte)(77)))));
            this.btnUpdateStatus.FlatAppearance.BorderSize = 0;
            this.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.Location = new System.Drawing.Point(15, 220);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(340, 40);
            this.btnUpdateStatus.TabIndex = 4;
            this.btnUpdateStatus.Text = "Cập nhật trạng thái";
            this.btnUpdateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNote.Location = new System.Drawing.Point(15, 120);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(340, 80);
            this.txtNote.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label8.Location = new System.Drawing.Point(15, 95);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 20);
            this.label8.TabIndex = 2;
            this.label8.Text = "Ghi chú cập nhật:";
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(15, 60);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(340, 31);
            this.cboStatus.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.Location = new System.Drawing.Point(15, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(163, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Chuyển sang trạng thái:";
            // 
            // grpTimeline
            // 
            this.grpTimeline.Controls.Add(this.pnlTimeline);
            this.grpTimeline.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpTimeline.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.grpTimeline.Location = new System.Drawing.Point(0, 0);
            this.grpTimeline.Name = "grpTimeline";
            this.grpTimeline.Size = new System.Drawing.Size(365, 350);
            this.grpTimeline.TabIndex = 0;
            this.grpTimeline.TabStop = false;
            this.grpTimeline.Text = "🚀 Tiến trình xử lý";
            // 
            // pnlTimeline
            // 
            this.pnlTimeline.AutoScroll = true;
            this.pnlTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTimeline.Location = new System.Drawing.Point(3, 26);
            this.pnlTimeline.Name = "pnlTimeline";
            this.pnlTimeline.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTimeline.Size = new System.Drawing.Size(359, 321);
            this.pnlTimeline.TabIndex = 0;
            // 
            // ucChiTietDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(245)))), ((int)(((byte)(246)))));
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Name = "ucChiTietDonHang";
            this.Size = new System.Drawing.Size(1100, 800);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.grpSanPham.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.grpKhachHang.ResumeLayout(false);
            this.grpKhachHang.PerformLayout();
            this.flpKhachHang.ResumeLayout(false);
            this.flpKhachHang.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.grpAction.ResumeLayout(false);
            this.grpAction.PerformLayout();
            this.grpTimeline.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblMaDon;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.Label lblStatusBadge;
        private System.Windows.Forms.Button btnInHoaDon;
        private System.Windows.Forms.Button btnPhanHoi;
        private System.Windows.Forms.TableLayoutPanel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.GroupBox grpKhachHang;
        private System.Windows.Forms.FlowLayoutPanel flpKhachHang;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblNguoiNhan;
        private System.Windows.Forms.Label lblHinhThuc;
        private System.Windows.Forms.Label lblDiaChi;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.GroupBox grpSanPham;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.Panel pnlSummary;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpTimeline;
        private System.Windows.Forms.Panel pnlTimeline;
        private System.Windows.Forms.GroupBox grpAction;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label7;
    }
}
