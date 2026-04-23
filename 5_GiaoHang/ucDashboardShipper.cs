using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys._5_GiaoHang
{
    public partial class ucDashboardShipper : UserControl
    {
        private string currentUserMaNV = FloriSys.Services.SessionManager.MaNV;
        private string currentMaGH = "";

        public ucDashboardShipper()
        {
            InitializeComponent();
        }

        private void ucDashboardShipper_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            LoadStats();
            LoadList();
            LoadCurrentDelivery();
        }

        private void LoadStats()
        {
            ThongKeShipper stats = GiaoHangDAO.ThongKeShipper(currentUserMaNV);
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
            List<GiaoHang> dsGH = GiaoHangDAO.LayDonCuaShipper(currentUserMaNV);
            dgvAllDon.DataSource = dsGH;
            if (dgvAllDon.Columns.Count > 0)
            {
                dgvAllDon.Columns["MaGiaoHang"].Visible = false;
                dgvAllDon.Columns["MaDon"].HeaderText = "Mã đơn";
                dgvAllDon.Columns["TenKH"].HeaderText = "Khách hàng";
                dgvAllDon.Columns["DiaChi"].HeaderText = "Địa chỉ";
                dgvAllDon.Columns["SoDienThoai"].HeaderText = "SĐT";
                dgvAllDon.Columns["TrangThai"].HeaderText = "Trạng thái";
                dgvAllDon.Columns["TongTien"].HeaderText = "Tổng tiền";
                dgvAllDon.Columns["TongTien"].DefaultCellStyle.Format = "N0";
            }
            
            dgvAllDon.ReadOnly = true;
            dgvAllDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAllDon.MultiSelect = false;
            dgvAllDon.AllowUserToResizeRows = false;
        }

        private void LoadCurrentDelivery()
        {
            List<GiaoHang> dsGH = dgvAllDon.DataSource as List<GiaoHang>;
            GiaoHang current = null;
            if (dsGH != null)
            {
                // Prioritize the specifically selected order
                if (!string.IsNullOrEmpty(currentMaGH))
                {
                    foreach (GiaoHang gh in dsGH)
                    {
                        if (gh.TrangThai == "DangGiao" && gh.MaGiaoHang == currentMaGH)
                        {
                            current = gh;
                            break;
                        }
                    }
                }

                // Fallback to the first available DangGiao
                if (current == null)
                {
                    foreach (GiaoHang gh in dsGH)
                    {
                        if (gh.TrangThai == "DangGiao")
                        {
                            current = gh;
                            break;
                        }
                    }
                }
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
            else
            {
                pnlCurrent.Visible = false;
            }
        }

        private void btnThanhCong_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            GiaoHangDAO.CapNhatTrangThai(currentMaGH, "GiaoThanhCong");
            MessageBox.Show("Đã cập nhật giao hàng thành công!");
            LoadData();
        }

        private void btnKhachVang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            GiaoHangDAO.CapNhatTrangThai(currentMaGH, "GiaoLai");
            MessageBox.Show("Ghi nhận khách vắng mặt.");
            LoadData();
        }

        private void btnHoanHang_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaGH)) return;
            GiaoHangDAO.CapNhatTrangThai(currentMaGH, "HoanHang");
            MessageBox.Show("Đã ghi nhận hoàn hàng.");
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
                    if (MessageBox.Show("Bạn muốn bắt đầu giao đơn này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        GiaoHangDAO.CapNhatTrangThai(gh.MaGiaoHang, "DangGiao");
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
