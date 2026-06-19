using System;
using System.Data;
using FloriSys.DataAccess;
using FloriSys.Models;

namespace FloriSys.Services
{
    /// <summary>
    /// Service Trả Hàng - đóng gói các nghiệp vụ xử lý khách trả lại hàng.
    /// Thể hiện: TÍNH ĐÓNG GÓI (xử lý đồng thời tạo phiếu trả + cập nhật lại tồn kho + đổi trạng thái đơn hàng).
    /// </summary>
    public class TraHangService
    {
        private readonly TraHangRepository _thRepo;

        public TraHangService()
        {
            _thRepo = new TraHangRepository();
        }

        /// <summary>
        /// Xử lý một phiếu trả hàng hoàn chỉnh: tạo phiếu trả + chi tiết + hoàn lại tồn kho + cập nhật trạng thái đơn.
        /// TÍNH ĐÓNG GÓI: mọi bước được xử lý nội bộ thông qua Transaction (Đảm bảo tính toàn vẹn).
        /// </summary>
        public string XuLyTraHang(string maDon, string lyDo, string hinhThuc,
                                   string ghiChu, DataTable chiTietTra, out string error)
        {
            error = "";

            if (string.IsNullOrEmpty(maDon))
            {
                error = "Thiếu mã đơn hàng.";
                return null;
            }
            if (chiTietTra == null || chiTietTra.Rows.Count == 0)
            {
                error = "Chưa chọn sản phẩm trả.";
                return null;
            }

            // Ủy thác cho phương thức có hỗ trợ Transaction ở tầng Repository
            string maPhieu = _thRepo.ThemPhieuTraHoanChinh(maDon, lyDo, hinhThuc, ghiChu, chiTietTra);

            return maPhieu;
        }
    }
}

