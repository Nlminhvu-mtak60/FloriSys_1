using System;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Service Giao Hàng - đóng gói logic phân công shipper và cập nhật trạng thái giao hàng.
    /// Thể hiện: TÍNH ĐÓNG GÓI (giữ cho trạng thái đơn hàng và trạng thái giao hàng luôn đồng bộ).
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
        /// Phân công shipper cho một đơn giao hàng và cập nhật trạng thái.
        /// TÍNH ĐÓNG GÓI: Giữ cho trạng thái giao hàng và đơn hàng đồng bộ.
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
        /// Cập nhật trạng thái giao hàng.
        /// TÍNH ĐÓNG GÓI: Kiểm tra tính hợp lệ khi chuyển đổi trạng thái.
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
        /// Lấy thống kê hiệu suất của shipper (số đơn thành công, đang giao...).
        /// </summary>
        public ThongKeShipper ThongKe(string maNV)
        {
            return _ghRepo.ThongKe(maNV);
        }
    }
}
