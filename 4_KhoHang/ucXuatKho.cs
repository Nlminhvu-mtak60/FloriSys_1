using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;
using FloriSys._2_QuanLy;

namespace FloriSys._4_KhoHang
{
    public partial class ucXuatKho : BaseUserControl
    {
        private readonly DonHangRepository _dhRepo = new DonHangRepository();

        public ucXuatKho()
        {
            InitializeComponent();
        }

        private void ucXuatKho_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                List<DonChoXuatKho> dsXK = _dhRepo.LayDonChoXuatKho();
                dgvXuatKho.DataSource = dsXK;
                FormatGrid();

                // Kiểm tra cảnh báo đơn hàng chờ lâu (> 30 phút)
                int donTre = 0;
                foreach (var item in dsXK)
                {
                    if ((DateTime.Now - item.NgayTao).TotalMinutes >= 30)
                    {
                        donTre++;
                    }
                }

                if (donTre > 0)
                {
                    lblAlert.Text = $"⚠️ CẢNH BÁO TỒN ĐỌNG! Hiện đang có {donTre} đơn hàng chờ xuất kho quá 30 phút! Vui lòng kiểm tra và ưu tiên xử lý ngay.";
                    pnlAlert.Visible = true;
                    dgvXuatKho.BringToFront();
                }
                else
                {
                    pnlAlert.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải danh sách xuất kho: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvXuatKho.Columns.Count == 0) return;

            var visibleCols = new List<string> { "MaDon", "TenKH", "TenSP", "SoLuong", "TinhTrangKho", "HinhThucNhanHang", "ThoiGianCho", "btnXuat", "btnHuy" };

            if (!dgvXuatKho.Columns.Contains("ThoiGianCho"))
            {
                DataGridViewTextBoxColumn colWait = new DataGridViewTextBoxColumn();
                colWait.Name = "ThoiGianCho";
                colWait.HeaderText = "Thời gian chờ";
                colWait.ReadOnly = true;
                colWait.DisplayIndex = 6; 
                dgvXuatKho.Columns.Add(colWait);
            }

            foreach (DataGridViewColumn col in dgvXuatKho.Columns) 
            { 
                if (!visibleCols.Contains(col.Name)) col.Visible = false; 
                else col.Visible = true;
            }

            dgvXuatKho.Columns["MaDon"].HeaderText = "Mã đơn";
            dgvXuatKho.Columns["TenKH"].HeaderText = "Khách hàng";
            dgvXuatKho.Columns["TenSP"].HeaderText = "Sản phẩm";
            dgvXuatKho.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvXuatKho.Columns["TinhTrangKho"].HeaderText = "Tình trạng";
            if (dgvXuatKho.Columns.Contains("HinhThucNhanHang"))
                dgvXuatKho.Columns["HinhThucNhanHang"].HeaderText = "Hình thức";
            dgvXuatKho.Columns["ThoiGianCho"].HeaderText = "Thời gian chờ";

            if (!dgvXuatKho.Columns.Contains("btnXuat"))
            {
                DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                btnCol.Name = "btnXuat";
                btnCol.HeaderText = "Thao tác";
                btnCol.Text = "Xác nhận xuất";
                btnCol.UseColumnTextForButtonValue = true;
                dgvXuatKho.Columns.Add(btnCol);
            }

            if (!dgvXuatKho.Columns.Contains("btnHuy"))
            {
                DataGridViewButtonColumn btnHuyCol = new DataGridViewButtonColumn();
                btnHuyCol.Name = "btnHuy";
                btnHuyCol.HeaderText = "Hủy";
                btnHuyCol.Text = "Hủy đơn";
                btnHuyCol.UseColumnTextForButtonValue = true;
                dgvXuatKho.Columns.Add(btnHuyCol);
            }
            dgvXuatKho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvXuatKho.CellFormatting -= DgvXuatKho_CellFormatting;
            dgvXuatKho.CellFormatting += DgvXuatKho_CellFormatting;
        }

        private void DgvXuatKho_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            string colName = dgvXuatKho.Columns[e.ColumnIndex].Name;

            DonChoXuatKho item = dgvXuatKho.Rows[e.RowIndex].DataBoundItem as DonChoXuatKho;
            if (item == null) return;

