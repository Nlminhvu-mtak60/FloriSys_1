using System;
using System.Data;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Order service - encapsulates complex order creation and status management.
    /// Demonstrates: ENCAPSULATION (multi-step order creation hidden from UI),
    /// ABSTRACTION (UI only calls TaoDonHang, doesn't know about SPs/transactions).
    /// </summary>
    public class DonHangService
    {
        private readonly KhachHangRepository _khRepo;
        private readonly DonHangRepository _dhRepo;
        private readonly GiaoHangRepository _ghRepo;

        public DonHangService()
        {
            _khRepo = new KhachHangRepository();
            _dhRepo = new DonHangRepository();
            _ghRepo = new GiaoHangRepository();
        }

        /// <summary>
        /// Create a complete order: find/create customer → create order → add items → create delivery.
        /// ENCAPSULATION: all logic in one method (was scattered in ucTaoDon UI).
        /// </summary>
        public string TaoDonHang(string tenKH, string sdt, string diaChi,
                                 string hinhThuc, string ghiChu, DataTable gioHang,
                                 string maNV, out string error)
        {
            error = "";

            // Step 1: Validate
            if (string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(sdt))
            {
                error = "Thiếu thông tin khách hàng (tên, SĐT).";
                return null;
            }
            if (gioHang == null || gioHang.Rows.Count == 0)
            {
                error = "Giỏ hàng trống.";
                return null;
            }

            // Step 2: Find or create customer
            string maKH = _khRepo.TimHoacTao(tenKH, sdt, diaChi);

            // Step 3: Create order with full transaction (atomic)
            string maDon = _dhRepo.TaoDonHangHoanChinh(maKH, maNV, hinhThuc, ghiChu, gioHang);

            return maDon;
        }

        /// <summary>
        /// Update order status with validation.
        /// ENCAPSULATION: business rules checked before DB update.
        /// </summary>
        public bool CapNhatTrangThai(string maDon, string trangThaiMoi, out string error)
        {
            error = "";
            var don = _dhRepo.LayThongTinDon(maDon);
            if (don == null)
            {
                error = "Không tìm thấy đơn hàng.";
                return false;
            }

            // Business rules: can't change completed/cancelled orders
            if (don.IsComplete || don.IsCancelled)
            {
                error = "Không thể thay đổi trạng thái đơn hàng đã hoàn thành hoặc đã hủy.";
                return false;
            }

            _dhRepo.CapNhatTrangThai(maDon, trangThaiMoi);
            return true;
        }

        /// <summary>
        /// Get order details with full info.
        /// </summary>
        public DonHang LayChiTietDon(string maDon)
        {
            var don = _dhRepo.LayThongTinDon(maDon);
            if (don != null)
            {
                don.ChiTiet = _dhRepo.LayChiTiet(maDon);
            }
            return don;
        }
    }
}
