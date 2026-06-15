using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Shared;
using FloriSys.Services;

namespace FloriSys._3_BanHang
{
    public partial class ucChiTietDonHang : BaseUserControl
    {
        private readonly DonHangRepository _dhRepo = new DonHangRepository();
        private string _maDon;

        public ucChiTietDonHang()
        {
            InitializeComponent();
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
                    lblTenKH.Text = "Người đặt: " + dh.TenKH;
                    lblSDT.Text = "SĐT: " + dh.SoDienThoai;
                    
                    var receiverInfo = OrderParser.ParseReceiverInfo(dh.GhiChu, dh.DiaChi);
                    string ghiChu = receiverInfo.GhiChuRutGon;
                    string tenNhan = receiverInfo.TenNhan;
                    string sdtNhan = receiverInfo.SdtNhan;
                    string diaChiNhan = receiverInfo.DiaChiNhan;

                    // Remove dynamically added label if any
                    var oldLabels = grpKhachHang.Controls.OfType<Label>().Where(l => l.Name == "lblNguoiNhanDynamic").ToList();
                    foreach (var l in oldLabels) grpKhachHang.Controls.Remove(l);

                    int currentY = 110; // Default Y for Hình thức

                    if (!string.IsNullOrEmpty(tenNhan))
                    {
                        Label lblNguoiNhan = new Label();
                        lblNguoiNhan.Name = "lblNguoiNhanDynamic";
                        lblNguoiNhan.AutoSize = true;
                        lblNguoiNhan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
                        lblNguoiNhan.ForeColor = System.Drawing.Color.FromArgb(232, 57, 77); // Red-ish highlight for receiver
                        lblNguoiNhan.Location = new System.Drawing.Point(15, currentY);
                        lblNguoiNhan.Text = "Người nhận: " + tenNhan + " — SĐT: " + sdtNhan;
                        grpKhachHang.Controls.Add(lblNguoiNhan);
                        currentY += 35;
                    }

                    lblHinhThuc.Location = new System.Drawing.Point(15, currentY);
                    lblHinhThuc.Text = "Hình thức: " + dh.HinhThucDisplay;
                    currentY += 35;

                    lblDiaChi.Location = new System.Drawing.Point(15, currentY);
                    lblDiaChi.Text = "Địa chỉ: " + diaChiNhan;
                    currentY += 35;

                    lblGhiChu.Location = new System.Drawing.Point(15, currentY);
                    lblGhiChu.Text = "Ghi chú: " + ghiChu;
                    currentY += 40;

                    grpKhachHang.Height = currentY;

                    lblTongTien.Text = string.Format("{0:#,##0}đ", dh.TongTien);
                    
                    lblStatusBadge.Text = dh.TrangThai;
                    
                    cboStatus.Items.Clear();
                    if (dh.TrangThai == "HoanThanh" || dh.TrangThai == "Huy" || dh.TrangThai == "HoanHang")
                    {
                        cboStatus.Enabled = false;
                        btnUpdateStatus.Visible = false;
                        cboStatus.Items.Add(dh.TrangThai);
                        cboStatus.SelectedIndex = 0;
                    }
                    else
                    {
                        cboStatus.Enabled = true;
                        btnUpdateStatus.Visible = true;
                        
                        cboStatus.Items.Add(dh.TrangThai);
                        if (dh.TrangThai == "Moi")
                        {
                            cboStatus.Items.Add("DangXuLy");
                            cboStatus.Items.Add("Huy");
                        }
                        else if (dh.TrangThai == "DangXuLy")
                        {
                            if (!dh.IsGiaoTanNoi) {
                                cboStatus.Items.Add("HoanThanh");
                            }
                            cboStatus.Items.Add("Huy");
                        }
                        else if (dh.TrangThai == "DaGiao")
                        {
                            cboStatus.Items.Add("HoanThanh");
                        }
                        
                        cboStatus.SelectedItem = dh.TrangThai;
                    }
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
            var visibleCols = new List<string> { "MaSP", "TenSP", "SoLuong", "DonGia", "ThanhTien" };
            var headers = new Dictionary<string, string>
            {
                { "MaSP", "Mã SP" },
                { "TenSP", "Tên sản phẩm" },
                { "SoLuong", "Số lượng" },
                { "DonGia", "Đơn giá" },
                { "ThanhTien", "Thành tiền" }
            };
            var formats = new Dictionary<string, string>
            {
                { "DonGia", "#,##0" },
                { "ThanhTien", "#,##0" }
            };

            GridHelper.FormatGrid(dgvChiTiet, visibleCols, headers, formats);
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