            if (colName == "ThoiGianCho")
            {
                TimeSpan diff = DateTime.Now - item.NgayTao;
                if (diff.TotalMinutes < 0) diff = TimeSpan.Zero;

                if (diff.TotalDays >= 1)
                {
                    e.Value = $"{(int)diff.TotalDays} ngày {diff.Hours} giờ";
                }
                else if (diff.TotalHours >= 1)
                {
                    e.Value = $"{diff.Hours} giờ {diff.Minutes} phút";
                }
                else
                {
                    e.Value = $"{(int)diff.TotalMinutes} phút";
                }
            }

            if (colName == "HinhThucNhanHang" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == "TaiQuay") { e.Value = "Tại quầy"; e.CellStyle.ForeColor = Color.FromArgb(30, 64, 175); }
                else if (val == "GiaoTanNoi") { e.Value = "Giao hàng"; e.CellStyle.ForeColor = Color.FromArgb(146, 64, 14); }
                e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            }
            else if (colName == "TinhTrangKho" && e.Value != null)
            {
                string val = e.Value.ToString();
                if (val == "DuHang") { e.Value = "Đủ hàng"; e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52); }
                else if (val == "KhongDu") { e.Value = "Không đủ"; e.CellStyle.ForeColor = Color.FromArgb(185, 28, 28); }
                e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            }

            TimeSpan totalWait = DateTime.Now - item.NgayTao;
            if (totalWait.TotalMinutes >= 30)
            {
                e.CellStyle.BackColor = Color.FromArgb(254, 226, 226); 
                e.CellStyle.ForeColor = (colName == "TinhTrangKho" || colName == "HinhThucNhanHang") ? e.CellStyle.ForeColor : Color.FromArgb(153, 27, 27); 
            }
        }

        private void dgvXuatKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            
            DonChoXuatKho item = dgvXuatKho.Rows[e.RowIndex].DataBoundItem as DonChoXuatKho;
            if (item == null) return;

            if (dgvXuatKho.Columns[e.ColumnIndex].Name == "btnXuat")
            {
                if (item.TinhTrangKho == "KhongDu")
                {
                    ShowWarning("Không thể xuất kho vì tồn kho không đủ cho một số sản phẩm trong đơn hàng!");
                    return;
                }

                if (Confirm($"Xác nhận xuất kho cho đơn {item.MaDon}?"))
                {
                    XacNhanXuat(item);
                }
            }
            else if (dgvXuatKho.Columns[e.ColumnIndex].Name == "btnHuy")
            {
                if (Confirm($"Bạn có chắc chắn muốn HỦY đơn {item.MaDon} do không đủ hàng?"))
                {
                    HuyDon(item.MaDon);
                }
            }
        }

        // FIX: Nhận luôn đối tượng DonChoXuatKho và thực hiện cập nhật atomic trong 1 lần DB call
        private void XacNhanXuat(DonChoXuatKho item)
        {
            try
            {
                if (item.HinhThucNhanHang == "TaiQuay")
                {
                    // Nếu là nhận tại quầy -> hoàn thành luôn
                    _dhRepo.CapNhatTrangThai(item.MaDon, "HoanThanh");
                    ShowSuccess($"Xuất kho thành công! Đơn {item.MaDon} (nhận tại quầy) đã hoàn thành.");
                }
                else
                {
                    // Nếu cần giao hàng -> chuyển sang chờ phân công / đang xử lý
                    _dhRepo.CapNhatTrangThai(item.MaDon, "DangXuLy");
                    ShowSuccess($"Xuất kho thành công! Đơn {item.MaDon} chờ giao hàng.");
                }
                
                LoadData();
                var mainForm = this.FindForm() as frmMain;
                if (mainForm != null)
                {
                    mainForm.RefreshMenuBadges();
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi xác nhận xuất kho: " + ex.Message);
            }
        }

        private void HuyDon(string maDon)
        {
            try
            {
                _dhRepo.CapNhatTrangThai(maDon, "Huy");
                ShowSuccess("Đã hủy đơn hàng thành công.");
                LoadData();
                var mainForm = this.FindForm() as frmMain;
                if (mainForm != null)
                {
                    mainForm.RefreshMenuBadges();
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi hủy đơn: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}