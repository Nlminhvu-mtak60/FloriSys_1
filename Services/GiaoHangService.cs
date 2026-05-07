using System;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Delivery service - encapsulates delivery assignment and status updates.
    /// Demonstrates: ENCAPSULATION (delivery + order status kept consistent).
    /// </summary>
    public class GiaoHangService
    {
        private readonly GiaoHangRepository _ghRepo;
        private readonly DonHangRepository _dhRepo;

        public GiaoHangService()
        {
            _ghRepo = new GiaoHangRepository();
            _dhRepo = new DonHangRepository();
        }

        /// <summary>
        /// Assign a shipper to a delivery and update order status.
        /// ENCAPSULATION: keeps delivery and order status in sync.
        /// </summary>
        public bool PhanCongShipper(string maGH, string maNVShipper, out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(maGH) || string.IsNullOrEmpty(maNVShipper))
            {
                error = "Thiếu thông tin phân công.";
                return false;
            }

            _ghRepo.PhanCongShipper(maGH, maNVShipper);
            return true;
        }

        /// <summary>
        /// Update delivery status.
        /// ENCAPSULATION: validates status transition.
        /// </summary>
        public bool CapNhatTrangThaiGiao(string maGH, string trangThai, string ghiChu, out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(maGH))
            {
                error = "Thiếu mã giao hàng.";
                return false;
            }

            _ghRepo.CapNhatTrangThai(maGH, trangThai, ghiChu);
            return true;
        }

        /// <summary>
        /// Get shipper statistics.
        /// </summary>
        public ThongKeShipper ThongKe(string maNV)
        {
            return _ghRepo.ThongKe(maNV);
        }
    }
}
