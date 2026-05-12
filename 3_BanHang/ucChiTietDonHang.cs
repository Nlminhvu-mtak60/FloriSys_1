using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;

namespace FloriSys._3_BanHang
{
    public partial class ucChiTietDonHang : BaseUserControl
    {
        private readonly DonHangRepository _dhRepo = new DonHangRepository();
        private string _maDon;

        public ucChiTietDonHang()
        {
            InitializeComponent();
            LoadStatusList();
        }

        private void LoadStatusList()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.AddRange(new object[] { "Moi", "DangXuLy", "DaGiao", "HoanThanh", "Huy" });
        }

        public void SetMaDon(string maDon)
        {
            _maDon = maDon;
            LoadData();
        }

        public override void LoadData()
        {
            LoadInfo();
            LoadItems();
            LoadTimeline();
        }

        private void LoadInfo()
        {
            try
            {
                DonHang dh = _dhRepo.LayThongTinDon(_maDon);
                if (dh != null)
                {
                    lblMaDon.Text = "Đơn hàng " + _maDon;
                    lblTenKH.Text = dh.TenKH;
                    lblSDT.Text = "SĐT: " + dh.SoDienThoai;
                    lblHinhThuc.Text = "Hình thức: " + dh.HinhThucNhanHang;
                    lblDiaChi.Text = "Địa chỉ: " + dh.DiaChi;
                    lblGhiChu.Text = "Ghi chú: " + dh.GhiChu;
                    lblTongTien.Text = string.Format("{0:#,##0}đ", dh.TongTien);
                    
                    lblStatusBadge.Text = dh.TrangThai;
                    cboStatus.SelectedItem = dh.TrangThai;
                    cboStatus.Enabled = false;
                    btnUpdateStatus.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải thông tin đơn: " + ex.Message);
            }
        }

        private void LoadItems()
        {
            try
            {
                List<ChiTietDonHang> dsCT = _dhRepo.LayChiTiet(_maDon);
                dgvChiTiet.DataSource = dsCT;
                FormatGrid();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi tải chi tiết sản phẩm: " + ex.Message);
            }
        }

        public override void FormatGrid()
        {
            if (dgvChiTiet.Columns.Count == 0) return;

            var visibleCols = new List<string> { "MaSP", "TenSP", "SoLuong", "DonGia", "ThanhTien" };
            foreach (DataGridViewColumn col in dgvChiTiet.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

            dgvChiTiet.Columns["MaSP"].HeaderText = "Mã SP";
            dgvChiTiet.Columns["TenSP"].HeaderText = "Tên sản phẩm";
            dgvChiTiet.Columns["SoLuong"].HeaderText = "Số lượng";
            dgvChiTiet.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvChiTiet.Columns["ThanhTien"].HeaderText = "Thành tiền";

            dgvChiTiet.Columns["DonGia"].DefaultCellStyle.Format = "#,##0";
            dgvChiTiet.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0";
            
            dgvChiTiet.ReadOnly = true;
            dgvChiTiet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChiTiet.MultiSelect = false;
        }

        private void LoadTimeline()
        {
            pnlTimeline.Controls.Clear();
            try
            {
                List<LichSuDonHang> dsLS = _dhRepo.LayLichSuDonHang(_maDon);

                if (dsLS == null || dsLS.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        Text = "Chưa có lịch sử xử lý.",
                        AutoSize = true,
                        ForeColor = System.Drawing.Color.FromArgb(156, 163, 175),
                        Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Italic),
                        Location = new System.Drawing.Point(15, 15)
                    };
                    pnlTimeline.Controls.Add(lblEmpty);
                    return;
                }

                int y = 10;
                for (int i = 0; i < dsLS.Count; i++)
                {
                    LichSuDonHang ls = dsLS[i];
                    bool isLast = (i == dsLS.Count - 1);

                    // Icon dựa theo trạng thái
                    string icon;
                    System.Drawing.Color iconColor;
                    switch (ls.TrangThai)
                    {
                        case "Moi":       icon = "🆕"; iconColor = System.Drawing.Color.FromArgb(59, 130, 246); break;
                        case "DangXuLy":  icon = "📦"; iconColor = System.Drawing.Color.FromArgb(245, 158, 11); break;
                        case "DaGiao":    icon = "🚚"; iconColor = System.Drawing.Color.FromArgb(16, 185, 129); break;
                        case "HoanThanh": icon = "✅"; iconColor = System.Drawing.Color.FromArgb(34, 197, 94); break;
                        case "Huy":       icon = "❌"; iconColor = System.Drawing.Color.FromArgb(239, 68, 68); break;
                        case "HoanHang":  icon = "↩️"; iconColor = System.Drawing.Color.FromArgb(168, 85, 247); break;
                        default:          icon = "●";  iconColor = System.Drawing.Color.Gray; break;
                    }

                    // Tên trạng thái hiển thị
                    string statusName;
                    switch (ls.TrangThai)
                    {
                        case "Moi":       statusName = "Đơn hàng mới"; break;
                        case "DangXuLy":  statusName = "Đang xử lý"; break;
                        case "DaGiao":    statusName = "Đã giao shipper"; break;
                        case "HoanThanh": statusName = "Hoàn thành"; break;
                        case "Huy":       statusName = "Đã hủy"; break;
                        case "HoanHang":  statusName = "Hoàn hàng"; break;
                        default:          statusName = ls.TrangThai; break;
                    }

                    // Dòng thời gian + trạng thái
                    string timeStr = ls.ThoiGian.ToString("dd/MM/yyyy HH:mm");
                    Label lblEntry = new Label
                    {
                        Text = $"{icon}  {timeStr} — {statusName}",
                        AutoSize = true,
                        Font = new System.Drawing.Font("Segoe UI", 10f, isLast ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular),
                        ForeColor = isLast ? iconColor : System.Drawing.Color.FromArgb(55, 65, 81),
                        Location = new System.Drawing.Point(15, y)
                    };
                    pnlTimeline.Controls.Add(lblEntry);
                    y += 24;

                    // Ghi chú (nếu có)
                    if (!string.IsNullOrWhiteSpace(ls.GhiChu))
                    {
                        Label lblNote = new Label
                        {
                            Text = "     " + ls.GhiChu,
                            AutoSize = true,
                            Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Italic),
                            ForeColor = System.Drawing.Color.FromArgb(156, 163, 175),
                            Location = new System.Drawing.Point(15, y)
                        };
                        pnlTimeline.Controls.Add(lblNote);
                        y += 20;
                    }
                    y += 4; // spacing between entries
                }
            }
            catch
            {
                // Bảng LICH_SU_DON_HANG chưa tồn tại → hiển thị fallback
                Label lblFallback = new Label
                {
                    Text = "⚠ Chưa có dữ liệu lịch sử.\nVui lòng chạy script SQL_LichSuDonHang.sql",
                    AutoSize = true,
                    ForeColor = System.Drawing.Color.FromArgb(202, 138, 4),
                    Font = new System.Drawing.Font("Segoe UI", 9.5f),
                    Location = new System.Drawing.Point(15, 15)
                };
                pnlTimeline.Controls.Add(lblFallback);
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (cboStatus.SelectedItem == null) return;
            try
            {
                string newStatus = cboStatus.SelectedItem.ToString();
                _dhRepo.CapNhatTrangThai(_maDon, newStatus);
                ShowSuccess("Cập nhật trạng thái thành công!");
                LoadInfo();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi cập nhật: " + ex.Message);
            }
        }

        private void btnPhanHoi_Click(object sender, EventArgs e)
        {
            var frmMain = this.FindForm() as FloriSys._2_QuanLy.frmMain;
            if (frmMain != null)
            {
                frmMain.OnMenuClicked("PhanHoi", _maDon);
            }
        }

        private void lblBack_Click(object sender, EventArgs e)
        {
            var frmMain = this.FindForm() as FloriSys._2_QuanLy.frmMain;
            if (frmMain != null)
            {
                frmMain.OnMenuClicked("DanhSachDon");
            }
        }
    }
}
