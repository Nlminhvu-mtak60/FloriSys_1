using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Services;
using FloriSys.Shared;

namespace FloriSys._5_GiaoHang
{
    public partial class ucDashboardShipper : BaseUserControl
    {
        private readonly GiaoHangRepository _ghRepo = new GiaoHangRepository();
        private string currentUserMaNV = SessionManager.MaNV;
        private string currentMaGH = "";

        public ucDashboardShipper() { InitializeComponent(); }

        private void ucDashboardShipper_Load(object sender, EventArgs e) { LoadData(); }

        public override void LoadData()
        {
            LoadStats();
            LoadList();
            LoadCurrentDelivery();
        }

        private void LoadStats()
        {
            ThongKeShipper stats = _ghRepo.ThongKe(currentUserMaNV);
            if (stats != null)
            {
                lblValTongDon.Text = stats.TongDonHnay.ToString();
                lblValDaGiao.Text = stats.DaGiaoHnay.ToString();
                lblValDangGiao.Text = stats.DangDiGiao.ToString();
                lblValChuaGiao.Text = stats.ChuaGiao.ToString();
            }
        }

        private void LoadList()
        {
            List<GiaoHang> dsGH = _ghRepo.LayDonCuaShipper(currentUserMaNV);
            dgvAllDon.DataSource = null; // Reset to ensure columns are refreshed if needed
            dgvAllDon.DataSource = dsGH;
            
            if (dgvAllDon.Columns.Count > 0)
            {
                var visibleCols = new List<string> { "MaDon", "TenKH", "DiaChi", "SoDienThoai", "TrangThaiDisplay", "TongTien", "GhiChuDon", "GhiChuGiaoHang" };
                foreach (DataGridViewColumn col in dgvAllDon.Columns) { if (!visibleCols.Contains(col.Name)) col.Visible = false; }

                // Configure visible columns
                dgvAllDon.Columns["MaDon"].HeaderText = "Mã đơn";
                dgvAllDon.Columns["TenKH"].HeaderText = "Khách hàng";
                dgvAllDon.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvAllDon.Columns["SoDienThoai"].HeaderText = "SĐT";
                
                if (dgvAllDon.Columns.Contains("TrangThaiDisplay"))
                {
                    dgvAllDon.Columns["TrangThaiDisplay"].HeaderText = "Trạng thái";
                    dgvAllDon.Columns["TrangThaiDisplay"].DisplayIndex = 4; // Position after SĐT
                }

                dgvAllDon.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvAllDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
                
                if (dgvAllDon.Columns.Contains("GhiChuDon"))
                    dgvAllDon.Columns["GhiChuDon"].HeaderText = "Ghi chú đơn";

                if (dgvAllDon.Columns.Contains("GhiChuGiaoHang"))
                    dgvAllDon.Columns["GhiChuGiaoHang"].HeaderText = "Ghi chú giao";
            }
            dgvAllDon.ReadOnly = true;
            dgvAllDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAllDon.MultiSelect = false;
        }

        private void LoadCurrentDelivery()
        {
            List<GiaoHang> dsGH = dgvAllDon.DataSource as List<GiaoHang>;
            GiaoHang current = null;
            if (dsGH != null)
            {
                if (!string.IsNullOrEmpty(currentMaGH))
                    foreach (GiaoHang gh in dsGH)
                        if (gh.TrangThai == "DangGiao" && gh.MaGiaoHang == currentMaGH) { current = gh; break; }
                if (current == null)
                    foreach (GiaoHang gh in dsGH)
                        if (gh.TrangThai == "DangGiao") { current = gh; break; }
            }

            if (current != null)
            {
                pnlCurrent.Visible = true;
                currentMaGH = current.MaGiaoHang;
                lblCurTitle.Text = "🔴 Đơn đang giao – " + current.MaDon;
                lblCurCustomer.Text = "Khách hàng: " + current.TenKH;
                lblCurPhone.Text = "📞 SĐT: " + current.SoDienThoai;
                lblCurAddress.Text = "📍 Địa chỉ: " + current.DiaChi;
            }
            else { pnlCurrent.Visible = false; }
        }

        private void btnThanhCong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            _ghRepo.CapNhatTrangThai(currentMaGH, "GiaoThanhCong");
            ShowSuccess("Đã cập nhật giao hàng thành công!");
            LoadData();
        }

        private void btnKhachVang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            _ghRepo.CapNhatTrangThai(currentMaGH, "GiaoLai");
            ShowSuccess("Ghi nhận khách vắng mặt.");
            LoadData();
        }

        private void btnHoanHang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            _ghRepo.CapNhatTrangThai(currentMaGH, "HoanHang");
            ShowSuccess("Đã ghi nhận hoàn hàng.");
            LoadData();
        }

        private void dgvAllDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                GiaoHang gh = dgvAllDon.Rows[e.RowIndex].DataBoundItem as GiaoHang;
                if (gh == null) return;
                if (gh.TrangThai == "ChoPhanCong" || gh.TrangThai == "GiaoLai")
                {
                    if (Confirm("Bạn muốn bắt đầu giao đơn này?"))
                    {
                        _ghRepo.CapNhatTrangThai(gh.MaGiaoHang, "DangGiao");
                        currentMaGH = gh.MaGiaoHang;
                        LoadData();
                    }
                }
                else if (gh.TrangThai == "DangGiao")
                {
                    currentMaGH = gh.MaGiaoHang;
                    LoadCurrentDelivery();
                }
            }
        }
    }
}
