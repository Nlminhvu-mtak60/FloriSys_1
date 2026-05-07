using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FloriSys.DataAccess;
using FloriSys.Models;
using FloriSys.Services;
using FloriSys.Shared;

namespace FloriSys._5_GiaoHang
{
    public partial class ucCapNhatGH : BaseUserControl
    {
        private readonly GiaoHangRepository _ghRepo = new GiaoHangRepository();
        private List<GiaoHang> dsDonGiao;

        public ucCapNhatGH()
        {
            InitializeComponent();
            this.Load += ucCapNhatGH_Load;
            btnGiaoXong1.Click += btnGiaoXong1_Click;
            btnVang1.Click += btnVang1_Click;
            btnHoan1.Click += btnHoan1_Click;
            button1.Click += btnBatDau2_Click;
        }

        private void ucCapNhatGH_Load(object sender, EventArgs e)
        {
            lblSub.Text = DateTime.Now.ToString("dd/MM/yyyy");
            LoadData();
        }

        public override void LoadData()
        {
            try
            {
                dsDonGiao = _ghRepo.LayDonCuaShipper(SessionManager.MaNV);
                if (dsDonGiao == null || dsDonGiao.Count == 0)
                {
                    pnlDon1.Visible = false;
                    panel2.Visible = false;
                    return;
                }

                GiaoHang dangGiao = null;
                GiaoHang choPhanCong = null;
                foreach (GiaoHang gh in dsDonGiao)
                {
                    if (gh.TrangThai == "DangGiao" && dangGiao == null) dangGiao = gh;
                    if ((gh.TrangThai == "ChoPhanCong" || gh.TrangThai == "GiaoLai") && choPhanCong == null) choPhanCong = gh;
                }

                if (dangGiao != null)
                {
                    pnlDon1.Visible = true;
                    pnlDon1.Tag = dangGiao.MaGiaoHang;
                    lblMaDon1.Text = dangGiao.MaDon + " – " + dangGiao.TenKH;
                    lblInfo1.Text = "📍 " + (dangGiao.DiaChi ?? "—") + "   📞 " + (dangGiao.SoDienThoai ?? "—");
                    lblTien1.Text = string.Format("💰 {0:N0}đ – COD", dangGiao.TongTien);
                    label1.Text = !string.IsNullOrEmpty(dangGiao.GhiChuDon) ? "📝 " + dangGiao.GhiChuDon : "";
                    label2.Text = !string.IsNullOrEmpty(dangGiao.GhiChuGiaoHang) ? "🚚 " + dangGiao.GhiChuGiaoHang : "";
                }
                else { pnlDon1.Visible = false; }

                if (choPhanCong != null)
                {
                    panel2.Visible = true;
                    panel2.Tag = choPhanCong.MaGiaoHang;
                    label7.Text = choPhanCong.MaDon + " – " + choPhanCong.TenKH;
                    label6.Text = "📍 " + (choPhanCong.DiaChi ?? "—");
                    label5.Text = "⏰ Chờ giao";
                    label4.Text = string.Format("💰 {0:N0}đ", choPhanCong.TongTien);
                    label3.Text = "📞 " + (choPhanCong.SoDienThoai ?? "—");
                }
                else { panel2.Visible = false; }
            }
            catch (Exception ex) { ShowError("Lỗi tải dữ liệu: " + ex.Message); }
        }

        private void btnGiaoXong1_Click(object sender, EventArgs e)
        {
            string maGH = pnlDon1.Tag?.ToString();
            if (string.IsNullOrEmpty(maGH)) return;
            if (Confirm("Xác nhận đơn này đã giao thành công?"))
            {
                try { _ghRepo.CapNhatTrangThai(maGH, "GiaoThanhCong"); ShowSuccess("Đã giao thành công!"); LoadData(); }
                catch (Exception ex) { ShowError(ex.Message); }
            }
        }

        private void btnVang1_Click(object sender, EventArgs e)
        {
            string maGH = pnlDon1.Tag?.ToString();
            if (string.IsNullOrEmpty(maGH)) return;
            if (Confirm("Ghi nhận giao lại (khách vắng)?"))
            {
                try { _ghRepo.CapNhatTrangThai(maGH, "GiaoLai", "Khách vắng"); ShowSuccess("Đã ghi nhận giao lại."); LoadData(); }
                catch (Exception ex) { ShowError(ex.Message); }
            }
        }

        private void btnHoan1_Click(object sender, EventArgs e)
        {
            string maGH = pnlDon1.Tag?.ToString();
            if (string.IsNullOrEmpty(maGH)) return;
            if (Confirm("Xác nhận HOÀN HÀNG đơn này?"))
            {
                try { _ghRepo.CapNhatTrangThai(maGH, "HoanHang", "Shipper hoàn hàng"); ShowSuccess("Đã hoàn hàng."); LoadData(); }
                catch (Exception ex) { ShowError(ex.Message); }
            }
        }

        private void btnBatDau2_Click(object sender, EventArgs e)
        {
            string maGH = panel2.Tag?.ToString();
            if (string.IsNullOrEmpty(maGH)) return;
            if (Confirm("Bắt đầu giao đơn này?"))
            {
                try { _ghRepo.CapNhatTrangThai(maGH, "DangGiao"); ShowSuccess("Đã bắt đầu giao!"); LoadData(); }
                catch (Exception ex) { ShowError(ex.Message); }
            }
        }
    }
}
